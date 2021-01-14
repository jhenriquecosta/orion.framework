
using System;
using System.Data;
using Framework.DataLayer.NHibernate.DataLayer.Stores;
using Framework.DataLayer.NHibernate.Domains;
using Framework.DataLayer.NHibernate.DataLayer.UnitOfWorks;
using NLog;
using Framework.DataLayer.NHibernate.UnitOfWorks;

namespace Framework.DataLayer.NHibernate.Repository.Base
{
    public abstract partial class RepositoryBase<TEntity> : RepositoryBase<TEntity, int>, IStore<TEntity> where TEntity : class, IKey<int>, IVersion
    {
        protected RepositoryBase(IUnitOfWorkFactory unitOfWork) : base(unitOfWork)
        {
           
        }
    }

   // [ServiceInterceptor(typeof(NHUnitOfWorkAttribute))]
    public abstract partial class RepositoryBase<TEntity,TKey> : IStore<TEntity, TKey> where TEntity : class, IKey<TKey>
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        //private readonly IUnitOfWork defaultUnitOfWork;
        private IUnitOfWork unitOfWork;
        public IUnitOfWorkBase UnitOfWork
        {
            get
            {
                if (unitOfWork != null && !unitOfWork.Disposed)
                {
                    return unitOfWork;
                }

                //if (defaultUnitOfWork != null && !defaultUnitOfWork.Disposed)
                //{
                //    return defaultUnitOfWork;
                //}

                throw new DataException(string.Format("Repository {0} has no assigned unit of work or it was disposed.", GetType().Name));
            }
        }
        public void Use(IUnitOfWork unitOfWorkToUse)
        {
            unitOfWork = unitOfWorkToUse;
        }
        protected RepositoryBase(IUnitOfWorkFactory unitOfWorkFactory)
        {
            var uow = (DefaultUnitOfWork) unitOfWorkFactory.Get(typeof(TEntity).FullName);

            unitOfWork =  uow ?? throw new ArgumentNullException("unitOfWork");
            //Session = uow.ActiveSession;
            //SessionReadOnly = uow.ActiveSessionReadOnly;
        }


       
        public void Dispose()
        {
            Dispose(true);
        }

        ~RepositoryBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                UnitOfWork.Dispose();
                GC.SuppressFinalize(this);
            }
        }

      
    }
}
