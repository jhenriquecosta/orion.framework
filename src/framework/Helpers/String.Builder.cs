using System.Text;

namespace Orion.Framework.Helpers {
    /// <summary>
    /// 
    /// </summary>
    public partial class String {
        /// <summary>
        /// 
        /// </summary>
        public String() {
            Builder = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        private StringBuilder Builder { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">>
        public String Append<T>( T value ) {
            Builder.Append( value );
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        public String Append( string value, params object[] args ) {
            if( args == null )
                args = new object[] { string.Empty };
            if( args.Length == 0 )
                Builder.Append( value );
            else
                Builder.AppendFormat( value, args );
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public String AppendLine() {
            Builder.AppendLine();
            return this;
        }

        /// <summary>
        /// 追加内容并换行
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        public String AppendLine<T>( T value ) {
            Append( value );
            Builder.AppendLine();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        public String AppendLine( string value, params object[] args ) {
            Append( value, args );
            Builder.AppendLine();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        public String Replace( string value ) {
            Builder.Clear();
            Builder.Append( value );
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="end"></param>
        public String RemoveEnd( string end ) {
            string result = Builder.ToString();
            if( !result.EndsWith( end ) )
                return this;
            Builder = new StringBuilder( result.TrimEnd( end.ToCharArray() ) );
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public String Clear() {
            Builder = Builder.Clear();
            return this;
        }

        /// <summary>
        /// 
        /// </summary>
        public int Length => Builder.Length;

        /// <summary>
        /// 
        /// </summary>
        public string Empty => string.Empty;

        /// <summary
        /// 
        /// </summary>
        public override string ToString() {
            return Builder.ToString();
        }
    }
}
