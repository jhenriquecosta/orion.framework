using System;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Persistence {
 
    public abstract class PersistentObjectBase : PersistentObjectBase<int> {
    }

   
    public abstract class PersistentObjectBase<TKey> : PersistentEntityBase<TKey>, IVersion {

        public int Version { get; set; }
    }
}
