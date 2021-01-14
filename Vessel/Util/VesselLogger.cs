namespace Vessel
{
	/// <summary>
	/// A logger that is used exclusively by the engine. Disabled in Release Builds
	/// </summary>
	internal class VesselLogger
	{
#if DEBUG
		internal static VesselLogger Logger;
		internal VesselEngine engine;

		internal VesselLogger(VesselEngine engine)
		{
			this.engine = engine;
			Logger = this;
		}

		#region Verbose

		internal void Verbose()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Verbose(string.Empty);
			}
		}

		internal void Verbose(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Verbose(message.ToString());
			}
		}

		internal void Verbose(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Verbose(message);
			}
		}

		internal void Verbose(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Verbose(string.Format(message, parameters));
			}
		}

		#endregion

		#region Debug

		internal void Debug()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Debug(string.Empty);
			}
		}

		internal void Debug(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Debug(message.ToString());
			}
		}

		internal void Debug(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Debug(message);
			}
		}

		internal void Debug(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Debug(string.Format(message, parameters));
			}
		}

		#endregion

		#region Info

		internal void Info()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Info(string.Empty);
			}
		}

		internal void Info(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Info(message.ToString());
			}
		}

		internal void Info(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Info(message);
			}
		}

		internal void Info(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Info(string.Format(message, parameters));
			}
		}

		#endregion

		#region Warn

		internal void Warn()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Warn(string.Empty);
			}
		}

		internal void Warn(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Warn(message.ToString());
			}
		}

		internal void Warn(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Warn(message);
			}
		}

		internal void Warn(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Warn(string.Format(message, parameters));
			}
		}

		#endregion

		#region Error

		internal void Error()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Error(string.Empty);
			}
		}

		internal void Error(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Error(message.ToString());
			}
		}

		internal void Error(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Error(message);
			}
		}

		internal void Error(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Error(string.Format(message, parameters));
			}
		}

		#endregion

		#region Fatal

		internal void Fatal()
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Fatal(string.Empty);
			}
		}

		internal void Fatal(object message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Fatal(message.ToString());
			}
		}

		internal void Fatal(string message)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Fatal(message);
			}
		}

		internal void Fatal(string message, params string[] parameters)
		{
			if (engine.EnableLogging)
			{
				engine.Logger.Fatal(string.Format(message, parameters));
			}
		}

		#endregion
#else
		//Release mode variant, with all logging functions reduced to unimplemented stubs

		internal static VesselLogger Logger;

		internal VesselLogger(VesselEngine engine)
		{
			Logger = this;
		}

		internal void Verbose() { }
		internal void Verbose(object message) { }
		internal void Verbose(string message) { }
		internal void Verbose(string message, params string[] parameters) { }

		internal void Debug() { }
		internal void Debug(object message) { }
		internal void Debug(string message) { }
		internal void Debug(string message, params string[] parameters) { }

		internal void Info() { }
		internal void Info(object message) { }
		internal void Info(string message) { }
		internal void Info(string message, params string[] parameters) { }

		internal void Warn() { }
		internal void Warn(object message) { }
		internal void Warn(string message) { }
		internal void Warn(string message, params string[] parameters) { }
		
		internal void Error() { }
		internal void Error(object message) { }
		internal void Error(string message) { }
		internal void Error(string message, params string[] parameters) { }

		internal void Fatal() { }
		internal void Fatal(object message) { }
		internal void Fatal(string message) { }
		internal void Fatal(string message, params string[] parameters) { }
#endif
	}
}
