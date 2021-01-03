using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public class VesselEngine : IDisposable
	{
		/// <summary>
		/// The window of this engine instance
		/// </summary>
		public VesselWindow Window;

		/// <summary>
		/// The graphics device responsible for driving this instance of the engine
		/// </summary>
		public GraphicsDevice GraphicsDevice;

		/// <summary>
		/// Whether or not Vessel will log events to the logger
		/// </summary>
		public bool EnableLogging
		/*{
			get
			{
				return (Logger != null && typeof(Logger) == Logger.GetType()) ? ((Logger)Logger).DoEngineLogging : false;
			}
			set
			{
				if (typeof(Logger) == Logger.GetType())
					((Logger)Logger).DoEngineLogging = value;
			}
			
		}*/
		;
		public ILogger Logger;

		private ApplicationConfig config;

		public VesselEngine(ApplicationConfig config)
		{
			this.config = config;
		}

		/// <summary>
		/// Initialises vessel and starts the game loop
		/// </summary>
		public void Run()
		{
			Initialise();
			MainLoop();
		}

		#region Public API

		/// <summary>
		/// Called before running any frames, and after the graphics API was setup
		/// Use this method to load data and initialise your game
		/// </summary>
		public virtual void Initialise()
		{
			Logger = new Logger();
		}
		
		/// <summary>
		/// Called every frame. Used to process the game logic before drawing
		/// </summary>
		public virtual void Update()
		{

		}

		/// <summary>
		/// Called every frame. Used to push draw calls to the screen
		/// </summary>
		public virtual void Draw()
		{


		}

		public virtual void Exiting()
		{

		}

		#endregion

		#region Main Loop
		/// <summary>
		/// Setups the engine and initialises the window 
		/// </summary>
		internal void MainLoop()
		{
			//TODO: LOGGER
			Logger.Info("Initialising Vessel...");

			VesselWindow window;
			window = new VesselWindow(GraphicsDevice, config.Name, config.Width, config.Height);
			Window = window;

			GraphicsDevice.Initialise(window, config);
			Window.Invalidate();

			Initialise();

			Logger.Info($"Started Vessel using {GraphicsDevice.GraphicsAPI}!");

			while (Window.Exists)
			{
				Window.ProcessEvents();

				Update();

				if (Window.Exists)
				{
					GraphicsDevice.OnFrameBegin();

					Draw();

					GraphicsDevice.OnFrameEnd();
				}
			}

			// Cleanup
			Logger.Info("Shutting down Vessel...");
			Exiting();
			Dispose();
		}

		public void Dispose()
		{
			// clean up
			GraphicsDevice.Dispose();
			Logger.Close();
		}

		#endregion

	}
}
