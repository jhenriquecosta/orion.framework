#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion
using System.Linq;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NLog;
using NHibernate.Cfg;
using Environment = NHibernate.Cfg.Environment;
using Orion.Framework.DataLayer.NH.Events.Listeners;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Settings;
using Orion.Framework.Helpers;
using Orion.Framework.DataLayer.NH.Events.Interceptors;
using System.Collections.Generic;
using Orion.Framework.Domains.Enums;
using Orion.Framework.DataLayer.NH.Fluent;
using Orion.Framework.DataLayer.NH.Events.Filters;
using Orion.Framework.DataLayer.SessionContext;
using NHibernate;
using NHibernate.Util;

namespace Orion.Framework.DataLayer.NH
{


    public class DbBuildFluentConfiguration : IDbBuildConfiguration
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private static DbBuildFluentConfiguration _instance = null;
        private ISessionContext _sessionContext;
        private IPersistenceConfigurer _persistenceConfigurer;
        private Configuration _configuration;
        private bool _exportMappings;
        private string _exportMappingsFolder;
        private Database _database;
        private DatabaseAction _databaseAction;

        private string _strConnection;
        public static DbBuildFluentConfiguration Configure => _instance ?? (_instance = new DbBuildFluentConfiguration());

        public IDbBuildConfiguration WithContext(ISessionContext sessionContext)
        {
            _sessionContext = sessionContext;
            Settings();
            return _instance;
        }

        public DbBuildFluentConfiguration()
        { }
        public DbBuildFluentConfiguration(ISessionContext context)
        {
            if (context != null) _sessionContext = context;
            Settings();
        }
        public IDbBuildConfiguration Build()
        {
            return _instance;
        }


        void Settings()
        {

            var key = _sessionContext.Name;
            var props = AppHelper.AppSettings().GetSection(XTConstants.AppSettingsSessionContext).GetSection(key).GetValue<IDictionary<string, object>>();
            var strDatabase = props["Database"];
            var strConnectionKey = props["ConnectionString"].ToString();
            _databaseAction = props["SchemaAction"].To<DatabaseAction>();

            _exportMappings = props["ExportMappings"].To<bool>();
            _exportMappingsFolder = props["ExportMappingsFolder"].ToString();
            _strConnection = AppHelper.AppSettings().GetStringConnection(strConnectionKey);
            _database = (Database)HelperEnum.GetValue<Database>(strDatabase);
            _persistenceConfigurer = new UseDatabase(_database, _strConnection).Configure();
        


        }
        /// <summary>
        /// Configures this instance.
        /// </summary>
        /// <returns>IEnumerable &lt;Configuration&gt;</returns>
        public Configuration GetConfiguration()
        {
            var cfg = BuildConfiguration();

            return cfg; // new SingletonEnumerable<Configuration>(cfg); ;
        }

        
        public Configuration BuildConfiguration()
        {
            
            _configuration = new Configuration();

            var deleteEventListener = new SoftDeleteEventListener(() => Ioc.Create<Sessions.ISession>());
            var _fluentConfiguration = Fluently.Configure()
                     .Database(_persistenceConfigurer)
                     .Mappings(m =>
                     {
                         var mappings = m.AutoMappings.Add(GetAutoPersistenceModel());
                         ExportMappings(mappings);
                     }).Cache(c => c.UseSecondLevelCache())                    
                     .ExposeConfiguration(cfg =>
                     {
                         BuildSchema(cfg);     
                     });

            var buildConfiguration = _fluentConfiguration.BuildConfiguration();
         
            _configuration = buildConfiguration
#if DEBUG

                  .SetProperty(Environment.ShowSql, "false")
                  .SetProperty(Environment.UseSqlComments, "true")
                  .SetProperty(Environment.FormatSql, "true")

#endif             
                  .SetProperty(Environment.CommandTimeout, "60")
                  .SetProperty(Environment.PrepareSql, "true")
                  .SetProperty(Environment.GenerateStatistics, "true");





            //if (WebHttp.IsWebContext())
            ////Verifico se a aplicação é Desktop ou Web
            //{
            //    _configuration.CurrentSessionContext<WebSessionContext>();
            //}
            //else
            //{
            //    _configuration.CurrentSessionContext<ThreadStaticSessionContext>();

            //}
            //_configuration.AddInterceptor(interceptor);
            //  _configuration.AddInterceptor(new AuditableInterceptor(() => Ioc.Create<Sessions.ISession>()));

            var audit = AppHelper.GetService<AuditInterceptors>();
            _configuration.AddInterceptor(audit);

            return _configuration;

        }
        private void BuildSchema(Configuration cfg)
        {
            if (_databaseAction == DatabaseAction.Update) cfg.DBUpdate(_sessionContext.Name);
            if (_databaseAction == DatabaseAction.Reset) cfg.DBReset(_sessionContext.Name);
            // delete the existing db on each run
            //if (File.Exists(DbFile))
            //    File.Delete(DbFile);

            //// this NHibernate tool takes a configuration (with mapping info in)
            //// and exports a database schema from it
            //new SchemaExport(config)
            //    .Create(false, true);
        }
        private void ExportMappings(AutoMappingsContainer container)
        {
            if (!_exportMappings) return;
            if (_exportMappingsFolder.IsNullOrEmpty()) _exportMappingsFolder = "default";
            var folderMapping = AppHelper.SysSettings().Folder.Mappings;
            folderMapping = AppHelper.GetFolder(new string[] { folderMapping, _exportMappingsFolder });
            container.ExportTo(folderMapping);
        }

        private AutoPersistenceModel GetAutoPersistenceModel()
        {
            var autoPersistenceModel = AutoMap.Assemblies(new AutomappingConfiguration(), _sessionContext.LocateDomainAssemblies().ToArray());

            // Add Pillar NHibernate conventions
            autoPersistenceModel = autoPersistenceModel.Conventions.AddFromAssemblyOf<AutomappingConfiguration>();

            // To allow two persistent classes with the same unqualified name

          
            // Add conventions and overrides from infrastructure assebmlies
            var infrastructureAssebmlies = _sessionContext.LocateInfrastructureAssemblies();
            foreach (var infrastructureAssebmly in infrastructureAssebmlies)
            {
                autoPersistenceModel = autoPersistenceModel
               .Conventions.AddAssembly(infrastructureAssebmly)
               .UseOverridesFromAssembly(infrastructureAssebmly);
            }
            foreach (var type in _sessionContext.IgnoreBaseTypes())
            {
                autoPersistenceModel.IgnoreBase(type);
            }
            autoPersistenceModel.AddFilter<AppFilterSoftDelete>();
            autoPersistenceModel.AddFilter<AppFilterOrganization>();
            return autoPersistenceModel;
        }
        #region Code Backup
        // .CurrentSessionContext<ThreadStaticSessionContext>()
        //.Cache(
        //    p =>
        //    p.UseSecondLevelCache().UseQueryCache().ProviderClass(
        //        typeof(global::NHibernate.Caches.SysCache.SysCacheProvider).AssemblyQualifiedName))

        #endregion
    }
}
