using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vessel;

namespace VesselSandbox
{
	public class SandboxGame : VesselEngine
	{
		VertexBuffer<VertexPositionColor> vertexBuffer;
		IndexBuffer indexBuffer;
		ShaderTechnique Shader;

		public SandboxGame() :
			base (new ApplicationConfig()
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

			// Load the shaders from disk
			Shader = new ShaderTechnique(GraphicsDevice,
				System.IO.File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\VesselSharp\ShaderTests\ShaderTest0.vert.spv"),
				System.IO.File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\VesselSharp\ShaderTests\ShaderTest0.frag.spv"),
				"ShaderTest0");

			// Ideally, once shader binaries and assets are properly abstracted we use this:
			//Shader = new ShaderTechnique(GraphicsDevice, "ShaderTest0");

			// Define the model's vertices and indices
			VertexPositionColor[] quadVertices =
			{
				new VertexPositionColor(new Vector2(-.75f, .75f), Color.Red),
				new VertexPositionColor(new Vector2(.75f, .75f), Color.Green),
				new VertexPositionColor(new Vector2(-.75f, -.75f), Color.Blue),
				new VertexPositionColor(new Vector2(.75f, -.75f), Color.Yellow)
			};
			ushort[] quadIndices = { 0, 1, 2, 3 };

			//Create a vertex buffer
			vertexBuffer = new VertexBuffer<VertexPositionColor>(
				GraphicsDevice, 
				new VertexPositionColor(), 
				quadVertices.Length, 
				VertexPositionColor.SizeInBytes);

			//Set the data of the vertex buffer to the vertices
			vertexBuffer.SetData(0, quadVertices);

			//Create an index buffer
			indexBuffer = new IndexBuffer(
				GraphicsDevice, 
				quadIndices.Length, 
				IndexFormat.UInt16);
			
			//Set the data of the index buffer to the indices
			indexBuffer.SetData(0, quadIndices);
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw()
		{
			base.Draw();
			
			GraphicsDevice.Clear(Color.Black);
			GraphicsDevice.BindBuffer(vertexBuffer);
			GraphicsDevice.BindBuffer(indexBuffer);
			Shader.Apply();
			GraphicsDevice.DrawIndexed(indexBuffer.Size, 1, 0, 0, 0);
			
		}
	}
}
