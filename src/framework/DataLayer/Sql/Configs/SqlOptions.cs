using Orion.Framework.Domains.Enums;

namespace Orion.Framework.DataLayer.Sql.Configs
{

    public class SqlOptions {
       
        public Database DatabaseType { get; set; } = Database.SqlServer;
       
        public bool IsClearAfterExecution { get; set; } = true;
    }
}