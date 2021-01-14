namespace Orion.Framework.DataLayer.Queries {
    /// <summary>
    /// 
    /// </summary>
    public class OrderByItem {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="desc"></param>
        public OrderByItem( string name, bool desc ) {
            Name = name;
            Desc = desc;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool Desc { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Generate() {
            if( Desc )
                return $"{Name} desc";
            return Name;
        }
    }
}
