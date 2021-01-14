using System;
using System.Collections.Concurrent;
using Orion.Framework.Helpers;
using NHibernate;
using NHibernate.Cfg;
using Orion.Framework.DataLayer.NHibernate.Contracts;

namespace Orion.Framework.DataLayer.NHibernate
{

    public class SessionFactoryProvider : ISessionFactoryProvider
    {
        private ConcurrentDictionary<string, ISessionFactory> sessionFactories = new ConcurrentDictionary<string, ISessionFactory>();
        private ConcurrentDictionary<string, Configuration> configurations = new ConcurrentDictionary<string, Configuration>();

        private static readonly string DefaultConfigurationKey = typeof(Configuration).FullName;
        private static readonly string DefaultSessionFactoryKey= typeof(ISessionFactory).FullName;

        public SessionFactoryProvider()         
        {
            
        }
        public ISession GetSession()
        {
            //if (trackEntitiesConcurrency)
            //{
            //    return GetSessionFactory().WithOptions().Interceptor(new StaleInterceptor()).OpenSession();
            //}
            //return GetSessionFactory().OpenSession(); 
            return  Ioc.Create<ISession>();
        }
        public ISession OpenSession()
        {
           
            return GetSessionFactory().OpenSession(); 
           // return Ioc.Create<ISession>();
        }
        public ISession GetSessionWithFilter()
        {
            try
            {
                //if (CurrentSessionContext.HasBind(GetSessionFactory()))
                //{
                //    var _currentSession = GetSessionFactory().GetCurrentSession();
                //    if (!_currentSession.IsConnected)
                //    {
                //        CurrentSessionContext.Unbind(GetSessionFactory());
                //    }
                //    else
                //    {
                //        return _currentSession;
                //    }
                //}
                //var session = GetSessionFactory().OpenSession();
                var session = Ioc.Create<ISession>();
                ////session.FlushMode = FlushMode.Commit;

                ////session.EnableFilter(XTAppSettings.OrganizationFilterName)
                ////       .SetParameter(XTAppSettings.OrganizationCodeQueryParamName, XTAppSettings.OrganizationCode)
                ////       .SetParameter(XTAppSettings.SoftDeleteParamName, true);

                //CurrentSessionContext.Bind(session);

                return session;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível criar a Sessão.", ex);
            }
        }
        public IStatelessSession GetSessionReadOnly()
        {
            return GetSessionFactory().OpenStatelessSession();

        }
        public ISessionFactory GetSessionFactory()
        {
                var sessionFactory= sessionFactories.GetOrAdd(
                DefaultSessionFactoryKey,
                key =>
                {
                    var cfg = configurations[DefaultConfigurationKey];
                    if (cfg == null)
                    {
                        throw new System.InvalidOperationException(
                            "Default configuration does not exists!"
                        );
                    }
                    var sessionFactory = cfg.BuildSessionFactory();
                    return sessionFactory;
                }
            );
            return sessionFactory;
        }

        public ISessionFactory GetSessionFactory(string key) 
        {
            return sessionFactories.GetOrAdd(
                key,
                k => {
                    var cfg = configurations[key];
                    if (cfg == null) {
                        throw new System.InvalidOperationException(
                            $"Configuration with {key} does not exists!"
                        );
                    }
                    var sessionFactory = cfg.BuildSessionFactory();
                    return sessionFactory;
                }
            );
        }

        public void SetSessionFactory(ISessionFactory sessionFactory) {
            sessionFactories[DefaultSessionFactoryKey] = sessionFactory;
        }

        public void SetSessionFactory(string key, ISessionFactory sessionFactory) {
            sessionFactories[key] = sessionFactory;
        }

        public Configuration GetConfiguration() {
            return configurations[DefaultConfigurationKey];
        }

        public Configuration GetConfiguration(string key) {
            return configurations[key];
        }

        public void SetConfiguration(Configuration configuration)
        {
            configurations[DefaultConfigurationKey] = configuration;
        }

        public void SetConfiguration(string key, Configuration configuration) {
            configurations[key] = configuration;
        }

       

    }

}
