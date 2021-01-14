using System;
using System.Linq.Expressions;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Stores.Operations {
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface ICount<TEntity, in TKey> where TEntity : class, IKey<TKey> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        int Count( Expression<Func<TEntity, bool>> predicate = null );
    }
}