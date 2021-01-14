using NHibernate;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;

namespace Orion.Framework.DataLayer.NH.UnitOfWorks
{
    public class NhUnitOfWorkSessionProvider : ISessionProvider
    {
        private readonly ICurrentUnitOfWorkProvider _currentUnitOfWorkProvider;

        public NhUnitOfWorkSessionProvider(ICurrentUnitOfWorkProvider currentUnitOfWorkProvider)
        {
            _currentUnitOfWorkProvider = currentUnitOfWorkProvider;
             
        }

        public ISession GetSession<TSessionContext>() where TSessionContext : ISessionContext
        {
            return _currentUnitOfWorkProvider.Current.GetSession<TSessionContext>();
        }
    }
    
}
