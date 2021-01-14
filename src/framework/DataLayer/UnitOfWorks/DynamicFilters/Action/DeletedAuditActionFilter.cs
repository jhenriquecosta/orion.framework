using Orion.Framework.Domains;
using Orion.Framework.Timing;

namespace Orion.Framework.DataLayer.UnitOfWorks.DynamicFilters.Action
{
    public class DeletedAuditActionFilter : ActionFilterBase
    {
        public override void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity)
        {
            var userId = GetAuditUserId();
            if (entity is ISoftDelete)
            {
                var record = entity.As<ISoftDelete>();
                record.IsDeleted = true;
            }
            if (entity is IDeletedAudited changed)
            {
                changed.DeletedOn = Clock.Now;
                changed.DeletedUser = userId;
            }

        }
    }
}
