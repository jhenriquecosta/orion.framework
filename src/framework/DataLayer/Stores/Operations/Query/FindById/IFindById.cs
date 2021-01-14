using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
   
    public interface IFindById<out TEntity, in TKey> where TEntity : class, IKey<TKey> 
    {
        TEntity Find(object id,bool detached=false);
        
    }
}
