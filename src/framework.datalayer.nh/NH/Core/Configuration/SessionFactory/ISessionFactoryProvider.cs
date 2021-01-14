using NHibernate.Cfg;
using NHibernate;
using System.Collections.Concurrent;
using Orion.Framework.DataLayer.SessionContext;
using System.Data.Common;

namespace Orion.Framework.DataLayer.NH.Contracts
{
    public interface ISessionFactoryProvider
    {
        ISessionFactory GetSessionFactory<TSessionContext>() where TSessionContext : ISessionContext;
        ISessionFactory GetSessionFactory(string key = null);

        void SetSessionFactory(ISessionFactory sessionFactory,string key = null);

        Configuration GetConfiguration(string key = null);
        ConcurrentDictionary<string, Configuration> GetConfigurations();

        void SetConfiguration(Configuration configuration,string key);
        DbConnection GetDbConnection<TSessionContext>() where TSessionContext : ISessionContext;

    }
}
