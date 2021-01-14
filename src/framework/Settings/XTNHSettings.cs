using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.Settings
{
    public class XTNHSettings
    {
        /// <summary>
        /// Configuration Section name
        /// </summary>
        public const string SECTION_NAME = "NHibernate";

        /// <summary>
        /// The name/key of the connection string to use from the ConnectionStrings section of appsettings.json.
        /// </summary>
        public string ConnectionStringName { get; set; }
        public string FolderMappings { get; set; }
        public bool ExportMappings { get; set; }
        public bool UseSchema { get; set; }
        public string LogDebugSql { get; set; }

        /// <summary>
        /// Dictionary of NHibernate configuration properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }
   
}
