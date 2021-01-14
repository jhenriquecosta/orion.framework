using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Domains.ValueObjects
{
    public class TreeData
    {
        public int Id { get; set; }
        public int? Ancestral { get; set; }
        public string Text { get; set; }
        public string Url { get; set; }
        public string IconCls { get; set; }
        public bool Expanded { get; set; }
        public bool Selected { get; set; }
        public bool HasChild { get; set; }
    }
}
