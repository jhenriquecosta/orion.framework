using System;

namespace Orion.Framework.Domains
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class MapTableAttribute : Attribute
    {
        public MapTableAttribute() { }

        public MapTableAttribute(string table)
        {
            this.Table = table;
        }

        public MapTableAttribute(string schema, string table)
        {
            this.Table = table;
            this.Schema = schema;
        }
        public string Catalog { get; set; }
        public string Table { get; set; }
        public string Schema { get; set; }
        public string PrimaryKey { get; set; }
    }
}
