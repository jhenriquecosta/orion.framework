using System.Text;

namespace Orion.Framework.Domains {
    /// <summary>
    /// 
    /// </summary>
    public class ChangeValue {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="description">param>
        /// <param name="oldValue"></param>
        /// <param name="newValue"></param>
        public ChangeValue( string propertyName, string description, string oldValue, string newValue ) {
            PropertyName = propertyName;
            Description = description;
            OldValue = oldValue;
            NewValue = newValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public string PropertyName { get; }
        /// <summary>
        /// 
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// 
        /// </summary>
        public string OldValue { get; }
        /// <summary>
        /// 
        /// </summary>
        public string NewValue { get; }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() {
            var result = new StringBuilder();
            result.AppendFormat( "{0}({1}),", PropertyName, Description );
            result.AppendFormat( ":{0},:{1}", OldValue, NewValue );
            return result.ToString();
        }
    }
}
