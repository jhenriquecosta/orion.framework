using NHibernate.Cfg;
using Orion.Framework.DataLayer.SessionContext;

namespace Orion.Framework.DataLayer.NH.Contracts
{
    public interface IDbBuildConfiguration
    {
        IDbBuildConfiguration Build();
        IDbBuildConfiguration WithContext(ISessionContext sessionContext);
        Configuration GetConfiguration();
       
    }
}
