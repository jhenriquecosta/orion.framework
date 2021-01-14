using System;
using Orion.Framework.Sessions;

namespace Orion.Framework.Domains.Auditing {

    public class CreationAuditedInitializer
    {
        
        private readonly object _entity;
       
        private readonly string _userId;
      
        private readonly string _userName;

      
        private CreationAuditedInitializer(object entity, string userId, string userName)
        {
            _entity = entity;
            _userId = userId;
            _userName = userName;
        }

       
        public static void Init(object entity, string userId, string userName)
        {
            new CreationAuditedInitializer(entity, userId, userName).Init();
        }

       
        public void Init()
        {
            if (_entity == null)
                return;
            InitCreationTime();
            InitCreator();
            if (string.IsNullOrWhiteSpace(_userId))
                return;
            if (_entity is ICreationAudited<Guid>)
            {
                InitGuid();
                return;
            }
            if (_entity is ICreationAudited<Guid?>)
            {
                InitNullableGuid();
                return;
            }
            if (_entity is ICreationAudited<int>)
            {
                InitInt();
                return;
            }
            if (_entity is ICreationAudited<int?>)
            {
                InitNullableInt();
                return;
            }
            if (_entity is ICreationAudited<string>)
            {
                InitString();
                return;
            }
            if (_entity is ICreationAudited<long>)
            {
                InitLong();
                return;
            }
            if (_entity is ICreationAudited<long?>)
            {
                InitNullableLong();
                return;
            }
        }

     
        private void InitCreationTime()
        {
            if (_entity is ICreationTime result)
                result.CreationTime = DateTime.Now;
        }

      
        private void InitCreator()
        {
            if (string.IsNullOrWhiteSpace(_userName))
                return;
            if (_entity is ICreator result)
                result.Creator = _userName;
        }

  
        private void InitGuid()
        {
            var result = (ICreationAudited<Guid>)_entity;
         //   result.CreatedUser = _userId.ToGuid();
        }

     
        private void InitNullableGuid()
        {
            var result = (ICreationAudited<Guid?>)_entity;
       //     result.CreatedUser = _userId.ToGuidOrNull();
        }

      
        private void InitInt()
        {
            var result = (ICreationAudited<int>)_entity;
            result.CreatedUser = _userId.ToInt();
        }

        
        private void InitNullableInt()
        {
            var result = (ICreationAudited<int?>)_entity;
         //   result.CreatedUser = _userId.ToIntOrNull();
        }

      
        private void InitString()
        {
            var result = (ICreationAudited<string>)_entity;
          //  result.CreatedUser = _userId.SafeString();
        }

       
        private void InitLong()
        {
            var result = (ICreationAudited<long>)_entity;
          //  result.CreatedUser = _userId.ToLong();
        }

    
        private void InitNullableLong()
        {
            var result = (ICreationAudited<long?>)_entity;
           // result.CreatedUser = _userId.ToLongOrNull();
        }
    }
}
