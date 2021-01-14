using System;
using System.Data;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Sessions.Configuration;
using NLog;
using Zeus.DataLayer.UnitOfWorks;
using ISession = NHibernate.ISession;

namespace Zeus.DataLayer.NHibernate
{
    public class NHUnitOfWork : IUnitOfWorkBase
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly IServiceProvider _serviceProvider;
        private readonly ISingleSessionFactoryConfigurer _sessionFactory;
        private ITransaction _transaction;
        private ISession _session;

        public ISession GetSession()
        {
            if (_session != null && (_session != null || _session.IsConnected)) return _session;
            _session?.Dispose();
            _session = _sessionFactory.CreateLazySessionFactory().Value.OpenSession();
            return _session;

        }

        public ISession Session => GetSession();

        public string TraceId { get; set; }
        public Guid? Id { get; set; }

        [field: ThreadStatic]
        public static NHUnitOfWork Current { get; set; }

        public NHUnitOfWork(ISingleSessionFactoryConfigurer _session)
        {
            _sessionFactory = _session;
            Id = Guid.NewGuid();
            TraceId = Id.ToString();
            Logger.Info($"Data: {DateTime.Now} Trace {TraceId}");
        }

        //public ISession GetSession()
        //{
        //    if (_session == null || !_session.IsConnected)
        //    {
        //        _session?.Dispose();
        //        _session = _serviceProvider.GetSessionFactory().OpenSession();
        //        //_session.FlushMode = FlushMode.Auto;
        //        //_session.CacheMode = CacheMode.Normal;
        //    }

        //    return _session;

        //}
        public void BeginTransaction(IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
         
            if (_transaction != null && _transaction.IsActive) return;
            _transaction?.Dispose();
            _transaction = _session.BeginTransaction(isolationLevel);
        }
        
        public void Commit()
        {
            CommitAsync().Wait();
        }

        public async Task CommitAsync()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                {
                    _session.Flush();
                    _session.Clear();
                }

                if (_transaction != null) await _transaction.CommitAsync();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.RollbackAsync();
                throw;
            }
            finally
            {
                CloseSession();
            }

        }

        public void Flush()
        {
            if (_session != null)
            {
                _session.Flush();
                _session.Clear();
                _session.Close();
                _session.Dispose();
                _session = null;
            }
        }
        public void Rollback()
        {
            RollbackAsync().Wait();
        }

        public async Task RollbackAsync()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.RollbackAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                CloseSession();
            }
        }
        public void CloseSession()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }

            if (_session != null)
            {
                _session.Flush();
                _session.Clear();
                _session.Close();
                _session.Dispose();
                _session = null;
            }
           

        }

        public void Dispose()
        {

        }
    }


}
