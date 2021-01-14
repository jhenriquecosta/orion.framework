using System.Collections.Generic;
using System.Linq;

namespace Orion.Framework.DataLayer.Queries {
    /// <summary>
    /// 
    /// </summary>
    public class OrderByBuilder {
        /// <summary>
        /// 
        /// </summary>
        private readonly List<OrderByItem> _items;

        /// <summary>
        /// 
        /// </summary>
        public OrderByBuilder() {
            _items = new List<OrderByItem>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        public void Add( string name, bool desc = false ) {
            if( string.IsNullOrWhiteSpace( name ) )
                return;
            _items.Add( new OrderByItem( name, desc ) );
        }

        /// <summary>
        /// 
        /// </summary>
        public string Generate() {
            return _items.Select( t => t.Generate() ).ToList().Join();
        }
    }
}
