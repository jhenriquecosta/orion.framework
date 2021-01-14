using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Domains.Enums;

namespace Orion.Framework.DataLayer
{
    public class DatabaseConfigurer : IDatabaseConfigurer
    {
        public Database Engine { get; set; }
        public string ServerName { get; set; }
        public string Catalog { get; set; }
        public bool UseTrustedConnection { get; set; }
        public string FolderExportMapping { get; set; }
        public string FolderExportSchema { get; set; }
    }
}
