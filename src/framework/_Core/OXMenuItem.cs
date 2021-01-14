using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework
{
    public class FWorkMenuItem
    {
        public string Area { get; set; }
        public string Home { get; set; }
        public string Target { get; set; }
        public string Restriction { get; set; }

        public string Text { get; set; }
        public string Url { get; set; }
        public string IconCss { get; set; }

        public string Description { get; set; }
        public List<FWorkMenuItem> SubItems { get; set; } = new List<FWorkMenuItem>();

    }
}
