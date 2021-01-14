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
		/// Whether the engine is in DEBUG Mode
		/// </summary>
		public static bool DebugMode { get; private set; }

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
		public bool EnableLogging;
		
		/// <summary>
		/// A rendering path to be used by Vessel. To override with your own rendering path override <see cref="Draw"/>.
		/// </summary>
		public IRenderPath Renderer;

		public ILogger Logger;

		private ApplicationConfig config;

		public VesselEngine(ApplicationConfig config)
		{
			this.config = config;
			EnableLogging = DebugMode = config.DebugMode;

			//Initialise the logger
			Logger = new Logger();
			new VesselLogger(this);
		}

		/// <summary>
		/// Initialises vessel and starts the game loop
		/// </summary>
		public unsafe void Run()
		{
			Veldrid.Sdl2.SDL_version version;
			Veldrid.Sdl2.Sdl2Native.SDL_GetVersion(&version);

			MainLoop();
		}

#region Public API

		/// <summary>
		/// Called before running any frames, and after the graphics API was setup
		/// Use this method to load data and initialise your game
		/// </summary>
		public virtual void Initialise()
		{

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

		/// <summary>
		/// Called when the game closes, but before disposal of graphics resources.
		/// Use this to save any data and close active file handles
		/// </summary>
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
			VesselLogger.Logger.Info("Initialising Vessel...");

			//Setup the window
			VesselWindow window;
			window = new VesselWindow(GraphicsDevice, config.Name, config.Width, config.Height);
			Window = window;

			//Initialise the Graphics Device
			GraphicsDevice.Initialise(window, config);
			Window.Invalidate();

			//Callback for the engine
			Initialise();

			VesselLogger.Logger.Info($"Started Vessel using {GraphicsDevice.GraphicsAPI}!");

			//Main Loop
			while (Window.Exists)
			{
				Window.ProcessEvents();

				Update();

				//Only Draw if the window is still open
				if (Window.Exists)
				{
					GraphicsDevice.OnFrameBegin();

					Draw();

					GraphicsDevice.OnFrameEnd();
				}
			}

			// Cleanup
			VesselLogger.Logger.Info("Shutting down Vessel...");
			Exiting();
		}

		public void Dispose()
		{
			// clean up
			Window.Dispose();

			GraphicsDevice.Dispose();
			Logger.Close();

		}

#endregion

	}
}
