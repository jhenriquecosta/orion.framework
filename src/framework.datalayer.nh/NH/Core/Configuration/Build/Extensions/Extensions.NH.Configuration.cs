using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Orion.Framework.Domains;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Orion.Framework
{
    public static partial class Extensions
    {

        private static string nameContext;
        public static Configuration DBReset(this Configuration cfg,string context=null)
        {
            nameContext = context ?? "";
            nameContext = $"{nameContext}-reset";

            var fileName = Path.Combine(AppHelper.SysSettings().Folder.Sql, $"schema-{nameContext.ToLower()}.sql");

            var schemaExport = new SchemaExport(cfg);
            schemaExport.SetOutputFile(fileName);
            schemaExport.Drop(true, true);
            schemaExport.Create(true, true);
            return cfg;
        }
        public static Configuration AddInterceptor(this Configuration cfg, IInterceptor interceptor)
        {
            cfg.SetInterceptor(interceptor);
            return cfg;
        }
        public static Configuration DBCreate(this Configuration cfg)
        {
            var fileName = "c:\\usr\\create_schema.sql";
            var schemaExport = new SchemaExport(cfg);
            schemaExport.SetOutputFile(fileName);
            schemaExport.Create(true, true);
            return cfg;
        }
        public static Configuration DBUpdate(this Configuration cfg,string context =null)
        {
            nameContext = context ?? "update";
            
            var update = new SchemaUpdate(cfg);
            update.Execute(LogAutoMigration, true);
            return cfg;

        }
        public static Configuration DBDrop(this Configuration cfg)
        {
            new SchemaExport(cfg).Drop(true, true);
            return cfg;
        }

        public static Configuration UpdateSchema(this Configuration cfg, bool export = false)
        {
            if (export)
            {
                var fileName = "c:\\usr\\create_schema.sql";
                var schemaExport = new SchemaExport(cfg);
                schemaExport.SetOutputFile(fileName);
                schemaExport.Create(true, true);
                return cfg;
            }
            var update = new SchemaUpdate(cfg);
            update.Execute(LogAutoMigration, true);
            return cfg;
        }

        private static void LogAutoMigration(string sql)
        {

            var fileName = Path.Combine(AppHelper.SysSettings().Folder.Sql, $"schema-{nameContext.ToLower()}.sql");
            using var file = new FileStream(fileName, FileMode.Append);
            using var sw = new StreamWriter(file);
            sw.Write(sql);
        }

    }
}
