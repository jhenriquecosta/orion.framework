using System;
using Orion.Framework.Sessions;

namespace Orion.Framework.Domains.Auditing {
   
    public class ModificationAuditedInitializer {

        private readonly object _entity;
        
        private readonly string _userId;
        
        private readonly string _userName;

        private ModificationAuditedInitializer(object entity, string userId, string userName)
        {
            _entity = entity;
            _userId = userId;
            _userName = userName;
        }
        public static void Init(object entity, string userId, string userName)
        {
            new ModificationAuditedInitializer(entity, userId, userName).Init();
        }



        public void Init() {
            if( _entity is IModificationAudited<Guid>) {
                InitGuid();
                return;
            }
            if ( _entity is IModificationAudited<Guid?> ) {
                InitNullableGuid();
                return;
            }
            if ( _entity is IModificationAudited<int> ) {
                InitInt();
                return;
            }
            if ( _entity is IModificationAudited<int?> ) {
                InitNullableInt();
                return;
            }
            if ( _entity is IModificationAudited<string> ) {
                InitString();
                return;
            }
            if ( _entity is IModificationAudited<long> ) {
                InitLong();
                return;
            }
            if ( _entity is IModificationAudited<long?> ) {
                InitNullableLong();
                return;
            }
        }
        private void InitLastModificationTime()
        {
            if (_entity is IModificationTime result)
                result.LastModificationTime = DateTime.Now;
        }

      
        private void InitModifier()
        {
            if (string.IsNullOrWhiteSpace(_userName))
                return;
            if (_entity is IModifier result)
                result.Modifier = _userName;
        }

        private void InitGuid() {
            var result = (IModificationAudited<Guid>)_entity;
            result.ChangedTime = DateTime.Now;
    //        result.ChangedUser =_userId.ToGuid();
        }

      
        private void InitNullableGuid() {
            var result = (IModificationAudited<Guid?>)_entity;
            result.ChangedTime = DateTime.Now;
          //  result.ChangedUser =_userId.ToGuidOrNull();
        }

      
        private void InitInt() {
            var result = (IModificationAudited<int>)_entity;
            result.ChangedTime = DateTime.Now;
        //    result.ChangedUser =_userId.ToInt();
        }

     
        private void InitNullableInt() {
            var result = (IModificationAudited<int?>)_entity;
            result.ChangedTime = DateTime.Now;
        //    result.ChangedUser =_userId.ToIntOrNull();
        }

    
        private void InitString() {
            var result = (IModificationAudited<string>)_entity;
            result.ChangedTime = DateTime.Now;
          //  result.ChangedUser =_userId.SafeString();
        }

       
        private void InitLong() {
            var result = (IModificationAudited<long>)_entity;
            result.ChangedTime = DateTime.Now;
        //    result.ChangedUser =_userId.ToLong();
        }

       
        private void InitNullableLong() {
            var result = (IModificationAudited<long?>)_entity;
            result.ChangedTime = DateTime.Now;
          //  result.ChangedUser =_userId.ToLongOrNull();
        }
    }
}
