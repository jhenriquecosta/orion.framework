using FluentNHibernate.Cfg.Db;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Domains.Enums;

namespace Orion.Framework.DataLayer.NH.Fluent
{
    
    public class UseDatabase : IPersistenceFluentProvider
    {
        IPersistenceConfigurer _database = null;
        Database _provider;
        string _connex;
        public UseDatabase(Database persistent, string strConnex)
        {
            _provider = persistent;
            _connex = strConnex;
        }
        public IPersistenceConfigurer Configure()
        {
            return Get();
        }
        public IPersistenceConfigurer Get()
        {
            if (_provider == Database.SqlServer)
            {
                 _database = MsSqlConfiguration.MsSql2012.ConnectionString(builder => builder.Is(_connex))
                .AdoNetBatchSize(10)
                .UseOuterJoin()
                .ShowSql()
                .QuerySubstitutions("true 1, false 0, yes 'Y', no 'N'");
            }
            else if (_provider == Database.MySql)
            {
                _database = MySQLConfiguration.Standard.ConnectionString(builder => builder.Is(_connex))
                .AdoNetBatchSize(10)
               .UseOuterJoin()
               .ShowSql()
               .QuerySubstitutions("true 1, false 0, yes 'Y', no 'N'");
            }
            else if (_provider == Database.Sqlite)
            {
                _database = SQLiteConfiguration.Standard
               .AdoNetBatchSize(10)
               .UseOuterJoin()
               .QuerySubstitutions("true 1, false 0, yes 'Y', no 'N'")
               .ConnectionString(_connex);
            }

            return _database;
        }
        
    }
}
