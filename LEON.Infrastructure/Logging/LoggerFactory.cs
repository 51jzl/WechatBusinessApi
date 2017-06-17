using System;
namespace LEON.Logging
{
	public static class LoggerFactory
	{
		public static ILogger GetLogger(string loggerName)
		{
			ILoggerFactoryAdapter loggerFactoryAdapter = DIContainer.Resolve<ILoggerFactoryAdapter>();
			return loggerFactoryAdapter.GetLogger(loggerName);
		}
		public static ILogger GetLogger()
		{
			return LoggerFactory.GetLogger("LEON.Logging");
		}
	}
}
