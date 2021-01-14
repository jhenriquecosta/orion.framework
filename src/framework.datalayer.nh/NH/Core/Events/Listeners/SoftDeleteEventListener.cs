using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.NH.Events.Listeners
{
    public class SoftDeleteEventListener :  DefaultDeleteEventListener
    {
        private readonly Func<Sessions.ISession> _session;
        public SoftDeleteEventListener(Func<Sessions.ISession> session)
        {
            _session = session;
        }

        protected override Task DeleteEntityAsync(
        IEventSource session, object entity,
        EntityEntry entityEntry,
        bool isCascadeDeleteEnabled,
        IEntityPersister persister,
        ISet<object> transientEntities, CancellationToken cancellationToken)
        {
            DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);
            return Task.CompletedTask;
        }

        protected override void DeleteEntity(
        IEventSource session, object entity,
        EntityEntry entityEntry,
        bool isCascadeDeleteEnabled,
        IEntityPersister persister,
        ISet<object> transientEntities)
        {
            var entityBase = entity.GetType();
            var isDeleteble = typeof(IDeletedAudited).IsAssignableFrom(entityBase);
            if (isDeleteble)
            {
                var e = entity as ISoftDelete;

                e.IsDeleted = true;

                var changes = entity as IDeletedAudited;

                changes.DeletedOn = DateTime.UtcNow;
                changes.DeletedUser = _session().UserId;

                CascadeBeforeDelete(
                session,
                persister,
                e,
                entityEntry,
                transientEntities);
                CascadeAfterDelete(
                session,
                persister,
                e,
                transientEntities);
            }
            else
            {
                base.DeleteEntity(
                session,
                entity,
                entityEntry,
                isCascadeDeleteEnabled,
                persister,
                transientEntities);
            }
        }
    }
}
