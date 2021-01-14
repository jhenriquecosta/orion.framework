using NHibernate;
using NHibernate.Cfg;

namespace Orion.Framework.DataLayer.NHibernate.Contracts
{

    public interface ISessionFactoryProvider
    {
        ISession OpenSession();
        ISession GetSession();
        ISession GetSessionWithFilter();

        IStatelessSession GetSessionReadOnly();
        ISessionFactory GetSessionFactory();

        ISessionFactory GetSessionFactory(string key);

        void SetSessionFactory(ISessionFactory sessionFactory);

        void SetSessionFactory(string key, ISessionFactory sessionFactory);

        Configuration GetConfiguration();

        Configuration GetConfiguration(string key);

        void SetConfiguration(Configuration configuration);

        void SetConfiguration(string key, Configuration configuration);

        
    }

}
