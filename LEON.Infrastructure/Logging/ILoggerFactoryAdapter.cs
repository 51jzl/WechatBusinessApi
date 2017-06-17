using System;
namespace LEON.Logging
{
	public interface ILoggerFactoryAdapter
	{
		ILogger GetLogger(string loggerName);
	}
}
