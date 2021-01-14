using System;
using System.Linq;
using System.Reflection;

namespace Orion.Framework.Domains {
   
    public abstract class ValueObjectBase<TValueObject> : DomainBase<TValueObject>, IEquatable<TValueObject> where TValueObject : ValueObjectBase<TValueObject> {
        /// <summary>
    
        /// </summary>
        public bool Equals( TValueObject other ) {
            return this == other;
        }

        /// <summary>
      
        /// </summary>
        public override bool Equals( object other ) {
            return Equals( other as TValueObject );
        }

        /// <summary>
      
        /// </summary>
        public static bool operator ==( ValueObjectBase<TValueObject> left, ValueObjectBase<TValueObject> right ) {
            if( (object)left == null && (object)right == null )
                return true;
            if( !( left is TValueObject ) || !( right is TValueObject ) )
                return false;
            var properties = left.GetType().GetTypeInfo().GetProperties();
            return properties.All( property => property.GetValue( left ) == property.GetValue( right ) );
        }

        /// <summary>
    
        /// </summary>
        public static bool operator !=( ValueObjectBase<TValueObject> left, ValueObjectBase<TValueObject> right ) {
            return !( left == right );
        }

        /// <summary>
      
        /// </summary>
        public override int GetHashCode() {
            var properties = GetType().GetTypeInfo().GetProperties();
            return properties.Select( property => property.GetValue( this ) )
                    .Where( value => value != null )
                    .Aggregate( 0, ( current, value ) => current ^ value.GetHashCode() );
        }

        /// <summary>
     
        /// </summary>
        public virtual TValueObject Clone() {
            return Orion.Framework.Helpers.TypeConvert.To<TValueObject>( MemberwiseClone() );
        }
    }
}
