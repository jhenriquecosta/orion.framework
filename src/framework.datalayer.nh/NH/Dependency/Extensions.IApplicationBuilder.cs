using Microsoft.Extensions.DependencyInjection;
using NHibernate.Cfg;
using NHibernate;
using Microsoft.AspNetCore.Builder;
using Orion.Framework.Domains.Enums;
using Orion.Framework.DataLayer.NH.Contracts;
using Orion.Framework.Validations;
using Orion.Framework.Settings;

namespace Orion.Framework
{
    public static partial class Extensions
    {
        public static IApplicationBuilder UseNHibernate(this IApplicationBuilder builder)
        {
            if (builder.ApplicationServices.GetService<ISessionFactory>() == null)
                throw new HibernateConfigException("Unable to initialize the session factorey.");

            return builder;
        }
        public static IApplicationBuilder WithAction(this IApplicationBuilder builder, DatabaseAction action = DatabaseAction.Update, string key = null)
        {
            var config = builder.ApplicationServices.GetService<ISessionFactoryProvider>();
            Ensure.NotNull(config, "Unable initialize configuration!");
            var cfg = config.GetConfiguration(key);
            if (action == DatabaseAction.Create) cfg.DBReset();
            if (action == DatabaseAction.Reset) cfg.DBReset();
            if (action == DatabaseAction.Update) cfg.DBUpdate();
            return builder;

        }
    }
}
