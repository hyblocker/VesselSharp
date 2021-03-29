using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Vessel.Debug;
using Vessel.Graphics;

namespace Vessel
{
	public class VesselEngine : IDisposable
	{
		#region Variables

		/// <summary>
		/// The singleton instance of the first <see cref="VesselEngine"/> created
		/// </summary>
		public static VesselEngine Instance { get; private set; }

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

		/// <summary>
		/// The group of <see cref="RenderLayer"/>s the engine will render after Rendering the entire <see cref="Scene"/>
		/// </summary>
		public static RenderLayerCollection RenderLayers;

		/// <summary>
		/// The logger that Vessel will use to log events to.
		/// </summary>
		public ILogger Logger;

		/// <summary>
		/// The target time in seconds between one frame and another
		/// </summary>
		public static double TargetFrameTime = 1.0 / 60.0;

		/// <summary>
		/// Whether or not to lock the framerate to <see cref="TargetFrameTime"/>
		/// </summary>
		public static bool LimitFrameRate = false;

		/// <summary>
		/// The time elapsed since the last frame, in seconds
		/// </summary>
		public float DeltaTime { get; private set; }

		/// <summary>
		/// The currently loaded scene
		/// </summary>
		public Scene Scene
		{
			get
			{
				return activeScene;
			}
			set
			{
				nextScene = value;
			}
		}

		private Scene activeScene;
		private Scene nextScene;

		private ApplicationConfig config;

		#endregion

		public VesselEngine(ApplicationConfig config)
		{
			//Only the first instance will be static, all other instances won't
			if (Instance == null)
			{
				VesselEngine.Instance = this;
			}

			this.config = config;
			EnableLogging = DebugMode = config.DebugMode;

			//Initialise the logger
			Logger = new Logger();
			new VesselLogger(this);

			//Set the garbage collector to low latency mode
			GCSettings.LatencyMode = GCLatencyMode.SustainedLowLatency;

			// Initialise RenderLayers
			RenderLayers = new RenderLayerCollection();
		}

		/// <summary>
		/// Initialises Vessel and starts the game loop
		/// </summary>
		public unsafe void Run()
		{
			// Is it safe to not run these?
			//Veldrid.Sdl2.SDL_version version;
			//Veldrid.Sdl2.Sdl2Native.SDL_GetVersion(&version);

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
			//Push update methods to the scene
			if (activeScene != null)
			{
				activeScene.PreUpdate();
				activeScene.Update();
				activeScene.PostUpdate();
				RenderLayers.Update();
			}

			//Loading of the next scene
			if (activeScene != nextScene)
			{
				if (activeScene != null)
				{
					activeScene.End();
					//To clean up resources
					activeScene.Dispose();
				}
				activeScene = nextScene;
				if (activeScene != null)
				{
					activeScene.Initialise();
				}
			}
		}

		/// <summary>
		/// Called every frame. Used to push draw calls to the screen
		/// </summary>
		public virtual void Draw()
		{
			Renderer?.Draw(activeScene);
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

			// Initialise RenderDoc
			if (config.RenderDoc)
			{
				RenderDoc.Initialise();
			}

			//Initialise the Graphics Device
			GraphicsDevice.Initialise(window, config);
			Window.Invalidate();

			//Callback for the engine
			Initialise();

			VesselLogger.Logger.Info($"Started Vessel using {GraphicsDevice.GraphicsAPI}!");

			//Deltatime variables
			long previousFrameTicks = 0;
			Stopwatch sw = new Stopwatch();
			sw.Start();

			//Main Loop
			while (Window.Exists)
			{
				Window.ProcessEvents();

				//Calculate delta-time
				long currentFrameTicks = sw.ElapsedTicks;
				double deltaSeconds = (currentFrameTicks - previousFrameTicks) / (double)Stopwatch.Frequency;

				//Wait till next frame if we are locking the target framerate
				while (LimitFrameRate && deltaSeconds < TargetFrameTime)
				{
					currentFrameTicks = sw.ElapsedTicks;
					deltaSeconds = (currentFrameTicks - previousFrameTicks) / (double)Stopwatch.Frequency;
				}

				DeltaTime = (float) deltaSeconds;

				previousFrameTicks = currentFrameTicks;

				//Update method
				Update();

				//Only Draw if the window is still open
				if (!Window.Exists)
				{
					break;
				}

				GraphicsDevice.OnFrameBegin();

				// Draw the scene
				GraphicsDevice.Debug.PushGroup("Draw Scene");
				Draw();
				GraphicsDevice.Debug.PopGroup();

				// Draw layers + post processing
				GraphicsDevice.Debug.PushGroup("Draw Layers");
				RenderLayers.Draw();
				GraphicsDevice.Debug.PopGroup();

				GraphicsDevice.OnFrameEnd();
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
