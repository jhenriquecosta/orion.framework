using System;
using System.Collections.Generic;
using System.Text;
using Orion.Framework.Domains.Enums;

namespace Orion.Framework.DataLayer
{
    public interface IDatabaseConfigurer
    {
        Database Engine { get; set; }
        string ServerName { get; set; }
        string Catalog { get; set; }
        bool UseTrustedConnection { get; set; }
        string FolderExportMapping { get; set; }
        string FolderExportSchema { get; set; }
    }
}
