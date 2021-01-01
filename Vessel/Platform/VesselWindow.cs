using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vessel.Native;

namespace Vessel
{
	public class VesselWindow
	{
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

		internal Thread ThreadInternal;
		internal NativeWindow WindowInternal;

		internal VesselWindow(GraphicsDevice device, string name, int windowWidth, int windowHeight)
		{
			Thread.CurrentThread.Name = "OS Thread";

			WidthInternal = windowWidth;
			HeightInternal = windowHeight;
			NameInternal = name;

			WindowInternal = new NativeWindow(name, windowWidth, windowHeight);
			WindowInternal.Show();


			Bgfx.SetWindowHandle(WindowInternal.Handle);
		}

		internal VesselWindow(GraphicsDevice device, IntPtr handle)
		{
			WindowInternal = new NativeWindow(handle);
			Bgfx.SetWindowHandle(handle);
		}

		internal void Run(Action<VesselWindow> renderThread)
		{
			ThreadInternal = new Thread(() => {
				renderThread(this);
				WindowInternal.Close();
			});
			ThreadInternal.Start();

			// run the native OS message loop on this thread
			// this blocks until the window closes and the loop exits
			WindowInternal.RunMessageLoop();

			// wait for the render thread to finish
			ThreadInternal.Join();
		}

		internal bool ProcessEvents(ResetFlags resetFlags)
		{
			WindowEvent? ev;
			var reset = false;

			while ((ev = WindowInternal.Poll()) != null)
			{
				var e = ev.Value;
				switch (e.Type)
				{
					case WindowEventType.Exit:
						return false;

					case WindowEventType.Size:
						WidthInternal = e.Width;
						HeightInternal = e.Height;
						reset = true;
						break;
				}
			}

			if (reset)
				Bgfx.Reset(WidthInternal, HeightInternal, resetFlags);

			return true;
		}

		/// <summary>
		/// Updates the window with the new properties
		/// </summary>
		internal void Invalidate()
		{
			//Set the window text
			WindowInternal.SetText(NameInternal);

			//Sync the window bounds

		}
	}
}
