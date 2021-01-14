using NHibernate;
using NHibernate.Type;
using Orion.Framework.Dependency;
using Orion.Framework.Domains;
using Orion.Framework.Sessions;
using Orion.Framework.Timing;
using System;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.NH.Events.Interceptors
{
    internal class AuditInterceptors : EmptyInterceptor, ITransientDependency
    {
        //private readonly Lazy<IOrionCommandContextAccessor> _commandContextAccessor;
        //private readonly Lazy<IEventBus> _eventBus;
        //private readonly Lazy<IGuidGenerator> _guidGenerator;
        private readonly Lazy<Sessions.ISession> _appSession;

        public AuditInterceptors()
        {

            var session = AppHelper.GetService<Sessions.ISession>();           
            _appSession = new Lazy<Sessions.ISession>(() => session ?? NullSession.Instance,true);
            var x = _appSession.Value;
        }

        public override bool OnSave(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            //Set Id for Guids
            if (entity is IEntity<Guid>)
            {
                var guidEntity = entity as IEntity<Guid>;
                if (guidEntity.IsTransient())
                {
                    //guidEntity.Id = _guidGenerator.Value.Create();
                }
            }

            //Set CreationTime for new entity
            if (entity is ICreatedAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "CreatedOn")
                    {
                        state[i] = (entity as ICreatedAudited).CreatedOn = Clock.Now;
                    }
                    if (propertyNames[i] == "CreatedUser")
                    {
                        state[i] = (entity as ICreatedAudited).CreatedUser = _appSession.Value.UserId;
                    }
                }
            }

            TriggerDomainEvents(entity);

            return base.OnSave(entity, id, state, propertyNames, types);
        }

        public override bool OnFlushDirty(object entity, object id, object[] currentState, object[] previousState, string[] propertyNames, IType[] types)
        {
            //Set modification audits
            if (entity is IChangedAudited)
            {
                for (var i = 0; i < propertyNames.Length; i++)
                {
                    if (propertyNames[i] == "ChangedOn")
                    {
                        currentState[i] = (entity as IChangedAudited).ChangedOn = Clock.Now;
                    }
                    if (propertyNames[i] == "ChangedUser")
                    {
                        currentState[i] = (entity as IChangedAudited).ChangedUser = _appSession.Value.UserId;
                    }
                }
            }

            if (entity is ISoftDelete && entity.As<ISoftDelete>().IsDeleted)
            {
                //Is deleted before? Normally, a deleted entity should not be updated later but I preferred to check it.
                var previousIsDeleted = false;
                if (previousState != null)
                {
                    for (var i = 0; i < propertyNames.Length; i++)
                    {
                        if (propertyNames[i] == "IsDeleted")
                        {

                            previousIsDeleted = (bool)previousState[i];
                            break;

                        }
                    }
                }

                if (!previousIsDeleted)
                {
                    //set DeletionTime
                    for (var i = 0; i < propertyNames.Length; i++)
                    {
                        if (propertyNames[i] == "DeletedOn")
                        {
                            currentState[i] = (entity as IDeletedAudited).DeletedOn = Clock.Now;
                        }
                        if (propertyNames[i] == "DeletedUser")
                        {
                            currentState[i] = (entity as IDeletedAudited).DeletedUser = _appSession.Value.UserId;
                        }
                    }

                }
            }

            TriggerDomainEvents(entity);

            return base.OnFlushDirty(entity, id, currentState, previousState, propertyNames, types);
        }

        public override void OnDelete(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            TriggerDomainEvents(entity);

            base.OnDelete(entity, id, state, propertyNames, types);
        }

        public override bool OnLoad(object entity, object id, object[] state, string[] propertyNames, IType[] types)
        {
            NormalizeDateTimePropertiesForEntity(state, types);
            return true;
        }

        private static void NormalizeDateTimePropertiesForEntity(object[] state, IList<IType> types)
        {
            for (var i = 0; i < types.Count; i++)
            {
                if (types[i].IsComponentType)
                {
                    NormalizeDateTimePropertiesForComponentType(state[i], types[i]);
                }

                if (types[i].ReturnedClass != typeof(DateTime) && types[i].ReturnedClass != typeof(DateTime?))
                {
                    continue;
                }

                if (!(state[i] is DateTime dateTime))
                {
                    continue;
                }

                state[i] = Clock.Normalize(dateTime);
            }
        }

        private static void NormalizeDateTimePropertiesForComponentType(object componentObject, IType type)
        {
            if (type is ComponentType componentType)
            {
                for (var i = 0; i < componentType.PropertyNames.Length; i++)
                {
                    string propertyName = componentType.PropertyNames[i];
                    if (componentType.Subtypes[i].IsComponentType)
                    {
                        object value = componentObject.GetType().GetProperty(propertyName)?.GetValue(componentObject, null);
                        NormalizeDateTimePropertiesForComponentType(value, componentType.Subtypes[i]);
                    }

                    if (componentType.Subtypes[i].ReturnedClass != typeof(DateTime) && componentType.Subtypes[i].ReturnedClass != typeof(DateTime?))
                    {
                        continue;
                    }

                    var dateTime = componentObject.GetType().GetProperty(propertyName)?.GetValue(componentObject) as DateTime?;

                    if (!dateTime.HasValue)
                    {
                        continue;
                    }

                    componentObject.GetType().GetProperty(propertyName)?.SetValue(componentObject, Clock.Normalize(dateTime.Value));
                }
            }
        }

        protected virtual void TriggerDomainEvents(object entity)
        {
            return;
            //if (!(entity is IAggregateChangeTracker aggregateChangeTracker))
            //{
            //    return;
            //}

            //if (aggregateChangeTracker.GetChanges().IsNullOrEmpty())
            //{
            //    return;
            //}

            //List<object> domainEvents = aggregateChangeTracker.GetChanges().ToList();
            //aggregateChangeTracker.ClearChanges();

            //foreach (object @event in domainEvents)
            //{
            //    _eventBus.Value.Publish(
            //        @event.GetType(),
            //        (IEvent)@event,
            //        new Headers()
            //        {
            //            [XTConstants.Events.CausationId] = _commandContextAccessor.Value.GetCorrelationIdOrEmpty(),
            //            [XTConstants.Events.UserId] = _appSession.Value.UserId,
            //            [XTConstants.Events.SourceType] = entity.GetType().FullName,
            //            [XTConstants.Events.QualifiedName] = @event.GetType().AssemblyQualifiedName,
            //            [XTConstants.Events.AggregateId] = ((dynamic)entity).Id
            //        });
            //}
        }
    }
}
