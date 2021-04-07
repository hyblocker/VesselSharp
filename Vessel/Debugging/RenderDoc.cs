using RDoc = Veldrid.RenderDoc;

namespace Vessel.Debug
{
	/// <summary>
	/// A class for interacting with the RenderDoc API
	/// <para></para>
	/// Note: Does not compile under Release if not using the <c>RENDERDOC_ENABLED</c> preprocessor directive
	/// </summary>
	public static class RenderDoc
	{
		/// <summary>
		/// Whether we can interface with RenderDoc
		/// </summary>
		public static bool IsLoaded { get; private set; } = false;

		private static RDoc renderDocInternal;

		/// <summary>
		/// Initialises the RenderDoc API
		/// </summary>
		/// <returns>Whether RenderDoc initialised successfully</returns>
		public static bool Initialise()
		{
			IsLoaded = RDoc.Load(out renderDocInternal);
			HookIntoChildren = true;
			CaptureAllCmdLists = true;
			return IsLoaded;
		}

		/// <summary>
		/// Initialises the RenderDoc API
		/// </summary>
		/// <param name="path">The path to the RenderDoc shared library (<c>renderdoc.dll / librenderdoc.dylib / librenderdoc.so</c>)</param>
		/// <returns>Whether RenderDoc initialised successfully</returns>
		public static bool Initialise(string path)
		{
			var success = RDoc.Load(path, out renderDocInternal);
			HookIntoChildren = true;
			CaptureAllCmdLists = true;
			return IsLoaded;
		}

		/// <summary>
		/// Starts the replay UI
		/// </summary>
		public static void LaunchReplayUI()
		{
			renderDocInternal.LaunchReplayUI();
		}

		/// <summary>
		/// Starts the replay UI
		/// </summary>
		/// <param name="args">The arguments to launch the replay UI with</param>
		public static void LaunchReplayUI(string args)
		{
			renderDocInternal.LaunchReplayUI(args);
		}

		/// <summary>
		/// Stop capturing the current frame
		/// </summary>
		/// <returns>Whether the operation was successful or not</returns>
		public static bool EndFrameCapture()
		{
			return renderDocInternal.EndFrameCapture();
		}

		/// <summary>
		/// Whether the current frame is being captured
		/// </summary>
		public static bool IsFrameCapturing()
		{
			return renderDocInternal.IsFrameCapturing();
		}

		/// <summary>
		/// Whether the target control is connected
		/// </summary>
		/// <returns></returns>
		public static bool IsTargetControlConnected()
		{
			return renderDocInternal.IsTargetControlConnected();
		}

		/// <summary>
		/// Sets the directory in which RenderDoc will save captures to
		/// </summary>
		/// <param name="path"></param>
		public static void SetCaptureSavePath(string path)
		{
			renderDocInternal.SetCaptureSavePath(path);
		}

		/// <summary>
		/// Start capturing the current frame
		/// </summary>
		public static void StartFrameCapture()
		{
			renderDocInternal.StartFrameCapture();
		}

		/// <summary>
		/// Captures the specified amount of frames
		/// </summary>
		/// <param name="numFrames">How many frames to capture</param>
		public static void TriggerCapture(uint numFrames)
		{
			renderDocInternal.TriggerCapture(numFrames);
		}

		/// <summary>
		/// Captures a single frame
		/// </summary>
		public static void TriggerCapture()
		{
			renderDocInternal.TriggerCapture();
		}

		/// <summary>
		/// Whether RenderDoc will validate API uUsage
		/// </summary>
		public static bool APIValidation
		{
			get
			{
				return renderDocInternal.APIValidation;
			}
			set
			{
				renderDocInternal.APIValidation = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc will overlay the framerate
		/// </summary>
		public static bool OverlayFrameRate
		{
			get
			{
				return renderDocInternal.OverlayFrameRate;
			}
			set
			{
				renderDocInternal.OverlayFrameRate = value;
			}
		}

		/// <summary>
		/// Whether the RenderDoc overlay is enabled or not
		/// </summary>
		public static bool OverlayEnabled
		{
			get
			{
				return renderDocInternal.OverlayEnabled;
			}
			set
			{
				renderDocInternal.OverlayEnabled = value;
			}
		}

		/// <summary>
		/// The amount of captures we've done in RenderDoc
		/// </summary>
		public static uint CaptureCount
		{
			get
			{
				return renderDocInternal.CaptureCount;
			}
		}

		/// <summary>
		/// Whether debug outputting is to be muted or not
		/// </summary>
		public static bool DebugOutputMute
		{
			get
			{
				return renderDocInternal.DebugOutputMute;
			}
			set
			{
				renderDocInternal.DebugOutputMute = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc will capture all command lists
		/// </summary>
		public static bool CaptureAllCmdLists
		{
			get
			{
				return renderDocInternal.CaptureAllCmdLists;
			}
			set
			{
				renderDocInternal.CaptureAllCmdLists = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc will reference all resources
		/// </summary>
		public static bool RefAllResources
		{
			get
			{
				return renderDocInternal.RefAllResources;
			}
			set
			{
				renderDocInternal.RefAllResources = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc should hook into child processes
		/// </summary>
		public static bool HookIntoChildren
		{
			get
			{
				return renderDocInternal.HookIntoChildren;
			}
			set
			{
				renderDocInternal.HookIntoChildren = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc should verify buffer access
		/// </summary>
		public static bool VerifyBufferAccess
		{
			get
			{
				return renderDocInternal.VerifyBufferAccess;
			}
			set
			{
				renderDocInternal.VerifyBufferAccess = value;
			}
		}

		/// <summary>
		/// How long to wait until capturing frames in milliseconds
		/// </summary>
		public static uint DelayForDebugger
		{
			get
			{
				return renderDocInternal.DelayForDebugger;
			}
			set
			{
				renderDocInternal.DelayForDebugger = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc should only capture callstacks for drawing operations
		/// </summary>
		public static bool CaptureCallstacksOnlyDraws
		{
			get
			{
				return renderDocInternal.CaptureCallstacksOnlyDraws;
			}
			set
			{
				renderDocInternal.CaptureCallstacksOnlyDraws = value;
			}
		}

		/// <summary>
		/// Whether RenderDoc should capture callstacks
		/// </summary>
		public static bool CaptureCallstacks
		{
			get
			{
				return renderDocInternal.CaptureCallstacks;
			}
			set
			{
				renderDocInternal.CaptureCallstacks = value;
			}
		}

		/// <summary>
		/// Whether the frame number is visible in the overlay
		/// </summary>
		public static bool OverlayFrameNumber
		{
			get
			{
				return renderDocInternal.OverlayFrameNumber;
			}
			set
			{
				renderDocInternal.OverlayFrameNumber = value;
			}
		}

		/// <summary>
		/// Whether the number of captures is visible in the overlay
		/// </summary>
		public static bool OverlayCaptureList
		{
			get
			{
				return renderDocInternal.OverlayCaptureList;
			}
			set
			{
				renderDocInternal.OverlayCaptureList = value;
			}
		}

		/// <summary>
		/// Whether the game is allowed to use VSync while being captured by RenderDoc
		/// </summary>
		public static bool AllowVSync
		{
			get
			{
				return renderDocInternal.AllowVSync;
			}
			set
			{
				renderDocInternal.AllowVSync = value;
			}
		}

		/// <summary>
		/// Whether the game can use Fullscreen while being captured by RenderDoc
		/// </summary>
		public static bool AllowFullscreen
		{
			get
			{
				return renderDocInternal.AllowFullscreen;
			}
			set
			{
				renderDocInternal.AllowFullscreen = value;
			}
		}
	}
}
