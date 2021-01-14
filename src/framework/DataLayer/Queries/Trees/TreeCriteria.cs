using System;
using System.Linq.Expressions;

using Orion.Framework.Domains.Repositories;
using Orion.Framework.Domains.Trees;

namespace Orion.Framework.DataLayer.Queries.Trees {
   
    public class TreeCriteria<TEntity> : TreeCriteria<TEntity, Guid?> where TEntity : IPath, IEnabled, IParentId<Guid?> {
      
        public TreeCriteria( ITreeQueryParameter parameter ) : base( parameter ) {
            if( parameter.ParentId != null )
                Predicate = Predicate.And( t => t.ParentId == parameter.ParentId );
        }
    }

   
    public class TreeCriteria<TEntity, TParentId> : ICriteria<TEntity> where TEntity : IPath, IEnabled {
      
        public TreeCriteria( ITreeQueryParameter<TParentId> parameter ) {
            if( !string.IsNullOrWhiteSpace( parameter.Path ) )
                Predicate = Predicate.And( t => t.Path.StartsWith( parameter.Path ) );
            if( parameter.Level != null )
                Predicate = Predicate.And( t => t.Level == parameter.Level );
            if( parameter.Enabled != null )
                Predicate = Predicate.And( t => t.Enabled == parameter.Enabled );
        }

        protected Expression<Func<TEntity, bool>> Predicate { get; set; }

        
        public Expression<Func<TEntity, bool>> GetPredicate() {
            return Predicate;
        }
    }
}
