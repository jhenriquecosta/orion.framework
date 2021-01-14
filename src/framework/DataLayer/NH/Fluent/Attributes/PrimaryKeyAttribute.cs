using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.DataLayer.NH.Fluent
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PrimaryKeyAttribute : Attribute
    {
        public PrimaryKeyAttribute() { }
        
        public PrimaryKeyAttribute(string column)
        {
            this.Column = column;
        }
        public string Column { get; set; }

    }

}
