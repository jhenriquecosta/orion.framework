using NHibernate;
using NHibernate.Cfg;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Settings;
using System.Collections.Concurrent;
using System.Data.Common;
using System.Linq;
using Engine = NHibernate.Engine;

namespace Orion.Framework.DataLayer.NH
{
    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private ConcurrentDictionary<string, ISessionContext> sessionContexts = new ConcurrentDictionary<string, ISessionContext>();
        private ConcurrentDictionary<string, ISessionFactory> sessionFactories = new ConcurrentDictionary<string, ISessionFactory>();
        private ConcurrentDictionary<string, Configuration> configurations = new ConcurrentDictionary<string, Configuration>();
        private static readonly string DefaultConfigurationKey = XTConstants.SESSION_CONTEXT_DEFAULT;
        private static readonly string DefaultSessionFactoryKey = nameof(sessionFactories);


        private ISessionContext GetContext<TSessionContext>() where TSessionContext : ISessionContext
        {
            var key = typeof(TSessionContext).Name;
            if (sessionContexts.Count == 0)
            {
                var _context = Ioc.Create<TSessionContext>();
                sessionContexts[key] = _context;
            }
            var context = sessionContexts.GetOrAdd(
               key,
               k =>
               {
                   var _context = sessionContexts[key];
                   if (_context == null)
                   {

                   }
                   return _context;
               }
           );
           return context;
        }
        public DbConnection GetDbConnection<TSessionContext>() where TSessionContext : ISessionContext
        {
            var _sessionFactory = GetSessionFactory<TSessionContext>();
            var dbconn = (_sessionFactory as Engine.ISessionFactoryImplementor).ConnectionProvider.GetConnection();
            return dbconn;
        }

        public ISessionFactory GetSessionFactory<TSessionContext>() where TSessionContext : ISessionContext
        {
            var _context = GetContext<TSessionContext>();
            var factory = GetSessionFactory(_context.Name);
            return factory;
        }

        public ConcurrentDictionary<string, Configuration> GetConfigurations()
        {
            return configurations;
        }
        public void SetSessionFactory(ISessionFactory sessionFactory, string key)
        {
            key ??= DefaultSessionFactoryKey;
            sessionFactories[key] = sessionFactory;

        }
       
        public ISessionFactory GetSessionFactory(string key = null)
        {
            return GetSessionFactoryFor(key);
        }
        private ISessionFactory GetSessionFactoryFor(string key)
        {
            return sessionFactories.GetOrAdd(
                key,
                k => {
                    var cfg = GetConfigurationByKey(key);
                    if (cfg == null)
                    {
                        throw new System.InvalidOperationException(
                            $"Configuration with {key} does not exists!"
                        );
                    }
                    var sessionFactory = cfg.BuildSessionFactory();
                    return sessionFactory;
                }
            );
        }

      
        private Configuration GetConfigurationByKey(string key = null)
        {
            if (key == null && configurations.Count == 1)
            {
                var value = configurations.FirstOrDefault();
                key = value.Key;
            }
            return configurations[key];
        }
        public Configuration GetConfiguration(string key = null)
        {
            //key ??= DefaultConfigurationKey;
            var cfg = GetConfigurationByKey(key);
           
            return cfg;
           
        }
        
        public void SetConfiguration(Configuration configuration,string key)
        {
            key ??= DefaultConfigurationKey;
            configurations[key] = configuration;
            //var all = configuration.GetEntityTypeInfos(key).ToList();
            //entities[key] = all;
        } 
 
    }
}
