using Orion.Framework.DataLayer.NHibernate.Contracts;
using Orion.Framework.DataLayer.NHibernate.UnitOfWorks.Contracts;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.DataLayer.NHibernate.UnitOfWorks
{
    public class DefaultUnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly ISessionFactoryProvider sessionFactoryProvider;
        public DefaultUnitOfWorkFactory(ISessionFactoryProvider sessionFactoryProvider)
        {
            this.sessionFactoryProvider = sessionFactoryProvider;
        }

        public IUnitOfWork Get(string uowName)
        {

            var uow = DataStoreCache.Get<IUnitOfWork>(uowName);
            if (uow.IsNull())
            {
               uow=  new DefaultUnitOfWork(sessionFactoryProvider); 
            }
            if (uowName.IsNullOrEmpty())
            {
                uowName = uow.TraceId;
            }
            DataStoreCache.Remove(uowName);
            DataStoreCache.Add(uowName, uow);
            return uow;
        }

       
        public IUnitOfWork New()
        {

            var uow = new DefaultUnitOfWork(sessionFactoryProvider);          
            return uow;

        }

        public void Remove(string uowName)
        {
            DataStoreCache.Remove(uowName);
        }
    }
}
