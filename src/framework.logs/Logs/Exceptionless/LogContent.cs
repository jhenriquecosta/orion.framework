using System.Collections.Generic;
using Orion.Framework.Domains.ValueObjects;
using Orion.Framework.Properties;

namespace Orion.Framework.Logs.Exceptionless
{
    /// <summary>
    /// 
    /// </summary>
    public class LogContent : Contents.LogContent, ILogConvert {
        /// <summary>
        /// 
        /// </summary>
        public List<DataItem> To() {
            return new List<DataItem> {
                { new DataItem( LogResource.LogName, LogName,1) },
                { new DataItem(LogResource.TraceId, TraceId,2) },
                { new DataItem(LogResource.OperationTime, OperationTime,3) },
                { new DataItem(LogResource.Duration, Duration,4) },
                { new DataItem(LogResource.ThreadId, ThreadId,5) },
                { new DataItem("Url", Url,6) },
                { new DataItem(LogResource.UserId, UserId,7) },
                { new DataItem(LogResource.Operator, Operator,8 ) },
                { new DataItem(LogResource.Role,Role,9)  },
                { new DataItem(LogResource.BusinessId,BusinessId,10) },
                { new DataItem(LogResource.Tenant,Tenant,11)  },
                { new DataItem(LogResource.Application,Application,12) },
                { new DataItem(LogResource.Module,Module ,13) },
                { new DataItem(LogResource.Class,Class ,14) },
                { new DataItem(LogResource.Method,Method ,15) },
                { new DataItem(LogResource.Params,Params.ToString(),16) },
                { new DataItem(LogResource.Caption,Caption ,17) },
                { new DataItem(LogResource.Content,Content.ToString(),18) },
                { new DataItem(LogResource.Sql,Sql.ToString(),19)  },
                { new DataItem(LogResource.SqlParams,SqlParams.ToString(),20) },
                { new DataItem(LogResource.ErrorCode,ErrorCode,21) }
            };
        }
    }
}
