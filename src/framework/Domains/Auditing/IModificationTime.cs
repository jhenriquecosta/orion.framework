using System;

namespace Orion.Framework.Domains.Auditing {

    public interface IModificationTime {
      
        DateTime? LastModificationTime { get; set; }
    }
}
