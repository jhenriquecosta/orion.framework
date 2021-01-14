using System;

namespace Orion.Framework.Domains.Auditing {
   
    public interface ICreationAudited : ICreationAudited<int?> {
    }

   
    public interface ICreationAudited<TKey>
    {
       
        DateTime? CreatedTime { get; set; }
     
        int? CreatedUser { get; set; }
    }
}
