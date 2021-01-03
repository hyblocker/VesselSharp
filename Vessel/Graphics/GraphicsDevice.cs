using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;
using VGDevice = Veldrid.GraphicsDevice;

namespace Vessel
{
	public class GraphicsDevice : IDisposable
	{
		//TODO: Keep keep a boolean per RenderTarget when bound to the viewport
		//		If is bound then bind throws exception, and most operations on it throw exception because its currently in use
		//		The currently bound to viewport id is also stored in memory, and a pointer to the RenderTarget is kept


		public GraphicsAPI GraphicsAPI = GraphicsAPI.Default;
		public IntPtr Handle;
		public VesselEngine Engine;

		internal VGDevice veldridGraphicsDevice;
		internal Pipeline veldridPipeline;
		internal CommandList veldridCommandList;

		public GraphicsDevice(VesselEngine engine)
		{
			Engine = engine;
		}

		//TODO: Remove

		private const string VertexCode = @"
#version 450
layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;
layout(location = 0) out vec4 fsin_Color;
void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";

		private const string FragmentCode = @"
#version 450
layout(location = 0) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;
void main()
{
    fsout_Color = fsin_Color;
}";

		private Shader[] _shaders;

		#region Public API
		/// <summary>
		/// Clears the backbuffer to the specified color
		/// </summary>
		/// <param name="color"></param>
		public void Clear(Color color)
		{
			veldridCommandList.ClearColorTarget(0, color.ToVeldrid());
		}

		/// <summary>
		/// Changes the graphics API to specified graphics API.
		/// Returns whether or not the API was successfully changed.
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public bool SetGraphicsAPI(GraphicsAPI api)
		{
			if (api != GraphicsAPI)
			{
				//TODO:
			}
			return false;
		}
		#endregion

		#region Internal API
		/// <summary>
		/// Initialises the GraphicsDevice
		/// </summary>
		/// <param name="graphicsAPI"></param>
		internal void Initialise(VesselWindow window, ApplicationConfig config)
		{
			GraphicsAPI = config.GraphicsAPI;

			GraphicsDeviceOptions options = new GraphicsDeviceOptions
			{
				PreferStandardClipSpaceYDirection = true,
				PreferDepthRangeZeroToOne = true,
				SyncToVerticalBlank = config.VSync,
				Debug = config.DebugMode,
			};

			if (config.GraphicsAPI == GraphicsAPI.Default)
				veldridGraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.WindowInternal, options);
			else
				veldridGraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.WindowInternal, options, (GraphicsBackend) config.GraphicsAPI);

			//Create the Graphics Resources
			ResourceFactory factory = veldridGraphicsDevice.ResourceFactory;

			//Shaders
			ShaderDescription vertexShaderDesc = new ShaderDescription(
				ShaderStages.Vertex,
				Encoding.UTF8.GetBytes(VertexCode),
				"main");
			ShaderDescription fragmentShaderDesc = new ShaderDescription(
				ShaderStages.Fragment,
				Encoding.UTF8.GetBytes(FragmentCode),
				"main");

			_shaders = factory.CreateFromSpirv(vertexShaderDesc, fragmentShaderDesc);

			// Create pipeline
			GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
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
				vertexLayouts: new VertexLayoutDescription[] { VertexPositionColor.VertexLayout },
				shaders: _shaders);
			pipelineDescription.Outputs = veldridGraphicsDevice.SwapchainFramebuffer.OutputDescription;

			veldridPipeline = factory.CreateGraphicsPipeline(pipelineDescription);

			veldridCommandList = factory.CreateCommandList();
		}

		/// <summary>
		/// Disposes of all graphics objects and shuts down the renderer
		/// </summary>
		public void Dispose()
		{
			veldridCommandList.Dispose();
			veldridGraphicsDevice.Dispose();
			foreach(var shader in _shaders)
			{
				shader.Dispose();
			}
		}

		/// <summary>
		/// Binds a viewport to window framebuffer
		/// </summary>
		/// <param name="v"></param>
		/// <param name="rectangle"></param>
		internal void BindToWindowBuffer()
		{
			veldridCommandList.SetFramebuffer(veldridGraphicsDevice.SwapchainFramebuffer);
		}

		/// <summary>
		/// Resets the graphics device's temporary buffers which may be disabled if performance is desired
		/// </summary>
		internal void OnFrameBegin()
		{
			// Begin() must be called before commands can be issued.
			veldridCommandList.Begin();
			BindToWindowBuffer();
		}

		/// <summary>
		/// Submits the queued draw calls to the GPU
		/// </summary>
		internal void OnFrameEnd()
		{
			// End() must be called before commands can be submitted for execution.
			veldridCommandList.End();
			veldridGraphicsDevice.SubmitCommands(veldridCommandList);

			// Once commands have been submitted, the rendered image can be presented to the application window.
			veldridGraphicsDevice.SwapBuffers();
		}
		#endregion
	}
}
