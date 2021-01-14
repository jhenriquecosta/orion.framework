using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IExists<TEntity, in TKey> where TEntity : class, IKey<TKey> {
   
        bool Exists( params TKey[] ids );
    }
}