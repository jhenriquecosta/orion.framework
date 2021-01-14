using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Domains.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ModelFieldAttribute : Attribute
    {
        public string PlaceHolder { get; set; }
        public string Display { get; set; }
        public string FieldMapping { get; set; }
        public string FieldServerMapping { get; set; }
        public bool Ignore { get; set; }
        public string DateFormat { get; set; }
        public bool AllowEdit { get; set; } = true;
        public bool AllowSort { get; set; } = true;
        public bool AllowFilter { get; set; } = true;
        public string Width { get; set; }
        public int Order { get; set; }
        public bool AutoFit { get; set; }
    }

    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ModelLookupAttribute : Attribute
    {
        public ModelLookupAttribute() { }
        public ModelLookupAttribute(string display,string value,string service)
        {
            FieldDisplay = display;
            FieldValue = value;
            Service = service;
        }
        public string FieldDisplay { get; set; }
        public string FieldValue { get; set; }
        public string Service { get; set; }
    }
    

}
