using System;
using System.ComponentModel.DataAnnotations;
using Orion.Framework.Domains;

namespace Orion.Framework.DataLayer.Persistence {

    public abstract class PersistentEntityBase : PersistentEntityBase<int> {
    }


    public abstract class PersistentEntityBase<TKey> : IKey<TKey> {
  
        [Key]
        public TKey Id { get; set; }


        public override bool Equals( object other ) {
            return this == (PersistentEntityBase<TKey>)other;
        }


        public override int GetHashCode() {
            return ReferenceEquals( Id, null ) ? 0 : Id.GetHashCode();
        }

  
        public static bool operator ==( PersistentEntityBase<TKey> left, PersistentEntityBase<TKey> right ) {
            if( (object)left == null && (object)right == null )
                return true;
            if( (object)left == null || (object)right == null )
                return false;
            if( left.GetType() != right.GetType() )
                return false;
            if( Equals( left.Id, null ) )
                return false;
            if( left.Id.Equals( default( TKey ) ) )
                return false;
            return left.Id.Equals( right.Id );
        }


        public static bool operator !=( PersistentEntityBase<TKey> left, PersistentEntityBase<TKey> right ) {
            return !( left == right );
        }
    }
}
