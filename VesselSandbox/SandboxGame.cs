using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vessel;

namespace VesselSandbox
{
	public class SandboxGame : VesselEngine
	{
		public SandboxGame() : 
			base(new ApplicationConfig()
			{
				Name = "Hello World",
				Width = 1280,
				Height = 720,
				GraphicsAPI = GraphicsAPI.Vulkan,
			})
		{
			GraphicsDevice = new GraphicsDevice(this);
		}

		public override void Initialise()
		{
			base.Initialise();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw()
		{
			base.Draw();
			GraphicsDevice.Clear(new Color(255, 255, 0));
		}
	}
}
