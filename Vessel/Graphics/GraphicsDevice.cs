using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;
using Veldrid.SPIRV;
using Veldrid.StartupUtilities;
using VGDevice = Veldrid.GraphicsDevice;
using ShaderProgram = Veldrid.Shader;

namespace Vessel
{
	public class GraphicsDevice : IDisposable
	{
		//TODO: Keep keep a boolean per RenderTarget when bound to the viewport
		//		If is bound then bind throws exception, and most operations on it throw exception because its currently in use
		//		The currently bound to viewport id is also stored in memory, and a pointer to the RenderTarget is kept


		public GraphicsAPI GraphicsAPI = GraphicsAPI.Default;
		public GraphicsAPI DefaultGraphicsAPI { get; private set; }
		public IntPtr Handle { get; private set; }
		public VesselEngine Engine;

		public VGDevice veldridGraphicsDevice;
		internal Pipeline veldridPipeline;
		internal CommandList veldridCommandList;
		public GraphicsPipelineDescription pipelineDescription;

		public DebugGraphicsDevice Debug;

		private uint nextBufferSlot = 0;
		//private IndexBuffer indexBufferCurrent;
		//private VertexBuffer<T> vertexBufferCurrent;

		public GraphicsDevice(VesselEngine engine)
		{
			Debug = new DebugGraphicsDevice(this);
			Engine = engine;
		}

		//TODO: make internal, default to an unlit shader that is prepackaged with Vessel
		public ShaderTechnique shader;

		#region Public API
		/// <summary>
		/// Clears the backbuffer to the specified color
		/// </summary>
		/// <param name="color">The color to clear to</param>
		public void Clear(Color color)
		{
			veldridCommandList.ClearColorTarget(0, color.ToVeldrid());
		}

		/// <summary>
		/// Clears the depth buffer to the specified value
		/// </summary>
		/// <param name="targetDepth"></param>
		public void ClearDepthBuffer(float targetDepth = 0f)
		{
			veldridCommandList.ClearDepthStencil(targetDepth);
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

		/// <summary>
		/// Returns whether the specified Graphics API is supported
		/// </summary>
		/// <param name="api"></param>
		/// <returns></returns>
		public static bool IsGraphicsAPISupported(GraphicsAPI api)
		{
			if (api == GraphicsAPI.Default)
				return true;
			else
				return VGDevice.IsBackendSupported((GraphicsBackend) api);
		}

		/// <summary>
		/// Binds the vertex buffer to the GPU
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="vertexBuffer"></param>
		public void BindBuffer<T> (VertexBuffer<T> vertexBuffer)
		{
			//vertexBufferCurrent = vertexBuffer;
			veldridCommandList.SetVertexBuffer(nextBufferSlot, vertexBuffer._vertexBuffer);
			nextBufferSlot++;
		}

		/// <summary>
		/// Binds the index buffer to the GPU
		/// </summary>
		/// <param name="indexBuffer"></param>
		public void BindBuffer(IndexBuffer indexBuffer)
		{
			//indexBufferCurrent = indexBuffer;
			veldridCommandList.SetIndexBuffer(indexBuffer._indexBuffer, indexBuffer._vdFormat);
		}

		public void DrawIndexed(uint indexCount, uint instanceCount, uint indexStart, int vertexOffset, uint instanceStart)
		{
			// Issue a Draw command for a single instance with 4 indices.
			veldridCommandList.DrawIndexed(indexCount, instanceCount, indexStart, vertexOffset, instanceStart);
		}

		//public void DrawIndexed(int indexCount, uint instanceCount, uint indexStart, int vertexOffset, uint instanceStart)
		//{
			// Issue a Draw command for a single instance with 4 indices.
			//veldridCommandList.DrawIndexed(Convert.ToUInt32(indexCount), instanceCount, indexStart, vertexOffset, instanceStart);
		//}
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

			DefaultGraphicsAPI = (GraphicsAPI) VeldridStartup.GetPlatformDefaultBackend();

			if (config.GraphicsAPI == GraphicsAPI.Default)
				veldridGraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.WindowInternal, options, (GraphicsBackend) DefaultGraphicsAPI);
			else
				veldridGraphicsDevice = VeldridStartup.CreateGraphicsDevice(window.WindowInternal, options, (GraphicsBackend) config.GraphicsAPI);

			//Create the Graphics Resources
			ResourceFactory factory = veldridGraphicsDevice.ResourceFactory;

			//TODO: Replace with ResourceFactory.Load("ShaderTest0");
			//shader = new ShaderTechnique(this,
				//System.IO.File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\VesselSharp\ShaderTests\ShaderTest0.vert.spv"),
				//System.IO.File.ReadAllBytes(@"E:\Data\Projects\Vessel\VesselSharp\VesselSharp\ShaderTests\ShaderTest0.frag.spv"),
				//"ShaderTest0");

			// TODO: Move pipeline to Shader class
			// TODO: Shader => ShaderTechnique; ComputeShader
			// TODO: Assets can be compiled (i.e. banks) or directories (i.e. folder of compiled assets (eg shader.shd), assets aren't packaged into an archive though) - for use during dev cuz packaging archives will probably be time consuming as fuck and decimate workflows

			// Create pipeline
			/*
			pipelineDescription = new GraphicsPipelineDescription();
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
					VertexPositionColor.VertexLayout,
				},
				shaders: shader.Programs);
			pipelineDescription.Outputs = veldridGraphicsDevice.SwapchainFramebuffer.OutputDescription;

			veldridPipeline = factory.CreateGraphicsPipeline(pipelineDescription);
			*/

			veldridCommandList = factory.CreateCommandList();
		}

		internal void RegeneratePipeline()
		{
			if (!veldridPipeline.IsDisposed)
			{
				veldridPipeline.Dispose();
			}
			veldridPipeline = veldridGraphicsDevice.ResourceFactory.CreateGraphicsPipeline(pipelineDescription);
		}

		/// <summary>
		/// Disposes of all graphics objects and shuts down the renderer
		/// </summary>
		public void Dispose()
		{
			veldridCommandList.Dispose();
			shader.Dispose();

			veldridGraphicsDevice.Dispose();
		}

		/// <summary>
		/// Binds a viewport to window framebuffer
		/// </summary>
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

			nextBufferSlot = 0;
		}
		#endregion
	}
}
