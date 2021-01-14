using NHibernate;
using NHibernate.Cfg;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Domains;
using Orion.Framework.Helpers;
using Orion.Framework.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Framework.DataLayer.NH
{
    public sealed class NHibernateManager : FrameworkComponentBase
    {


        public static NHibernateManager Instance
        {
            get
            {
                return Nested.NHibernateManager;
            }
        }
        private class Nested
        {
            static Nested() { }
            internal static readonly NHibernateManager NHibernateManager =
                new NHibernateManager();
        }
        private NHibernateManager()
        {

        }
        public ISession GetSession<T>()
        {
            var sessionContexts = AppHelper.GetServices<ISessionContext>();
            var unitOfWork = Ioc.Create<IUnitOfWork>();

            foreach (var item in sessionContexts)
            {
                var entity = AppHelper.GetCache<IEnumerable<EntityTypeInfo>>(item.Name).FirstOrDefault(f => f.EntityType == typeof(T));
                if (entity != null)
                {
                     
                }
            }
            return null;
        }

        public void UsingSession<TSessionContext>(Action<ISession> action) where TSessionContext : ISessionContext
        {
            var _connection = The<ISessionFactoryProvider>().GetDbConnection<TSessionContext>();
            using (ISession session = The<ISessionFactoryProvider>().GetSessionFactory<TSessionContext>().OpenSessionWithConnection(_connection))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<TSessionContext, T>(Func<ISession, T> func) where TSessionContext : ISessionContext
        {
            var _connection = The<ISessionFactoryProvider>().GetDbConnection<TSessionContext>();
            T result;

            using (ISession session = The<ISessionFactoryProvider>().GetSessionFactory<TSessionContext>().OpenSessionWithConnection(_connection))
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    result = func(session);
                    session.Flush();
                    transaction.Commit();
                }
            }

            return result;
        }



    }

}
