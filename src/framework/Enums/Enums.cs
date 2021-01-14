using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orion.Framework.Domains.Enums
{

    public enum SessionContextInfo
    {
        [Description("Database")]
        Database = 1,
        [Description("ConnectionString")]
        ConnectionString,
        [Description("ExportMappings")]
        MappingExport,
        [Description("ExportMappingsFolder")]
        MappingExportFolder

    }
    public enum Database
    {
        SqlServer,
        MySql,
        PgSql,
        Oracle,
        [Description("Sqlite")]
        Sqlite
    }
    public enum DatabaseAction
    {
        None=0,
        Create=1,
        Update,
        Reset
        
    }
    public enum CrudAction { New, Edit, Delete };
    public enum SchemaAction { None = 0, Validate, Update, Create, Recreate }

    public enum TypeUnidade 
    {
        [Description("Organização")]
        Organizacao = 1,
        [Description("Grupo")]
        Grupo,
        [Description("Empresa")]
        Empresa,
        [Description("Unidade")]
        Unidade 
    }
    public enum TypeMenuItem
    {
        [Description("Menu Item")]
        MenuItem = 1,
        [Description("Url")]
        Url
    }
    

    public enum ConfigurationBy { Fluent, Loquacious, Merge }
    public enum TypeSolution 
    {
        WebForms = 0,
        Mvc,
        WinForms,
        Android,
    }
    public enum DataAccessEngine
    {
        NHibernate = 0,
        EF,
        Dapper,
    }
    public enum ClientViewEngine
    {
        DevExpress = 0,
        JQuery,
        SyncFusion,
        ExtNet,
        Angular
    }
    public enum DatabaseEngine
    {
        [Description("None")]
        None = 0,
        [Description("SqlServer")]
        SqlServer,
        [Description("SqlLite")]
        SqlLite,
        [Description("MySql")]
        MySql
    }
}
