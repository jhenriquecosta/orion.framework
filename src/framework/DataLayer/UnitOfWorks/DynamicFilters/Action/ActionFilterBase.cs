using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using Orion.Framework.DataLayer.UnitOfWorks.Contracts;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using Orion.Framework.Helpers;
using Orion.Framework.Sessions;
using Orion.Framework.Utilities;

namespace Orion.Framework.DataLayer.UnitOfWorks.DynamicFilters.Action
{
    public abstract class ActionFilterBase : ITransientDependency
    {
        protected ActionFilterBase()
        {
            AppSession = NullSession.Instance;
            GuidGenerator = SequentialGuidGenerator.Instance;
        }

        public Sessions.ISession AppSession { get; set; }

        public ICurrentUnitOfWorkProvider CurrentUnitOfWorkProvider { get; set; }

        public IGuidGenerator GuidGenerator { get; set; }

        protected virtual string GetAuditUserId()
        {
            if (!@AppSession.UserId.IsNullOrEmpty() && CurrentUnitOfWorkProvider?.Current != null)
            {
                return AppSession.UserId;
            }

            return null;
        }

        public abstract void ExecuteFilter<TEntity, TPrimaryKey>(TEntity entity) where TEntity : class, IEntity<TPrimaryKey>;

        protected virtual void CheckAndSetId(object entityAsObj)
        {
            var entity = entityAsObj as IEntity<Guid>;
            if (entity != null && entity.Id == Guid.Empty)
            {
                Type entityType = entityAsObj.GetType();
                PropertyInfo idProperty = entityType.GetProperty("Id");
                var dbGeneratedAttr = ReflectionHelper.GetSingleAttributeOrDefault<DatabaseGeneratedAttribute>(idProperty);
                if (dbGeneratedAttr == null || dbGeneratedAttr.DatabaseGeneratedOption == DatabaseGeneratedOption.None)
                {
                    //entity = GuidGenerator.Create();
                }
            }
        }
    }
}
