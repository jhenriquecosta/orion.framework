using NHibernate;
using NLog;
using System;
using System.Threading.Tasks;
using Orion.Framework.Dependency;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.NH.Helpers
{
    public interface INHibernateHelper: IScopeDependency
    {
        ISession CurrentSession { get; }        
        void CloseSession();
        void Flush();
       
        
    }
    public class NHibernateHelper : INHibernateHelper
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly IServiceProvider _nhProvider;
        private readonly ISessionFactoryProvider _sessionFactory;
        private  ISession _session;
        private static NHibernateHelper _instance;
        public string TraceId { get; set; }
        public NHibernateHelper()
        {
            _sessionFactory = Ioc.Create<ISessionFactoryProvider>();
            _nhProvider = Ioc.Create<IServiceProvider>();
            TraceId = Guid.NewGuid().ToString();
            Logger.Info($"Data: {DateTime.Now} Trace {TraceId} Iniciando NHibernateHelper");

        }
        public ISessionFactory GetSessionFactory()
        {
            return _sessionFactory.GetSessionFactory();
        }
      
        public ISession OpenSession()
        {
            _session = _sessionFactory.GetSessionFactory().OpenSession();
            return _session;
        }
        public static NHibernateHelper Instance => _instance ?? (_instance = new NHibernateHelper());
       
        public ISession CurrentSession => OpenSession();
        public void CloseSession()
        {
            _session.Close();
            _session.Dispose();           
        }
        public void Flush()
        {
            _session.Flush();
        }
        public TOut Execute<TOut>(Func<ISession, TOut> actionToWrap, bool withTransaction = true)
        {
            object result = null;
            try
            {

                using (var session = OpenSession())
                {
                    result = actionToWrap(session);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return (TOut)result;

        }
        public async Task<TOut> ExecuteAsync<TOut>(Func<ISession, TOut> actionToWrap)
        {
            var session = OpenSession();

            var trans = session.BeginTransaction();
            try
            {
                return actionToWrap(session);
            }
            finally
            {
                if (trans.IsActive) await trans.CommitAsync();
                session.Close();
            }
        }
        public void UsingSession(Action<ISession> action)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    action(session);
                    session.Flush();
                    transaction.Commit();
                }
            }
        }

        public T UsingSession<T>(Func<ISession, T> func)
        {
            T result;

            using (var session = Ioc.Create<ISessionFactoryProvider>().GetSessionFactory().OpenSession())
            {
                using (var transaction = session.BeginTransaction())
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
