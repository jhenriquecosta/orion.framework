using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Orion.Framework.Domains.Trees
{
   
   
    public abstract class TreeEntityBase<TEntity> : TreeEntityBase<TEntity, Guid, Guid?> where TEntity : ITreeEntity<TEntity, Guid, Guid?>
    {
     
       
    }

   
    public abstract class TreeEntityBase<TEntity, TKey, TParentId> : AggregateRoot<TEntity,TKey>, ITreeEntity<TEntity, TKey, TParentId> where TEntity : ITreeEntity<TEntity, TKey, TParentId> {
       
        public virtual TParentId ParentId { get; set; }

      
        [Required]
        public virtual string Path { get; set; }

       
        public virtual int Level { get;  set; }

      
        public virtual bool Enabled { get; set; }

      
        public virtual int? SortId { get; set; }

      
        public virtual void InitPath()
        {
            InitPath( default(TEntity) );
        }

       
        public virtual void InitPath( TEntity parent ) {
            if( Equals( parent, null ) ) {
                Level = 1;
                Path = $"{Id},";
                return;
            }
            Level = parent.Level + 1;
            Path = $"{parent.Path}{Id},";
        }

        public virtual List<TKey> GetParentIdsFromPath( bool excludeSelf = true ) {
            if( string.IsNullOrWhiteSpace( Path ) )
                return new List<TKey>();
            var result = Path.Split( ',' ).Where( id => !string.IsNullOrWhiteSpace( id ) && id != "," ).ToList();
            if( excludeSelf )
                result = result.Where( id => id.SafeString().ToLower() != Id.SafeString().ToLower() ).ToList();
            return result.Select( Orion.Framework.Helpers.TypeConvert.To<TKey> ).ToList();
        }
    }
}
