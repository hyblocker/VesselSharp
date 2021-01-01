using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public class ApplicationConfig
	{
		public string Name = "Vessel Engine";
		public int Width = 1280;
		public int Height = 720;
		public bool VSync = true;

		public GraphicsAPI GraphicsAPI = GraphicsAPI.Default;

		internal Vessel.Native.ResetFlags GetResetFlags()
		{
			Vessel.Native.ResetFlags flags = Vessel.Native.ResetFlags.None;

			if (VSync)
				flags |= Vessel.Native.ResetFlags.Vsync;

			return flags;
		}

#if DEBUG
		public bool DebugMode = true;
#else
		public bool DebugMode = false;
#endif
	}
}
