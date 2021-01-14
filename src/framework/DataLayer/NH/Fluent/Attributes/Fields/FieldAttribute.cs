using System;

namespace Orion.Framework.Domains
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class FieldAttribute : Attribute
    {
        public FieldAttribute() { }

        public FieldAttribute(string name)
        {
            this.Name = name;
        } 
        public string Name { get; set; }
        
    }
}
