using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Repositories
{
    public class DapperNhRepositoryBase<TSessionContext, TEntity> : DapperNhRepositoryBase<TSessionContext, TEntity, int>, IDapperRepository<TEntity>
        where TEntity : class, IEntity<int>
        where TSessionContext : ISessionContext
    {
        public DapperNhRepositoryBase(IActiveTransactionProvider activeTransactionProvider) : base(activeTransactionProvider)
        {
        }
    }
}
