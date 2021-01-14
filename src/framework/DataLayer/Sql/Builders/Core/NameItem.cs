using System.Collections.Generic;
using System.Linq;
using Orion.Framework.DataLayer.Sql.Matedatas;
using Orion.Framework.Helpers;

namespace Orion.Framework.DataLayer.Sql.Builders.Core {
    /// <summary>
    /// ，.
    /// </summary>
    public class NameItem {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        public NameItem( string name ) {
            if( string.IsNullOrWhiteSpace( name ) )
                return;
            var list = IsComplex( name ) ? ResolveByPattern( name ) : ResolveBySplit( name );
            if( list.Count == 1 ) {
                Name = list[0];
                return;
            }
            if( list.Count == 2 ) {
                Prefix = list[0];
                Name = list[1];
                return;
            }
            if( list.Count == 3 ) {
                DatabaseName = list[0];
                Prefix = list[1];
                Name = list[2];
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool IsComplex( string name ) {
            return name.Contains( "[" ) || name.Contains( "`" ) || name.Contains( "\"" );
        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> ResolveBySplit( string name ) {
            return name.Split( '.' ).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        private List<string> ResolveByPattern( string name ) {
            var pattern = "^(([\\[`\"]\\S+?[\\]`\"]).)?(([\\[`\"]\\S+[\\]`\"]).)?([\\[`\"]\\S+[\\]`\"])$";
            var list = Regex.GetValues( name, pattern, new[] { "$1", "$2", "$3", "$4", "$5" } ).Select( t => t.Value ).ToList();
            return list.Where( t => string.IsNullOrWhiteSpace( t ) == false && t.EndsWith( "." ) == false ).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        public string DatabaseName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private string _prefix;
        /// <summary>
        /// 
        /// </summary>
        public string Prefix {
            get => _prefix.SafeString();
            set => _prefix = value;
        }

        /// <summary>
        /// 
        /// </summary>
        private string _name;
        /// <summary>
        /// 
        /// </summary>
        public string Name {
            get => _name.SafeString();
            set => _name = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dialect"></param>
        /// <param name="prefix">
        /// <param name="tableDatabase"></param>
        public string ToSql( IDialect dialect, string prefix = null, ITableDatabase tableDatabase = null ) {
            var name = GetName( dialect, prefix );
            var database = GetDatabase( dialect, tableDatabase, prefix );
            return string.IsNullOrWhiteSpace( database ) ? name : $"{database}.{name}";
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetName( IDialect dialect, string prefix ) {
            prefix = GetPrefix( prefix );
            if( string.IsNullOrWhiteSpace( prefix ) )
                return dialect.SafeName( Name );
            return $"{dialect.SafeName( prefix )}.{dialect.SafeName( Name )}";
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetPrefix( string prefix ) {
            if( string.IsNullOrWhiteSpace( Prefix ) )
                return prefix;
            return Prefix;
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetDatabase( IDialect dialect, ITableDatabase tableDatabase, string prefix ) {
            if ( string.IsNullOrWhiteSpace( DatabaseName ) == false )
                return dialect.SafeName( DatabaseName );
            return tableDatabase == null ? null : dialect.SafeName( tableDatabase.GetDatabase( GetName( prefix ) ) );
        }

        /// <summary>
        /// 
        /// </summary>
        private string GetName( string prefix ) {
            prefix = GetPrefix( prefix );
            if( string.IsNullOrWhiteSpace( prefix ) )
                return Name;
            return $"{prefix}.{Name}";
        }
    }
}
