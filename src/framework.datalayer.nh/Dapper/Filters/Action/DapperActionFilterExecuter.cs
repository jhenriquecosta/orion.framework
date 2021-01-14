using Orion.Framework.DataLayer.UnitOfWorks.DynamicFilters.Action;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Dapper.Filters.Action
{
    public class DapperActionFilterExecuter : IDapperActionFilterExecuter, ITransientDependency
    {
        //private readonly IScopeResolver _scopeResolver;

        public DapperActionFilterExecuter()
        {
             
        }

        public void ExecuteCreationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            var filter = Ioc.Create<CreatedAuditActionFilter>();
            filter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteModificationAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            var filter = Ioc.Create<ChangedAuditActionFilter>();
            filter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }

        public void ExecuteDeletionAuditFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>
        {
            var filter = Ioc.Create<DeletedAuditActionFilter>();
            filter.ExecuteFilter<TEntity, TPrimaryKey>(entity);
        }
    }
}
