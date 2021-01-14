using NHibernate;
using Serilog;
using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orion.Framework.DataLayer.NH.Logs.Serilog
{
   
	public class NHSerilogFactory : INHibernateLoggerFactory
	{
		public INHibernateLogger LoggerFor(string keyName)
		{
			var contextLogger = Log.Logger.ForContext(Constants.SourceContextPropertyName, keyName);
			return new NHSerilogLogger(contextLogger);
		}

		public INHibernateLogger LoggerFor(System.Type type)
		{
			var contextLogger = Log.Logger.ForContext(type);
			return new NHSerilogLogger(contextLogger);
		}
	}
}
