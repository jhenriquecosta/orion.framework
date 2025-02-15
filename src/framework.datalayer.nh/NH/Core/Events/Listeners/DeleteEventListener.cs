﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Persister.Entity;
using Zeus.Domains;

namespace Zeus.NHibernate.Events.Listeners
{
    /// <summary>
    /// nHibernate Delete Event Listener
    /// </summary>
    public class DeleteEventListener : DefaultDeleteEventListener
    {
        /// <summary>
        /// Event listener helper
        /// </summary>
        private readonly EventListenerHelper eventListenerHelper;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteEventListener" /> class.
        /// </summary>
        /// <param name="eventListenerHelper">The event listener helper.</param>
        public DeleteEventListener(EventListenerHelper eventListenerHelper)
        {
            this.eventListenerHelper = eventListenerHelper;
        }

        /// <summary>
        /// Perform the entity deletion.  Well, as with most operations, does not
        /// really perform it; just schedules an action/execution with the
        /// <see cref="T:NHibernate.Engine.ActionQueue" /> for execution during flush.
        /// </summary>
        /// <param name="session">The originating session</param>
        /// <param name="entity">The entity to delete</param>
        /// <param name="entityEntry">The entity's entry in the <see cref="T:NHibernate.ISession" /></param>
        /// <param name="isCascadeDeleteEnabled">Is delete cascading enabled?</param>
        /// <param name="persister">The entity persister.</param>
        /// <param name="transientEntities">A cache of already deleted entities.</param>
        protected override void DeleteEntity(IEventSource session, object entity, EntityEntry entityEntry, bool isCascadeDeleteEnabled, IEntityPersister persister, ISet<object> transientEntities)
        {
            if (entity is IEntity)
            {
                Events.CoreEvents.Instance.OnEntityDelete((IEntity)entity);
            }
            var IsDeletable = entity.GetType().IsImplementationOf(typeof(IDelete));
            if (entity is IEntity && (IsDeletable))
            {
                eventListenerHelper.OnDelete(entity);

                CascadeBeforeDelete(session, persister, entity, entityEntry, transientEntities);
                CascadeAfterDelete(session, persister, entity, transientEntities);
            }
            else
            {
                base.DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);
            }
        }
        protected override Task DeleteEntityAsync(IEventSource session, object entity, EntityEntry entityEntry, bool isCascadeDeleteEnabled, IEntityPersister persister, ISet<object> transientEntities, CancellationToken cancellationToken)
        {
            DeleteEntity(session, entity, entityEntry, isCascadeDeleteEnabled, persister, transientEntities);
            return Task.CompletedTask;
        }
    }
}
