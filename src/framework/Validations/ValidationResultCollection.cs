using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Orion.Framework.Validations {
    /// <summary>
    /// 
    /// </summary>
    public class ValidationResultCollection : IEnumerable<ValidationResult> {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<ValidationResult> _results;

        /// <summary>
        /// 
        /// </summary>
        public ValidationResultCollection() : this( "" ) {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public ValidationResultCollection( string result ) {
            _results = new List<ValidationResult>();
            if( string.IsNullOrWhiteSpace( result ) )
                return;
            _results.Add( new ValidationResult( result ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public static readonly ValidationResultCollection Success = new ValidationResultCollection();

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid => _results.Count == 0;

        /// <summary>
        /// 
        /// </summary>
        public int Count => _results.Count;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void Add( ValidationResult result ) {
            if( result == null )
                return;
            _results.Add( result );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="results"></param>
        public void AddList( IEnumerable<ValidationResult> results ) {
            if( results == null )
                return;
            foreach( var result in results )
                Add( result );
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerator<ValidationResult> IEnumerable<ValidationResult>.GetEnumerator() {
            return _results.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return _results.GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string ToString() 
        
        {
            if( IsValid )
                return string.Empty;
            return _results.First().ErrorMessage;
        }
    }
}
