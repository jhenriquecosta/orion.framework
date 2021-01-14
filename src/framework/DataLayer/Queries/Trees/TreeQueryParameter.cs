using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Orion.Framework.Helpers;

namespace Orion.Framework.DataLayer.Queries.Trees {
  
    public class TreeQueryParameter<TParentId> : QueryParameter, ITreeQueryParameter<TParentId> {
       
        protected TreeQueryParameter() {
            Order = "SortId";
        }

      
        public TParentId ParentId { get; set; }

       
        public int? Level { get; set; }

        private string _path = string.Empty;
      
        public string Path {
            get => _path == null ? string.Empty : _path.Trim();
            set => _path = value;
        }

      
        [Display( Name = "" )]
        public bool? Enabled { get; set; }

     
        public virtual bool IsSearch() {
            var items = Reflection.GetPublicProperties( this );
            return items.Any( t => IsSearchProperty( t.Text, t.Value ) );
        }

        
        protected virtual bool IsSearchProperty( string name, object value ) {
            if ( value.SafeString().IsEmpty() )
                return false;
            switch ( name.SafeString().ToLower() ) {
                case "order":
                case "pagesize":
                case "page":
                case "totalcount":
                    return false;
            }
            return true;
        }
    }

    
    public class TreeQueryParameter : TreeQueryParameter<Guid?>, ITreeQueryParameter {
    }
}
