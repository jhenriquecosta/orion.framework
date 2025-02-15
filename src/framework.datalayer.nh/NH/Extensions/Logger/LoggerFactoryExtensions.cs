﻿using System;
using NHibernate;

namespace Zeus
{

    public static class LoggerFactoryExtensions
    {

        public static void UseAsHibernateLoggerFactory(this Microsoft.Extensions.Logging.ILoggerFactory loggerFactory)
        {
            NHibernateLogger.SetLoggersFactory(new NetCoreLoggerFactory(loggerFactory));
        }

    }

}
