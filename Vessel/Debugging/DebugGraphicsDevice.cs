using System;
using System.Collections.Generic;
using System.Text;

namespace Vessel
{
	/// <summary>
	/// A graphics device that is used to call debug commands
	/// </summary>
	public class DebugGraphicsDevice
	{
		public GraphicsDevice GraphicsDevice;

		internal DebugGraphicsDevice(GraphicsDevice device)
		{
			GraphicsDevice = device;
		}

		/// <summary>
		/// Create a new debug group for use in frame debuggers such as RenderDoc
		/// </summary>
		/// <param name="name">The name of the debug group</param>
		public void PushGroup(string name)
		{
#if DEBUG
			GraphicsDevice.veldridCommandList.PushDebugGroup(name);
#endif
		}

		/// <summary>
		/// Exit the current debug group
		/// </summary>
		public void PopGroup()
		{
#if DEBUG
			GraphicsDevice.veldridCommandList.PopDebugGroup();
#endif
		}

		/// <summary>
		/// Inserts a debug marker for use in frame debuggers such as RenderDoc
		/// </summary>
		/// <param name="name">The name of the marker</param>
		public void Marker(string name)
		{
#if DEBUG
			GraphicsDevice.veldridCommandList.InsertDebugMarker(name);
#endif
		}
	}
}
