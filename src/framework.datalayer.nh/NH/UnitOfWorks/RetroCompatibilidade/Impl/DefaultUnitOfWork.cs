using System;
using System.Data;
using System.Threading.Tasks;
using NHibernate;
using NLog;
using Orion.Framework.Domains;
using Orion.Framework.Settings;
using Orion.Framework.Utilities;
using Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts;
using Orion.Framework.DataLayer.NHibernate.Contracts;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks
{


    public class DefaultUnitOfWork : IUnitOfWork
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ISessionFactoryProvider sessionFactoryProvider;
        private volatile ISession session;
        private volatile IStatelessSession sessionReadOnly;

        private ITransaction transaction;
        private bool disposed;

        public Type Entity { get; set; }
        public string TraceId { get; set; }

        public bool Disposed
        {
            get
            {
                return disposed;
            }
        }
        public DefaultUnitOfWork(ISessionFactoryProvider sessionFactoryProvider)
        {
            this.Init();
            this.sessionFactoryProvider = sessionFactoryProvider;
           // this.session = Session;
            transaction = null;
        }
        private bool IsClose()
        {
            var connect = (session == null) || (!session.IsConnected);
            return connect;
        }
        public ISession Session
        {
            get
            {
                if (disposed) return null;
                CheckDisposed();
                if (IsClose())
                {
                    lock (this)
                    {
                        if (IsClose())
                        {

                            ISession _session = sessionFactoryProvider.GetSessionFactory().OpenSession();
                          //  var entityBase = entity.GetType();
                            var isDeleteble = typeof(ISoftDelete).IsAssignableFrom(Entity);
                            if (isDeleteble)
                            {
                                _session.EnableFilter(XTAppSettings.SoftDeleteFilterName)
                                       .SetParameter(XTAppSettings.SoftDeleteParamName, true);
                            }
                            //newSession.FlushMode = FlushMode.Commit;
                            ////session.FlushMode = FlushMode.Commit;
                            ////session.EnableFilter(XTAppSettings.OrganizationFilterName)
                            ////       .SetParameter(XTAppSettings.OrganizationCodeQueryParamName, XTAppSettings.OrganizationCode)
                            ////       .SetParameter(XTAppSettings.SoftDeleteParamName, true);
                            ///
                            session = _session;
                        }
                    }
                }
                return session;
            }
        }

        public IStatelessSession SessionReadOnly
        {
            get
            {
                if (disposed) return null;
                CheckDisposed();
                if (sessionReadOnly == null)
                {
                    lock (this)
                    {
                        if (sessionReadOnly == null)
                        {

                            IStatelessSession newSession = sessionFactoryProvider.GetSessionFactory().OpenStatelessSession();
                            sessionReadOnly = newSession;
                        }
                    }
                }
                return sessionReadOnly;
            }
        }
        public bool IsActiveTransaction
        {
            get { return transaction != null && transaction.IsActive; }
        }

        ~DefaultUnitOfWork()
        {
            Dispose(false);
        }
        void Init()
        {  
            TraceId = Guid.NewGuid().ToString();
           
           
        }
     

        //public DefaultUnitOfWork(ISession session)
        //{
        //    this.Init();
        //    this.session = session;           
        //    sessionFactoryProvider = null;
        //    transaction = null;
        //}

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    if (transaction != null && transaction.IsActive && !transaction.WasRolledBack)
                    {
                        transaction.Rollback();
                        transaction.Dispose();
                    }

                    if (session != null)
                    {
                        if (!IsClose())
                        {
                            session.Close();
                            session.Dispose();
                            session = null;
                        }
                    }
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
           
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Commit()
        {
            AsyncUtil.RunSync(() => CommitAsync());
        }

        public async Task CommitAsync()
        {
            CheckDisposed();

            if (transaction != null)
            {
                if (!transaction.IsActive)
                {
                    throw new InvalidOperationException("No active transaction.");
                }

               
                await transaction.CommitAsync();
                await FlushAndClear();
                CloseTransaction();
            }
            else if (session != null)
            {
                await FlushAndClear();
            }
        }

        public void CloseTransaction()
        {
            CheckDisposed();
            if (transaction != null)
             {
                transaction.Dispose();
                transaction = null;
             }
            
         }
        public async Task FlushAndClear()
        {
            await session.FlushAsync();
            session.Clear();
        }

        public void BeginTransaction()
        {
            CheckDisposed();

            BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            CheckDisposed();

            if (transaction == null)
            {
                transaction = session.BeginTransaction(isolationLevel);
            }
            else
            {

                Rollback();
                throw new DataException("Transaction already created for this unit of work.");
            }
        }

        public void Rollback()
        {
            AsyncUtil.RunSync(() => RollbackAsync());
        }
        public async Task RollbackAsync()
        {
            CheckDisposed();

            if (transaction != null)
            {
                if (transaction.IsActive)
                {
                    await transaction.RollbackAsync();
                    transaction = null;
                }
            }
            else
            {
                throw new DataException("No transaction created for this unit of work.");
            }
        }
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException("UnitOfWork");
            }
        }
    }
}
