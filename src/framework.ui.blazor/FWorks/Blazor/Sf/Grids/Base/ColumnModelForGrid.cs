using Syncfusion.Blazor.Grids;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Ui.FWorks.Blazor.Sf.Grids
{
    public class ColumnModel
    {
        public int Order { get; set; }
        public string Field { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Width { get; set; } = "150";
        public bool Ignore { get; set; } = false;

        public bool IsSortable { get; set; } = true;
        public bool IsEditable { get; set; } = true;
        public bool IsFiltable { get; set; } = true;
        public bool IsResizeble { get; set; } = false;
        public bool IsCheckBox { get; set; } = false;
        public bool IsPrimaryKey { get; set; } = false;
        public ColumnType Type { get; set; }
        public string Format { get; set; }

    }
}
