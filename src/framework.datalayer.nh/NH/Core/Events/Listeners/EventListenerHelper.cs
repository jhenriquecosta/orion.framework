using System;
using Zeus.Domains;
using Zeus.Security.Principals;

namespace Zeus.NHibernate.Events.Listeners
{
    public class EventListenerHelper
    {
        private readonly Func<Sessions.ISession> _session;
        public EventListenerHelper(Func<Sessions.ISession> session) 
        {
            _session = session;
        }
        //private readonly IPrincipalProvider principalProvider;    
        //public EventListenerHelper(IPrincipalProvider principalProvider)
        //{
        //    this.principalProvider = principalProvider;
        //}

        /// <summary>
        /// Called when modifying entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnModify(object entity)
        {
            var savingEntity = entity as IBaseEntity;
            if (savingEntity != null)
            {
                savingEntity.ChangedOn = DateTime.Now;
                savingEntity.ChangedByUser = _session().UserId;
               

            }
        }

        /// <summary>
        /// Called when creating entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnCreate(object entity)
        {
            var savingEntity = entity as IBaseEntity;
            if (savingEntity != null)
            {
                savingEntity.CreatedOn = DateTime.Now;
                savingEntity.CreatedByUser = _session().UserId; 
                savingEntity.ChangedOn = DateTime.Now;
                savingEntity.ChangedByUser  = _session().UserId;
                savingEntity.OrganizationCode = _session().OrganizationCode;
            }
        }

        /// <summary>
        /// Called when deleting entity.
        /// </summary>
        /// <param name="entity">The entity.</param>
        public void OnDelete(object entity)
        {
            var deletingEntity = entity as IBaseEntity;
            if (deletingEntity != null)
            {
                deletingEntity.IsDeleted = true;
                deletingEntity.DeletedOn = DateTime.Now;
                deletingEntity.DeletedByUser = _session().UserId;
            }
        }
    }
}
