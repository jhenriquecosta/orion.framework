#region License
// Open Behavioral Health Information Technology Architecture (OBHITA.org)
// 
// Redistribution and use in source and binary forms, with or without
// modification, are permitted provided that the following conditions are met:
//     * Redistributions of source code must retain the above copyright
//       notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright
//       notice, this list of conditions and the following disclaimer in the
//       documentation and/or other materials provided with the distribution.
//     * Neither the name of the <organization> nor the
//       names of its contributors may be used to endorse or promote products
//       derived from this software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL <COPYRIGHT HOLDER> BE LIABLE FOR ANY
// DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
// (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
// ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
// (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
#endregion

using System;
using Orion.Framework.Domains;
using Orion.Framework.Settings;
using NHibernate;
using NHibernate.SqlCommand;
using NHibernate.Type;
using NLog;

namespace Orion.Framework.DataLayer.NH.Events.Interceptors
{
    /// <summary>
    /// The <see cref="T:Rem.Infrastructure.Domain.Interceptor.AuditableInterceptor"/> contains common functionalities for auditable interceptor.
    /// </summary>
    [Serializable]
    public class AuditableInterceptor : EmptyInterceptor
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly Func<Sessions.ISession> _session;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Rem.Infrastructure.Domain.Interceptor.AuditableInterceptor"/> class.
        /// </summary>
        /// <param name="session">The system account provider.</param>
        public AuditableInterceptor (Func<Sessions.ISession> session)
        {
            _session = session;
        }

        public override void OnDelete(object entity,
                                 object id,
                                 object[] state,
                                 string[] propertyNames,
                                 IType[] types)
        {
            var entityBase = entity.GetType();
            var isDeleted = typeof(IDeletedAudited).IsAssignableFrom(entityBase); // entityBase.IsParentOf(typeof(IAudited));  
            if (isDeleted)
            {

            }
        }
        /// <summary>
        /// Called when [save].
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <param name="id">The id of the entity.</param>
        /// <param name="state">The state information.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="types">The type information.</param>
        /// <returns><c>true</c> if it is an auditable entity.</returns>
        public override bool OnSave (
            object entity,
            object id,
            object[] state,
            string[] propertyNames,
            IType[] types )
        {
            bool entityModified = false;


            var entityBase = entity.GetType();
            var isAudited = typeof(IAudited).IsAssignableFrom(entityBase); // entityBase.IsParentOf(typeof(IAudited));  
            if (isAudited )
            {

                //  SystemAccount systemAccount = GetSystemAccount ();
                //savingEntity.CreatedOn = DateTime.Now;
                //savingEntity.CreatedByUser = _session().UserId;
                //savingEntity.ChangedOn = DateTime.Now;
                //savingEntity.ChangedByUser = _session().UserId;
                //savingEntity.OrganizationCode = _session().OrganizationCode;
                //   var dateTime = DateTime.Now;
                var dateTime = DateTime.UtcNow;
                var userId = _session().UserId;
                var orgId = _session().OrganizationCode;
                SetStateValue ( propertyNames, state, "CreatedOn", dateTime );
                SetStateValue ( propertyNames, state, "ChangedOn", dateTime );
                SetStateValue ( propertyNames, state, "CreatedUser", _session().UserId);
                SetStateValue ( propertyNames, state, "ChangedUser",  _session().UserId);
                entityModified = true;
            }
            var isOrg = typeof(IUnitOrganization).IsAssignableFrom(entityBase);
            if (isOrg)
            {
                var orgId = _session().OrganizationCode;
                SetStateValue(propertyNames, state, "OrganizationCode", orgId);
                entityModified = true;
            }

            return entityModified;
        }

        /// <summary>
        /// Called when [flush dirty].
        /// </summary>
        /// <param name="entity">The entity to be saved.</param>
        /// <param name="id">The id of the entity.</param>
        /// <param name="currentState">State of the current.</param>
        /// <param name="previousState">State of the previous.</param>
        /// <param name="propertyNames">The property names.</param>
        /// <param name="types">The type information.</param>
        /// <returns><c>true</c> if the entity is modified.</returns>
        public override bool OnFlushDirty (
            object entity,
            object id,
            object[] currentState,
            object[] previousState,
            string[] propertyNames,
            IType[] types )
        {
            bool entityModified = false;


            //if ( entity is IFullAudited)
            var entityBase = entity.GetType();
            var isAudited = typeof(IAudited).IsAssignableFrom(entityBase); // entityBase.IsParentOf(typeof(IAudited));  
            if (isAudited)
            {


                var dateTime = DateTime.UtcNow;

                SetStateValue ( propertyNames, currentState, "ChangedOn", dateTime );
                SetStateValue ( propertyNames, currentState, "ChangedUser", _session().UserId);
                entityModified = true;
            }

            return entityModified;
        }

        private static void SetStateValue (
            string[] propertyNames,
            object[] state,
            string propertyName,
            object value )
        {
            int index = Array.IndexOf ( propertyNames, propertyName );
            if ( index == -1 )
            {
                throw new AggregateException ( string.Format ( "Property {0} not found.", propertyName ) );
            }

            state[ index ] = value;
        }
      
        public override SqlString OnPrepareStatement(SqlString sql)
        {

          
            //if (options != null)
            //{
            //    if (options.LogDebugSql.ToBool())
            //    {
            //        var log = $"NHibernate {sql}";
            //        Logger.Trace(log);
            //    }
            //}
             return base.OnPrepareStatement(sql);
            
        }
    
        //private User GetSystemAccount()
        //{
        //    var account = _session().SystemAccount;
        //    if (account == null)
        //    {
        //        throw new SystemException ( "The system account is missing from SystemAccountProvider." );
        //    }

        //    return account;
        //}
    }
}