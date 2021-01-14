using System.Collections.Generic;
using System.Linq;
using Orion.Framework.Infrastructurelications.Trees;

namespace Orion.Framework {
  
    public static partial class Extensions {
      
        public static List<string> GetParentIdsFromPath( this ITreeNode node, bool excludeSelf = true ) {
            if( node == null || node.Path.IsEmpty() )
                return new List<string>();
            var result = node.Path.Split( ',' ).Where( id => !string.IsNullOrWhiteSpace( id ) && id != "," ).ToList();
            if( excludeSelf )
                result = result.Where( id => id.SafeString().ToLower() != node.Id.SafeString().ToLower() ).ToList();
            return result;
        }

     
        public static List<string> GetMissingParentIds<TEntity>( this IEnumerable<TEntity> entities ) where TEntity : class,ITreeNode {
            var result = new List<string>();
            if( entities == null )
                return result;
            var list = entities.ToList();
            list.ForEach( entity => {
                if( entity == null )
                    return;
                result.AddRange( entity.GetParentIdsFromPath().Select( t => t.SafeString() ) );
            } );
            var ids = list.Select( t => t?.Id.SafeString() );
            return result.Except( ids ).ToList();
        }
    }
}
