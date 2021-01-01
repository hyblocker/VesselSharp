namespace Vessel
{
	/// <summary>
	/// The base class for all Loggers that may be attached to Vessel
	/// Implement this logger and set it in the <see cref="VesselEngine"/> constructor to utilise it
	/// </summary>
	public interface ILogger
	{
		void Verbose();
		void Verbose(object message);
		void Verbose(string message);
		void Verbose(string message, params string[] parameters);

		void Debug();
		void Debug(object message);
		void Debug(string message);
		void Debug(string message, params string[] parameters);

		void Info();
		void Info(object message);
		void Info(string message);
		void Info(string message, params string[] parameters);

		void Warn();
		void Warn(object message);
		void Warn(string message);
		void Warn(string message, params string[] parameters);
		
		void Error();
		void Error(object message);
		void Error(string message);
		void Error(string message, params string[] parameters);

		void Fatal();
		void Fatal(object message);
		void Fatal(string message);
		void Fatal(string message, params string[] parameters);
	}
}
