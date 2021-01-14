using NHibernate.Cfg;

namespace Orion.Framework.App.Services.Security
{

    public static class ConfigurationExtensions {

        public static Configuration AddIdentityMappingsForPostgres(
            this Configuration cfg
        ) {
            var asm = typeof(AspNetUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.pg.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

        public static Configuration AddIdentityMappingsForSqlServer(
            this Configuration cfg
        ) {
            var asm = typeof(AspNetUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.mssql.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

        public static Configuration AddIdentityMappingsForMySql(
            this Configuration cfg
        ) {
            var asm = typeof(AspNetUser).Assembly;
            var stream = asm.GetManifestResourceStream(
                "NHibernate.AspNetCore.Identity.Mappings.AspNetCoreIdentity.mysql.xml"
            );
            cfg.AddInputStream(stream);
            return cfg;
        }

    }

}
