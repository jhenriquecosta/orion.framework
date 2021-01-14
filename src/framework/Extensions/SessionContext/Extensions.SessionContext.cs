using System.Collections.Generic;
using Orion.Framework.DataLayer.SessionContext;
using Orion.Framework.Domains.Enums;
using Orion.Framework.Settings;
using Orion.Framework.Validations;

namespace Orion.Framework
{
    /// <summary>
    /// 
    /// </summary>
    public static partial class Extensions
    {
       public static object GetInformation(this ISessionContext sessionContext, SessionContextInfo info)
       {
            var session = sessionContext.Name;
            var properties = AppHelper.AppSettings().GetSection(XTConstants.AppSettingsSessionContext).GetSection(session).GetValue<IDictionary<string, object>>();
            var infoKey = info.GetDescription();
            var value = properties[infoKey];
            return value;
        }
        public static string GetConnectionString(this ISessionContext sessionContext)
        {
            var session = sessionContext.Name;
            var connection = GetInformation(sessionContext, SessionContextInfo.ConnectionString);
            var infoKey = connection.ToString();
            var value = AppHelper.AppSettings().GetStringConnection(infoKey);
            return value;
        }
        public static string GetName(this ISessionContext sessionContext)
        {
            Ensure.NotNull(sessionContext, "Contexto invalido!");
            var value = sessionContext.Name;           
            return value;
        }
    }
}
