using System;
using System.Collections.Generic;
using System.Text;

namespace Vessel.Graphics
{
	public class RenderLayerCollection
	{
		// TODO: RenderLayers can take a framebuffer in and return a framebuffer
		//       Consider a frame buffer manager or something?
		private List<RenderLayer> renderLayers;

		public RenderLayerCollection()
		{
			renderLayers = new List<RenderLayer>();
		}

		public void Add(RenderLayer layer)
		{
			renderLayers.Add(layer);
		}
		
		public void Update()
		{
			for (int i = 0; i < renderLayers.Count; i++)
			{
				renderLayers[i].Update();
			}
		}

		public void Draw()
		{
			for (int i = 0; i < renderLayers.Count; i++)
			{
				renderLayers[i].Draw();
			}
		}
	}
}
