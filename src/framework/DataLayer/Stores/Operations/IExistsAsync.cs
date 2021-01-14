using System.Threading.Tasks;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
  
    public interface IExistsAsync<TEntity, in TKey> where TEntity : class, IKey<TKey> {
      
        Task<bool> ExistsAsync( params TKey[] ids );
    }
}