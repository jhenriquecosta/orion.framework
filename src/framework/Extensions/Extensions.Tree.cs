using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.Domains.Trees;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions 
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="entity"></param>
        public static async Task UpdatePathAsync<TEntity, TKey, TParentId>( this ITreeCompactRepository<TEntity, TKey, TParentId> repository, TEntity entity )
            where TEntity : class, ITreeEntity<TEntity, TKey, TParentId> {
            var manager = new UpdatePathManager<TEntity, TKey, TParentId>( repository );
            await manager.UpdatePathAsync( entity );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">实体</>
        /// <param name="swapEntity"></param>
        public static void SwapSort( this ISortId entity, ISortId swapEntity ) {
            var sortId = entity.SortId;
            entity.SortId = swapEntity.SortId;
            swapEntity.SortId = sortId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TParentId"></typeparam>
        /// <param name="entities"></param>
        public static List<string> GetMissingParentIds<TEntity,TKey,TParentId>( this IEnumerable<TEntity> entities ) where TEntity : class, ITreeEntity<TEntity, TKey, TParentId> {
            var result = new List<string>();
            if ( entities == null )
                return result;
            var list = entities.ToList();
            list.ForEach( entity => {
                if ( entity == null )
                    return;
                result.AddRange( entity.GetParentIdsFromPath().Select( t => t.SafeString() ) );
            } );
            var ids = list.Select( t => t?.Id.SafeString() );
            return result.Except( ids ).ToList();
        }
    }
}
