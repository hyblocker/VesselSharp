using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public class Logger : ILogger
	{
		public bool DoEngineLogging = true;

		public static Vessel.Logger Log;

		private Serilog.Core.Logger internalLogger;

		public Logger()
		{
			IOHelper.EnsureDirectoryExists("logs");

			internalLogger = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Minute, retainedFileCountLimit: null, shared: true)
				.CreateLogger();

			Serilog.Log.Logger = internalLogger;
		}

		public void Close()
		{
			Serilog.Log.CloseAndFlush();
		}

		#region Verbose

		public void Verbose()
		{
			internalLogger.Verbose(string.Empty);
		}

		public void Verbose(object message)
		{
			internalLogger.Verbose(message.ToString());
		}

		public void Verbose(string message)
		{
			internalLogger.Verbose(message);
		}

		public void Verbose(string message, params string[] parameters)
		{
			internalLogger.Verbose(string.Format(message, parameters));
		}

		#endregion

		#region Debug

		public void Debug()
		{
			internalLogger.Debug(string.Empty);
		}

		public void Debug(object message)
		{
			internalLogger.Debug(message.ToString());
		}

		public void Debug(string message)
		{
			internalLogger.Debug(message);
		}

		public void Debug(string message, params string[] parameters)
		{
			internalLogger.Debug(string.Format(message, parameters));
		}

		#endregion

		#region Info

		public void Info()
		{
			internalLogger.Information(string.Empty);
		}

		public void Info(object message)
		{
			internalLogger.Information(message.ToString());
		}

		public void Info(string message)
		{
			internalLogger.Information(message);
		}

		public void Info(string message, params string[] parameters)
		{
			internalLogger.Information(string.Format(message, parameters));
		}

		#endregion

		#region Warn

		public void Warn()
		{
			internalLogger.Warning(string.Empty);
		}

		public void Warn(object message)
		{
			internalLogger.Warning(message.ToString());
		}

		public void Warn(string message)
		{
			internalLogger.Warning(message);
		}

		public void Warn(string message, params string[] parameters)
		{
			internalLogger.Warning(string.Format(message, parameters));
		}

		#endregion

		#region Error

		public void Error()
		{
			internalLogger.Error(string.Empty);
		}

		public void Error(object message)
		{
			internalLogger.Error(message.ToString());
		}

		public void Error(string message)
		{
			internalLogger.Error(message);
		}

		public void Error(string message, params string[] parameters)
		{
			internalLogger.Error(string.Format(message, parameters));
		}

		#endregion

		#region Fatal

		public void Fatal()
		{
			internalLogger.Fatal(string.Empty);
		}

		public void Fatal(object message)
		{
			internalLogger.Fatal(message.ToString());
		}

		public void Fatal(string message)
		{
			internalLogger.Fatal(message);
		}

		public void Fatal(string message, params string[] parameters)
		{
			internalLogger.Fatal(string.Format(message, parameters));
		}

		#endregion

	}
}
