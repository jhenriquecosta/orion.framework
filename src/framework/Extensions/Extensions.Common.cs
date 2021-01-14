using System.Collections.Generic;

namespace Orion.Framework {
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public static T SafeValue<T>( this T? value ) where T : struct {
            return value ?? default( T );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        public static int Value( this System.Enum instance ) {
            return Orion.Framework.Helpers.HelperEnum.GetValue( instance.GetType(), instance );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="instance"></param>
        public static TResult Value<TResult>( this System.Enum instance ) {
            return Orion.Framework.Helpers.TypeConvert.To<TResult>( Value( instance ) );
        }

        /// <summary>
        /// ,
        /// </summary>
        /// <param name="instance"></param>
        public static string Description( this System.Enum instance ) {
            return Orion.Framework.Helpers.HelperEnum.GetDescription( instance.GetType(), instance );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="quotes"> "'"</param>
        /// <param name="separator">，</param>
        public static string Join<T>( this IEnumerable<T> list, string quotes = "", string separator = "," ) {
            return Orion.Framework.Helpers.String.Join( list, quotes, separator );
        }
    }
}
