using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Vessel
{
	public class VesselWindow : IDisposable
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
		
		//Pointer to the Window Icon
		private IntPtr WindowIcon;

		const string IconFileName = "Icon.bmp";

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

			// TODO: Abstract to internal window, SDL is native
			if (TryFetchIcon())
			{
				Sdl2Native.SDL_SetWindowIcon(WindowInternal.SdlWindowHandle, WindowIcon);
			}
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

		private bool TryFetchIcon()
		{
			// TODO: Abstract to internal window, SDL is native
			if (Assembly.GetEntryAssembly() != null)
			{
				//Get all resource files in the entry assembly
				string[] embeddedResources = Assembly.GetEntryAssembly().GetManifestResourceNames();
				string selectedFile = null;

				//Find the file ending with IconFileName
				foreach (string resource in embeddedResources)
				{
					if (resource.EndsWith(IconFileName))
					{
						selectedFile = resource;
						break;
					}
				}

				//Stream the icon bitmap from the assembly
				using (
					var stream =
						Assembly.GetEntryAssembly().GetManifestResourceStream(selectedFile) ??
						Assembly.GetEntryAssembly().GetManifestResourceStream(Assembly.GetEntryAssembly().EntryPoint.DeclaringType.Namespace + "." + IconFileName) ??
						Assembly.GetEntryAssembly().GetManifestResourceStream(Assembly.GetEntryAssembly().GetName().Name + "." + IconFileName) ??
						Assembly.GetEntryAssembly().GetManifestResourceStream(IconFileName) ??
						Assembly.GetExecutingAssembly().GetManifestResourceStream("Vessel.bmp") ??
						Assembly.GetExecutingAssembly().GetManifestResourceStream("Vessel.Vessel.bmp"))
				{
					if (stream != null)
					{
						using (var br = new BinaryReader(stream))
						{
							try
							{
								var src = Sdl2Native.RwFromMem(br.ReadBytes((int)stream.Length), (int)stream.Length);
								WindowIcon = Sdl2Native.LoadBMP_RW(src, 1);
							}
							catch
							{
								Logger.Log.Fatal("Failed to load icon, not using icon");
							}
						}
					}
				}

				return WindowIcon != IntPtr.Zero;
			}

			return false;
		}

		public void Dispose()
		{
			// TODO: Abstract to internal window, SDL is native
			//Dispose the icon if it were ever loaded
			if (WindowIcon != IntPtr.Zero)
			{
				Sdl2Native.FreeSurface(WindowIcon);
			}
		}
	}
}
