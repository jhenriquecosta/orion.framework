using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IFindByIdNoTracking<out TEntity, in TKey> where TEntity : class, IKey<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        TEntity FindNoTracking( TKey id );
    }
}