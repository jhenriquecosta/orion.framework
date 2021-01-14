using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Orion.Framework.Exceptions;
using Orion.Framework.Properties;

namespace Orion.Framework.Domains.Trees {
    
    public class UpdatePathManager<TEntity, TKey, TParentId>
        where TEntity : class, ITreeEntity<TEntity, TKey, TParentId> {
      
        private readonly ITreeCompactRepository<TEntity, TKey, TParentId> _repository;

     
        public UpdatePathManager( ITreeCompactRepository<TEntity, TKey, TParentId> repository ) {
            _repository = repository;
        }

   
        public async Task UpdatePathAsync( TEntity entity ) {
            entity.CheckNull( nameof( entity ) );
            if( entity.ParentId.Equals( entity.Id ) )
                throw new Warning( LibraryResource.NotSupportMoveToChildren );
            var old = await _repository.FindAsync( (object)entity.Id );
            if( old == null )
                return;
            if( entity.ParentId.Equals( old.ParentId ) )
                return;
            var children = await _repository.GetAllChildrenAsync( entity );
            if( children.Exists( t => t.Id.Equals( entity.ParentId ) ) )
                throw new Warning( LibraryResource.NotSupportMoveToChildren );
            var parent = await _repository.FindAsync ( entity.ParentId );
            entity.InitPath( parent );
            await UpdateChildrenPath( entity, children );
            await _repository.UpdateAsync( children );
        }

      
        private async Task UpdateChildrenPath( TEntity parent, List<TEntity> children ) {
            if( parent == null || children == null )
                return;
            var list = children.Where( t => t.ParentId.Equals( parent.Id ) ).ToList();
            foreach( var child in list ) {
                child.InitPath( parent );
                await UpdateChildrenPath( child, children );
            }
        }
    }
}
