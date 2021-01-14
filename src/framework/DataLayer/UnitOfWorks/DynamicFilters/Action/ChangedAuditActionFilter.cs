using Orion.Framework.Domains;
using Orion.Framework.Timing;

namespace Orion.Framework.DataLayer.UnitOfWorks.DynamicFilters.Action
{
    public class ChangedAuditActionFilter : ActionFilterBase
    {
        public override void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity)
        {

            var userId = GetAuditUserId();
            CheckAndSetId(entity);
            if (!(entity is IChangedAudited entityWithCreationTime))
            {
                return;
            }         
            if (entity is IChangedAudited changed)
            {
                changed.ChangedOn = Clock.Now;
                changed.ChangedUser = userId;
            }
        }
    }
}
