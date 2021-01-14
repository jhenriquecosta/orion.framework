namespace Orion.Framework.DataLayer.Sql.Builders.Core {
    /// <summary>
    /// 
    /// </summary>
    public class BuilderItem {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="builder"></param>
        public BuilderItem( string name, ISqlBuilder builder ) {
            Name = name;
            Builder = builder;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public ISqlBuilder Builder { get; }
    }
}
