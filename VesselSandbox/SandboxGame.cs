using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vessel;
using Vessel.Assets;
using Vessel.Debug;
using Vessel.Graphics;

namespace VesselSandbox
{
	public class SandboxGame : VesselEngine
	{
		VertexBuffer<VertexPositionColor> vertexBuffer;
		IndexBuffer indexBuffer;
		//ShaderTechnique Shader;
		Shader Shader;

		public SandboxGame() :
			base (new ApplicationConfig()
			{
				Name = "Vessel Sandbox",
				Width = 1280,
				Height = 720,
				GraphicsAPI = GraphicsAPI.Direct3D11,
				RenderDoc = true,
			})
		{
			GraphicsDevice = new GraphicsDevice(this);

			AssetManager.Initialise();
		}

		public override void Initialise()
		{
			base.Initialise();

			//Setup the render as a forward renderer
			Renderer = new RendererForward();
			
			//Load a scene
			Scene = new DummyScene();

			// Load the shaders from disk
			Shader = new Shader(GraphicsDevice, "ShaderTest0");

			// Define the model's vertices and indices
			VertexPositionColor[] quadVertices =
			{
				new VertexPositionColor(new Vector2(-.75f,  .75f), Color.Red   ),
				new VertexPositionColor(new Vector2( .75f,  .75f), Color.Green ),
				new VertexPositionColor(new Vector2(-.75f, -.75f), Color.Blue  ),
				new VertexPositionColor(new Vector2( .75f, -.75f), Color.Yellow)
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

			// Register the ImGUI Layer
			RenderLayers.Add(new ImGUILayer(GraphicsDevice));

			// Init RenderDoc
			// RenderDoc.LaunchReplayUI();
		}

		public override void Update()
		{
			base.Update();
		}

		public override void Draw()
		{
			base.Draw();

			GraphicsDevice.Debug.PushGroup("Test");

			GraphicsDevice.Clear(Color.Black);
			GraphicsDevice.Debug.Marker("Binding Mesh");
			GraphicsDevice.BindBuffer(vertexBuffer);
			GraphicsDevice.BindBuffer(indexBuffer);
			Shader.Apply();
			GraphicsDevice.DrawIndexed(indexBuffer.Size, 1, 0, 0, 0);
			
			GraphicsDevice.Debug.PopGroup();
		}
	}
}
