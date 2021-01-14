using System;

namespace Orion.Framework.Domains.Auditing {
  
    public interface IModificationAudited : IModificationAudited<int?> {
    }

 
    public interface IModificationAudited<TKey> {
   
        DateTime? ChangedTime { get; set; }
 
        int? ChangedUser { get; set; }
    }
}
