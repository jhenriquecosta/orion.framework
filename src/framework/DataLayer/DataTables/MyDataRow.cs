﻿using System;
using System.Collections.Generic;

namespace Orion.Framework.DataLayer.DataTables
{
    public class MyDataRow : Dictionary<string, object>
    {
        internal MyDataRow(MyDataTable table)
        {
            if (table == null) throw new ArgumentNullException("table");
            if (table.Columns.Count == 0) throw new InvalidOperationException("The given table has no columns.");
            foreach (var col in table.Columns)
            {
                Add(col.Key, col.Value.DataType?.GetDefaultValue());
            }
            Table = table;
        }

        public MyDataTable Table { get; set; }
    }
}
