using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Vessel
{
	public class VesselWindow
	{
		public bool Exists => WindowInternal.Exists;
		public IntPtr Handle => WindowInternal.Handle;
		public string Name
		{
			get { return NameInternal; }
			set
			{
				NameInternal = value;
				Invalidate();
			}
		}
		public int Width
		{
			get { return WidthInternal; }
			set 
			{ 
				WidthInternal = value;
				Invalidate();
			}
		}
		public int Height
		{
			get { return HeightInternal; }
			set
			{
				HeightInternal = value;
				Invalidate();
			}
		}

		private string NameInternal;
		private int WidthInternal;
		private int HeightInternal;

		internal Sdl2Window WindowInternal;

		internal VesselWindow(GraphicsDevice device, string name, int windowWidth, int windowHeight)
		{
			Thread.CurrentThread.Name = "OS Thread";

			WidthInternal = windowWidth;
			HeightInternal = windowHeight;
			NameInternal = name;

			WindowCreateInfo windowCI = new WindowCreateInfo()
			{
				X = Sdl2Native.SDL_WINDOWPOS_CENTERED,
				Y = Sdl2Native.SDL_WINDOWPOS_CENTERED,
				WindowWidth = windowWidth,
				WindowHeight = windowHeight,
				WindowInitialState = Veldrid.WindowState.Normal,
				WindowTitle = name,
			};

			WindowInternal = VeldridStartup.CreateWindow(ref windowCI);
		}

		internal void ProcessEvents()
		{
			WindowInternal.PumpEvents();
		}

		/// <summary>
		/// Updates the window with the new properties
		/// </summary>
		internal void Invalidate()
		{
			//Set the window text
			WindowInternal.Title = NameInternal;

			//Sync the window bounds
			
			WindowInternal.Width = WidthInternal;
			WindowInternal.Height = HeightInternal;
		}
	}
}
