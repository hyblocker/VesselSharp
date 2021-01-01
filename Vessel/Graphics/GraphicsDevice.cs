using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vessel.Native;

namespace Vessel
{
	public class GraphicsDevice
	{
		public GraphicsAPI GraphicsAPI = GraphicsAPI.Default;
		public IntPtr Handle;
		public VesselEngine Engine;

		public GraphicsDevice(VesselEngine engine)
		{
			Engine = engine;
		}

		#region Public API
		public void SetHandle(IntPtr handle)
		{
			Handle = handle;
			Bgfx.SetWindowHandle(handle);
		}

		public void Clear(Color color)
		{
			Bgfx.SetViewClear(0, ClearTargets.Color | ClearTargets.Depth, (int) color.PackedValueRGBA);
		}
		#endregion

		#region Internal API
		/// <summary>
		/// Initialises the GraphicsDevice
		/// </summary>
		/// <param name="graphicsAPI"></param>
		internal void Initialise(ApplicationConfig config)
		{
			GraphicsAPI = config.GraphicsAPI;
			Bgfx.Init(new InitSettings()
			{
				Backend		= (RendererBackend) config.GraphicsAPI,
				Width		= Engine.Window.Width,
				Height		= Engine.Window.Height,
				ResetFlags	= config.GetResetFlags(),
			});
		}

		/// <summary>
		/// Flushes the window parameters to the device handle
		/// </summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		/// <param name="flags"></param>
		internal void Reset(int width, int height, ResetFlags flags)
		{
			Bgfx.Reset(width, height, flags);
		}
		#endregion
	}
}
