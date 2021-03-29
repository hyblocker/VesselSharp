using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.Sdl2;
using Veldrid.StartupUtilities;

namespace Vessel
{
	public class VesselWindow : IDisposable
	{
		#region Events

		public event Action Resized;
		
		#endregion

		#region Properties

		public bool Exists => WindowInternal.Exists;
		public IntPtr Handle => WindowInternal.Handle;
		public bool Resizable => WindowInternal.Resizable;
		public bool Focused => WindowInternal.Focused;

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

		#endregion

		private string NameInternal;
		private int WidthInternal;
		private int HeightInternal;

		internal Sdl2Window WindowInternal;
		internal InputSnapshot InputSnapshot;

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

			//Hook events, we need to override
			WindowInternal.Resized += () =>
			{
				// Update the dimensions for APIs to interact correctly
				WidthInternal = WindowInternal.Width;
				HeightInternal = WindowInternal.Height;
				// Update the framebuffer
				device.veldridGraphicsDevice.MainSwapchain.Resize((uint)WidthInternal, (uint)HeightInternal);
				// Update the subscribed events
				Resized?.Invoke();
			};

			//TODO: Input hooks
			//WindowInternal.KeyDown += FocusGained;
			//WindowInternal.KeyUp += FocusLost;

			// TODO: Abstract to internal window, SDL is native
			if (TryFetchIcon())
			{
				SDL_SetWindowIcon(WindowInternal.SdlWindowHandle, WindowIcon);
			}
		}

		internal void ProcessEvents()
		{
			InputSnapshot = WindowInternal.PumpEvents();
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
								var src = RwFromMem(br.ReadBytes((int)stream.Length), (int)stream.Length);
								WindowIcon = LoadBMP_RW(src, 1);
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
				FreeSurface(WindowIcon);
			}
		}

		#region SDL Native


		//   ICONS

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void SDL_SetWindowIcon_t(IntPtr SDL2Window, IntPtr icon);
		private static SDL_SetWindowIcon_t s_setWindowIcon = Sdl2Native.LoadFunction<SDL_SetWindowIcon_t>("SDL_SetWindowIcon");
		public static void SDL_SetWindowIcon(IntPtr Sdl2Window, IntPtr icon) => s_setWindowIcon(Sdl2Window, icon);

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr SDL_rwfrommem(byte[] mem, int size);
		private static SDL_rwfrommem s_sdl_rwfrommem = Sdl2Native.LoadFunction<SDL_rwfrommem>("SDL_RWFromMem");
		public static IntPtr RwFromMem(byte[] mem, int size) { return s_sdl_rwfrommem(mem, size); }

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr SDL_loadbmp_rw(IntPtr src, int freesrc);
		private static SDL_loadbmp_rw s_sdl_loadbmp_rw = Sdl2Native.LoadFunction<SDL_loadbmp_rw>("SDL_LoadBMP_RW");
		public static IntPtr LoadBMP_RW(IntPtr src, int freesrc) { return s_sdl_loadbmp_rw(src, freesrc); }

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate IntPtr SDL_freesurface(IntPtr surface);
		private static SDL_freesurface s_sdl_freesurface = Sdl2Native.LoadFunction<SDL_freesurface>("SDL_FreeSurface");
		public static IntPtr FreeSurface(IntPtr surface) { return s_sdl_freesurface(surface); }

		#endregion
	}
}
