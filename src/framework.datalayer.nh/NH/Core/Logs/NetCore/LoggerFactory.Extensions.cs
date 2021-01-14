using NHibernate;
using System;
using Orion.Framework.DataLayer.NH.Logs.NetCore;

namespace Orion.Framework.DataLayer.NH 
{

    public static class LoggerFactoryExtensions {

        public static void UseAsHibernateLoggerFactory(
            this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory
        ) {
            NHibernateLogger.SetLoggersFactory(
                new NetCoreLoggerFactory(loggerFactory)
            );
        }

    }

}
