using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Orion.Framework.Domains.ValueObjects;

namespace Orion.Framework.Helpers
{
   
    public static class HelperEnum {

      
        public static TEnum Parse<TEnum>( object member ) {
            string value = member.SafeString();
            if( string.IsNullOrWhiteSpace( value ) ) {
                if( typeof( TEnum ).IsGenericType )
                    return default( TEnum );
                throw new ArgumentNullException( nameof( member ) );
            }
            return (TEnum)System.Enum.Parse( Common.GetType<TEnum>(), value, true );
        }

        public static string GetName<TEnum>( object member ) {
            return GetName( Common.GetType<TEnum>(), member );
        }

        public static string GetName( Type type, object member ) {
            if( type == null )
                return string.Empty;
            if( member == null )
                return string.Empty;
            if( member is string )
                return member.ToString();
            if( type.GetTypeInfo().IsEnum == false )
                return string.Empty;
            return System.Enum.GetName( type, member );
        }

        public static int GetValue<TEnum>( object member ) {
            return GetValue( Common.GetType<TEnum>(), member );
        }

        public static int GetValue( Type type, object member ) {
            string value = member.SafeString();
            if( string.IsNullOrWhiteSpace( value ) )
                throw new ArgumentNullException( nameof(member) );
            return (int)System.Enum.Parse( type, member.ToString(), true );
        }
       public static string GetDescription<TEnum>( object member ) {
            return Reflection.GetDescription<TEnum>( GetName<TEnum>( member ) );
        }

        public static string GetDescription( Type type, object member ) {
            return Reflection.GetDescription( type, GetName( type, member ) );
        }

        public static List<DataItem> GetItems<TEnum>() {
            return GetItems( typeof( TEnum ) );
        }
        public static List<DataItem> GetItems( Type type ) {
            type = Common.GetType( type );
            if( type.IsEnum == false )
                throw new InvalidOperationException( $" Listando {type} " );
            var result = new List<DataItem>();
            foreach( var field in type.GetFields() )
                AddItem( type, result, field );
            return result.OrderBy( t => t.SortId ).ToList();
        }
        private static void AddItem( Type type, ICollection<DataItem> result, FieldInfo field )
        {
            if( !field.FieldType.IsEnum )
                return;
            var value = GetValue( type, field.Name );
            var description = Reflection.GetDescription( field );
            result.Add( new DataItem(description,value,value));
        }
        /// <summary>
      
        public static List<string> GetNames<TEnum>()
        {
            return GetNames(typeof(TEnum));
        }

        /// <summary>
    
        public static List<string> GetNames(Type type)
        {
            type = Common.GetType(type);
            if (type.IsEnum == false)
                throw new InvalidOperationException($" {type} ");
            var result = new List<string>();
            foreach (var field in type.GetFields())
            {
                if (!field.FieldType.IsEnum)
                    continue;
                result.Add(field.Name);
            }
            return result;
        }
    }
}
