using Framework.Domains;
using NHibernate.Engine;
using NHibernate.Event;
using NHibernate.Event.Default;
using NHibernate.Intercept;
using NHibernate.Persister.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Framework.DataLayer.NHibernate.Events.Listeners
{
    /// <summary>
    /// This runs just before transaction is committed to the DB
    /// </summary>
    internal class AuditLogEventListener : AuditLogEventListener<int>
    {
       
        public AuditLogEventListener(Func<Sessions.ISession> session) : base(session)
        {
         
        }
      
       

      
    }


    internal  class AuditLogEventListener<idT> : AuditLogEventListenerBase<idT>, IPreInsertEventListener, IPreUpdateEventListener where idT : IEquatable<idT>
    {

        private readonly Func<Sessions.ISession> _session;
        protected AuditLogEventListener(Func<Sessions.ISession> session) : base(session)
        {
            _session = session;
        }

        public bool OnPreInsert(PreInsertEvent @event)
        {
            var propertyNames = @event.Persister.PropertyNames;
            var state = @event.State;
            var entity = @event.Entity as IBaseEntity;
            var sessionImpl = @event.Session.GetSessionImplementation();
            object[] fields = @event.Persister.GetPropertyValues(entity);
            EntityEntry entry = sessionImpl.PersistenceContext.GetEntry(entity);

            //SetStateValue(propertyNames, state, "CreatedByUser", _session().UserId);
            //SetStateValue(propertyNames, state, "ChangedByUser", _session().UserId);
            //SetStateValue(propertyNames, state, "CreatedOn", DateTime.Now);
            //SetStateValue(propertyNames, state, "ChangedOn", DateTime.Now);
            entity.CreatedByUser = _session().UserId;
            entity.ChangedByUser = _session().UserId;
            entity.CreatedOn = DateTime.Now;
            entity.ChangedOn = DateTime.Now;


            if (typeof(IUnitOrganization).IsAssignableFrom(entity.GetType()))
            {
                //SetStateValue(propertyNames, state, "OrganizationCode", _session().OrganizationCode);
                var _entity = entity as IUnitOrganization;
                _entity.OrganizationCode = _session().OrganizationCode;



            }
            //SetStateValue(propertyNames, state, "OrganizationCode", XTAppSettings.OrganizationCode);
           // PackageAuditLogItem(EventType.Added, @event.Persister, entity, fields, entry.LoadedState) ;
            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent @event)
        {

            var propertyNames = @event.Persister.PropertyNames;
            var state = @event.State;

            SetStateValue(propertyNames, state, "ChangedByUser", _session().UserId);
            SetStateValue(propertyNames, state, "ChangedOn", DateTime.Now);
            return false;
        }

        private static void SetStateValue(string[] propertyNames,object[] state,string propertyName,object value)
        {
            int index = Array.IndexOf(propertyNames, propertyName);
            if (index == -1)
            {
                throw new AggregateException(string.Format("Property {0} not found.", propertyName));
            }

            state[index] = value;
        }

        public Task<bool> OnPreInsertAsync(PreInsertEvent @event, CancellationToken cancellationToken)
        {
            var result = OnPreInsert(@event);
            return Task.FromResult(result);
        }

        public Task<bool> OnPreUpdateAsync(PreUpdateEvent @event, CancellationToken cancellationToken)
        {
            var result = OnPreUpdate(@event);
            return Task.FromResult(result);
        }
    }

        /// <summary>
        /// This runs just before transaction is committed to the DB 
        /// </summary>
        /// <typeparam name="idT">The Id Type. I usually use long (Int64)</typeparam>
        internal abstract class AuditLogEventListenerBase<idT> : DefaultFlushEventListener,IMergeEventListener, IPreDeleteEventListener where idT : IEquatable<idT>
      {
        private HashSet<AuditLog> _auditLogItems { get; set; } = new HashSet<AuditLog>();
        private readonly Func<Sessions.ISession> _session;
        public AuditLogEventListenerBase(Func<Sessions.ISession> session)
        {
            _session = session;
        }

      


        public override void OnFlush(FlushEvent @event)
        {
            SaveAuditLogs(@event.Session);
            base.OnFlush(@event);
        }

        public bool OnPreDelete(PreDeleteEvent @event)
        {
            var entity = ValidateOblect(@event.Entity);
            if (entity != null)
            {
                var sessionImpl = @event.Session.GetSessionImplementation();
                EntityEntry entry = sessionImpl.PersistenceContext.GetEntry(entity);
                entry.Status = Status.Loaded;
                entity.IsDeleted = true;
                //object id = @event.Persister.GetIdentifier(entity, @event.Session.EntityMode);
                //object[] fields = @event.Persister.GetPropertyValues(entity, @event.Session.EntityMode);
                //object version = @event.Persister.GetVersion(entity, @event.Session.EntityMode);
                object id = @event.Persister.GetIdentifier(entity);
                object[] fields = @event.Persister.GetPropertyValues(entity);
                object version = @event.Persister.GetVersion(entity);

                @event.Persister.Update(id, fields, new int[1], false, fields, version, entity, null, sessionImpl);

                PackageAuditLogItem(EventType.SoftDeleted, @event.Persister, entity as IRootEntity<idT>, fields, entry.DeletedState);
            }
            return true;
        }

        public Task<bool> OnPreDeleteAsync(PreDeleteEvent @event, CancellationToken cancellationToken)
        {
            var result = OnPreDelete(@event);
            return Task.FromResult(result);
        }

        public void OnMerge(MergeEvent @event)
        {
            DoMerge(@event);
        }

        public void OnMerge(MergeEvent @event, IDictionary copiedAlready)
        {
            DoMerge(@event);
        }

        public Task OnMergeAsync(MergeEvent @event, CancellationToken cancellationToken)
        {
            OnMerge(@event);
            return Task.CompletedTask;
        }

        public Task OnMergeAsync(MergeEvent @event, IDictionary copiedAlready, CancellationToken cancellationToken)
        {
            OnMerge(@event, copiedAlready);
            return Task.CompletedTask;
        }
        private void DoMerge(MergeEvent @event)
        {
            var entity = ValidateOblect(@event.Original);
            if (entity != null)
            {
                var entityEntry = @event.Session.PersistenceContext.GetEntry(entity);
                if (entityEntry == null) return;
                object[] currentState;
                var modified = IsEntityModified(entityEntry, entity, @event.Session, out currentState);
                if (modified)
                {
                    PackageAuditLogItem(EventType.Modified, entityEntry.Persister, entity as IRootEntity<idT>, currentState, entityEntry.LoadedState);
                }
            }
        }

        private IRootEntity ValidateOblect(object obj)
        {
            var entity = obj as IRootEntity;
            if (entity == null) return null;
          
            if (typeof(IDoNotNeedAudit).IsAssignableFrom(entity.GetType())) return null;

            if (entity.OrganizationCode == null)
            {
                entity.OrganizationCode = 0;
            }
            return entity;
        }

        protected void PackageAuditLogItem(EventType eventType, IEntityPersister persister,object entity, object[] currentValues, object[] oldValues)
        {
            //if (currentValues == null) return;
            //var _entity = entity as IBaseEntity;
            //_entity.ChangedOn = DateTime.Now.GetLocalTime();
            //var orgCode = 0;
            //if (typeof(IUnitOrganization).IsAssignableFrom(entity.GetType()))
            //{
            //    orgCode = (int)_session().OrganizationCode;
            //}
            //var audit = CreateLogRecord(orgCode, _session().UserId, entity, _entity.ChangedOn, eventType);
            //if (audit != null)
            //{
            //    audit.AuditData = AuditLogSerializer.SerializeData(persister, currentValues, oldValues);
            //    _auditLogItems.Add(audit);
            //}
        }


        /// <summary>
        /// This packages the auditlog entity for you. After calling this, you should then set the SerializedData property.
        /// Since it's DB implementation-dependent, it's not done here.
        /// </summary>
        /// <param name="instCode"></param>
        /// <param name="user"></param>
        /// <param name="ent"></param>
        /// <param name="changeTime"></param>
        /// <param name="eventType"></param>
        /// <returns></returns>
        private AuditLog CreateLogRecord(int? instCode, string user, object ent, DateTime? changeTime, EventType eventType)
        {
            var _ent = ent as IEntity<int>;
            var auditLog = new AuditLog
            {
                EventType = eventType,
                EventDate = changeTime.To<DateTime>(),
                EntityId = _ent.Id.ToString(),
                OrganizationCode = instCode,
            };
            auditLog.Entity = ent.GetType().Name.AsSplitPascalCasedString();

            if (user != null)
            {
                auditLog.UserId = user;
                auditLog.UserName = user;
            }
            try
            {
                auditLog.ClientIpAddress = WebHttp.Ip; //Current?.Request?.UserHostAddress;
                auditLog.ClientName = WebHttp.Host;  //HttpContext.Current?.Request?.UserHostName;
            }
            catch (Exception)
            {
                auditLog.ClientName = "[Could not resolve Client Name]";
            }

            auditLog.ApplicationName = XTAppSettings.Application;  //ConfigurationHelper.SectionItem<string>("ClientConfiguration", "ApplicationName");

            return auditLog;
        }

        private void SaveAuditLogs(IEventSource session)
        {
            if (_auditLogItems.Count == 0) return;

            //foreach (var audit in _auditLogItems)
            //{
            //    session.Save(audit);
            //}
            _auditLogItems.Clear();
        }

        private bool IsEntityModified(EntityEntry entry, object entity, ISessionImplementor session, out object[] currentState)
        {
            currentState = null;
            if (entry.Status != Status.Loaded) return false;
            if (!entry.ExistsInDatabase) return false;
            if (entry.LoadedState == null) return false;

            if (!entry.RequiresDirtyCheck(entity)) return false;

            IEntityPersister persister = entry.Persister;

            //var currState = currentState = persister.GetPropertyValues(entity, session.EntityMode);
            var currState = currentState = persister.GetPropertyValues(entity);
            object[] loadedState = entry.LoadedState;

            return persister.EntityMetamodel.Properties
                .Where((property, i) =>
                {
                    return !LazyPropertyInitializer.UnfetchedProperty.Equals(currState[i])
                            && property.Type.IsDirty(loadedState[i], currState[i], session);
                })
                .Any();
        }

     
    }
}
