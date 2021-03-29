using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;
using Vessel.Assets;

using ShaderProgram = Veldrid.Shader;
using VGraphicsDevice = Veldrid.GraphicsDevice;

namespace Vessel
{
	//TODO: Shader pool

	public class Shader : ShaderBase
	{
		public Shader(GraphicsDevice graphicsDevice, string path) : base(graphicsDevice)
		{
			// TODO: Load from file
			GraphicsDevice = graphicsDevice;
			AssetPath = path;

			// Initialise pipeline
			pipelineDescription = new GraphicsPipelineDescription();

			// TODO: Combine shaders into a single binary object, one where both the reflection data, and shader code are provided
			byte[] vert = AssetManager.FetchAssetBytes($"{path}.vert.spv");
			byte[] frag = AssetManager.FetchAssetBytes($"{path}.frag.spv");

			//Loads a SPIR-V binary from the byte[], and attempts to use SPIR-V cross to cross compile it for other platforms if the current platform isn't Vulkan
			Programs = graphicsDevice.veldridGraphicsDevice.ResourceFactory.CreateFromSpirv(
				new ShaderDescription(ShaderStages.Vertex, vert, "main", VesselEngine.DebugMode),
				new ShaderDescription(ShaderStages.Fragment, frag, "main", VesselEngine.DebugMode));

			CreatePipelineDescription();
			CrossCompileShader();

			shaderPipeline = veldridGraphicsDevice.ResourceFactory.CreateGraphicsPipeline(pipelineDescription);
		}

		public override void Apply()
		{
			//TODO: Pipeline is constructed with the shader technique
			//TODO: Set pipeline from shader

			pipelineDescription.ShaderSet = new ShaderSetDescription(
				vertexLayouts: new VertexLayoutDescription[]
				{
					// TODO: GET FROM SPIRV JFC
					VertexPositionColor.VertexLayout,
				},
				shaders: Programs);

			//TODO: Check if the pipeline needs to be invalidated
			if (!shaderPipeline.IsDisposed)
			{
				shaderPipeline.Dispose();
			}
			shaderPipeline = veldridGraphicsDevice.ResourceFactory.CreateGraphicsPipeline(pipelineDescription);

			//TODO: Apply shader to pipeline and regenerate or pull from cache
			GraphicsDevice.veldridCommandList.SetPipeline(shaderPipeline);
		}

		public override void Dispose()
		{
			foreach (var shader in Programs)
			{
				shader.Dispose();
			}
			shaderPipeline.Dispose();
		}

		public override void Unload()
		{
			Dispose();
			// TODO: Unload from ShaderCache
		}

		/// <summary>
		/// Generates the pipeline description
		/// </summary>
		private void CreatePipelineDescription()
		{
			// TODO: Properties to play with all these values
			pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;
			pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
				depthTestEnabled: true,
				depthWriteEnabled: true,
				comparisonKind: ComparisonKind.LessEqual);

			pipelineDescription.RasterizerState = new RasterizerStateDescription(
				cullMode: FaceCullMode.Back,
				fillMode: PolygonFillMode.Solid,
				frontFace: FrontFace.Clockwise,
				depthClipEnabled: true,
				scissorTestEnabled: false);

			pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleStrip;
			pipelineDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();
			pipelineDescription.ShaderSet = new ShaderSetDescription(
				vertexLayouts: new VertexLayoutDescription[]
				{
					// TODO: Load from SPIR-V reflection metadata
					VertexPositionColor.VertexLayout,
				},
				shaders: Programs);

			// TODO: GraphicsDevice.FrameBuffer.InternalFramebuffer.OutputDescription => gets the output description of the currently bound framebuffer
			pipelineDescription.Outputs = veldridGraphicsDevice.SwapchainFramebuffer.OutputDescription;
		}

		#region Shader Compilation & Reflection

		private void CrossCompileShader()
		{
			// Attempt to create a cross compile shader target where we use Y-Up, and DirectX Clip Space Coords
			//var options = new CrossCompileOptions(true, true, false);
			//var target = GetCompilationTarget(factory.BackendType);
			//var compilationResult = SpirvCompilation.CompileVertexFragment(
			//	vertexShaderDescription.ShaderBytes,
			//	fragmentShaderDescription.ShaderBytes,
			//	target,
			//	options);
		}

		private static CrossCompileTarget GetCompilationTarget(GraphicsBackend backend)
		{
			switch (backend)
			{
				case GraphicsBackend.Direct3D11:
					return CrossCompileTarget.HLSL;
				case GraphicsBackend.OpenGL:
					return CrossCompileTarget.GLSL;
				case GraphicsBackend.Metal:
					return CrossCompileTarget.MSL;
				case GraphicsBackend.OpenGLES:
					return CrossCompileTarget.ESSL;
				default:
					throw new SpirvCompilationException($"Invalid GraphicsBackend: {backend}");
			}
		}

		#endregion
	}
}
