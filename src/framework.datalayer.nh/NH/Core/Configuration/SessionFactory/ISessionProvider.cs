using NHibernate;
using Orion.Framework.DataLayer.SessionContext;

namespace Orion.Framework.DataLayer.NH.Contracts
{
    public interface ISessionProvider
    {
        ISession GetSession<TSessionContext>() where TSessionContext : ISessionContext;
    }
}
