using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orion.Framework.Domains {
    /// <summary>
    /// 
    /// </summary>
    public class ChangeValueCollection : List<ChangeValue> {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="description"></param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public void Add( string propertyName, string description, string oldValue, string newValue ) {
            if( string.IsNullOrWhiteSpace( propertyName ) )
                return;
            Add( new ChangeValue( propertyName, description, oldValue, newValue ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() {
            var result = new StringBuilder();
            foreach( var item in this )
                result.AppendLine( item.ToString() );
            return result.ToString();
        }
    }
}
