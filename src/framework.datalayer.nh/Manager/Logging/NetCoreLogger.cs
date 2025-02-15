﻿using System;
using Microsoft.Extensions.Logging;
using NHibernate;

namespace Zeus.NHibernate.Logs {

    public class NetCoreLogger : IDisposable, INHibernateLogger {

        private ILogger logger;

        public NetCoreLogger(
            ILogger logger
        ) {
            this.logger = logger;
        }

        public void Dispose() {
            logger = null;
        }

        public bool IsEnabled(NHibernateLogLevel logLevel) {
            var level = (int)logLevel;
            var msLogLevel = (LogLevel)level;
            return logger.IsEnabled(msLogLevel);
        }

        public void Log(
            NHibernateLogLevel logLevel,
            NHibernateLogValues state,
            Exception exception
        ) {
            var level = (int)logLevel;
            var msLogLevel = (LogLevel)level;
            logger.Log(
                msLogLevel,
                default(EventId),
                state,
                exception,
                (s, ex) => {
                    var message = s.ToString();
                    if (ex != null) {
                        message += ex.ToString();
                    }
                    return message;
                }
            );
        }
    }

}
