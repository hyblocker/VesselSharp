namespace Vessel
{
	// TODO: struct
	public class ApplicationConfig
	{
		public string Name = "Vessel Engine";
		public int Width = 1280;
		public int Height = 720;
		public bool VSync = true;
		public bool Fullscreen = false;
		/// <summary>
		/// Whether to use RenderDoc or not
		/// </summary>
		public bool RenderDoc = false;

		public GraphicsAPI GraphicsAPI = GraphicsAPI.Default;


#if DEBUG
		public bool DebugMode = true;
#else
		public bool DebugMode = false;
#endif
	}
}
