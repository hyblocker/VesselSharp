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
		private SpirvReflection refl;
		private DeviceBuffer buffer;

		public Shader(GraphicsDevice graphicsDevice, string path) : base(graphicsDevice)
		{
			// TODO: Load from file
			GraphicsDevice = graphicsDevice;
			AssetPath = path;

			// Initialise pipeline
			pipelineDescription = new GraphicsPipelineDescription();

			byte[] shaderBytes = AssetManager.FetchAssetBytes($"{path}.shdr");

			VesselShaderReader.BlobToShader(shaderBytes, out refl, out byte[][] data);

			// TODO: Blob to shader needs work, the method should also output a ShaderStages[] for the stages

			//Loads a SPIR-V binary from the byte[], and attempts to use SPIR-V cross to cross compile it for other platforms if the current platform isn't Vulkan
			Programs = graphicsDevice.veldridGraphicsDevice.ResourceFactory.CreateFromSpirv(
				new ShaderDescription(ShaderStages.Vertex, data[0], "main", VesselEngine.DebugMode),
				new ShaderDescription(ShaderStages.Fragment, data[1], "main", VesselEngine.DebugMode));

			InitPipeline();
			CrossCompileShader();
		}

		public override void Apply()
		{
			if (isDirty)
			{
				if (shaderPipeline != null && !shaderPipeline.IsDisposed)
				{
					shaderPipeline.Dispose();
				}
				shaderPipeline = veldridGraphicsDevice.ResourceFactory.CreateGraphicsPipeline(pipelineDescription);
			}

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
		/// Initializes the pipeline
		/// </summary>
		private void InitPipeline()
		{
			var factory = GraphicsDevice.veldridGraphicsDevice.ResourceFactory;

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
			//pipelineDescription.ResourceLayouts = Array.Empty<ResourceLayout>();

			// Create the resource layouts from the shader descriptions
			ResourceLayout[] resourceLayouts = new ResourceLayout[refl.ResourceLayouts.Length];
			for (int i = 0; i < resourceLayouts.Length; i++)
			{
				resourceLayouts[0] = factory.CreateResourceLayout(refl.ResourceLayouts[i]);
			}
			pipelineDescription.ResourceLayouts = resourceLayouts;

			pipelineDescription.ShaderSet = new ShaderSetDescription(
				vertexLayouts:
					new[] {
						new VertexLayoutDescription(refl.VertexElements)
					},
				shaders: Programs);

			//factory.CreateResourceSet(new ResourceSetDescription(,))

			System.Diagnostics.Debugger.Break();

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
