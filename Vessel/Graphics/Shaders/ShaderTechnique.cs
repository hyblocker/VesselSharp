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
	public class ShaderTechnique : IResource
	{
		internal ShaderProgram InternalVertexShader;
		internal ShaderProgram InternalFragmentShader;
		internal Pipeline shaderPipeline;

		public GraphicsDevice GraphicsDevice;
		public ShaderProgram[] Programs { get; private set; }
		public string AssetPath => _assetPath;
		public bool IsLoaded => InternalFragmentShader.IsDisposed == false || InternalVertexShader.IsDisposed == false;

		private string _assetPath = "Shader";

		/// <summary>
		/// Attempts to load a shader by extracting the binary blob from the asset bank at the specified resource
		/// </summary>
		/// <param name="sourceBank"></param>
		/// <param name="shaderName"></param>
		public ShaderTechnique(GraphicsDevice device, AssetBank sourceBank, string shaderName)
		{
			_assetPath = shaderName;
		}

		/// <summary>
		/// Attempts to load a shader from the file at the specified path
		/// </summary>
		/// <param name="file">An absolute path which points to a shader object</param>
		public ShaderTechnique(GraphicsDevice device, string file)
		{
			_assetPath = Path.GetFileNameWithoutExtension(file);
			byte[] binaryBytes = File.ReadAllBytes(file);
			LoadFromShaderBinary(device, binaryBytes);
		}

		/// <summary>
		/// Attempts to load a shader from the given byte[]
		/// </summary>
		/// <param name="shaderBinary">The byte stream</param>
		public ShaderTechnique(GraphicsDevice device, byte[] shaderBinary, string name)
		{
			_assetPath = name;
			LoadFromShaderBinary(device, shaderBinary);
		}

		//TODO: REMOVE
		public ShaderTechnique(GraphicsDevice device, byte[] vert, byte[] frag, string name)
		{
			GraphicsDevice = device;
			_assetPath = name;

			//Loads a SPIR-V binary from the byte[], and attempts to use SPIR-V cross to cross compile it for other platforms if the current platform isn't Vulkan
			Programs = device.veldridGraphicsDevice.ResourceFactory.CreateFromSpirv(
				new ShaderDescription(ShaderStages.Vertex, vert, "main", VesselEngine.DebugMode),
				new ShaderDescription(ShaderStages.Fragment, frag, "main", VesselEngine.DebugMode));

			InternalVertexShader = Programs[0];
			InternalFragmentShader = Programs[1];
		}

		/// <summary>
		/// Loads the shader from the binary object
		/// </summary>
		/// <param name="bytes">The byte stream</param>
		private void LoadFromShaderBinary(GraphicsDevice device, byte[] bytes)
		{
			GraphicsDevice = device;

			//TODO: Read ShaderTechnique

			/*
			 * ENCODED USING LITTLE ENDIAN
			 * 
			 * ShaderTable
			 * {
			 *		byte availablePrograms = A|B|C|D|E|F|G|H; //each bit in this byte represents a specific shader stage. the order of bits is the order of the following 4 longs
			 *		ulong vertexShader;						  //pointer to the start of the vertex shader program
			 *		ulong fragmentShader;					  //pointer to the start of the fragment shader program
			 *		//etc, repeat for each shader stage
			 * }
			 * Shader []
			 * {
			 *		int nameLength; // length in bytes of name character; size = 4b
			 *		byte[] name;    // ASCII encoded byte[] representing the name
			 *		ulong offset;   // pointer to the start of the byte[] in the file stream
			 *		ulong length;   // how long the byte[] is in bytes, used to determine when to stop reading
			 * }
			 * 
			 * availablePrograms
			 * 
			 * 
			 */

			int pointer = 0;
			byte availableProgramFlags = bytes[pointer++];

			int shaderCount = 0;

			//Determine which shaders are included in the binary
			bool hasVertexProgram					= (availableProgramFlags & (1 << 1 - 1)) != 0;
			bool hasGeometryProgram					= (availableProgramFlags & (1 << 2 - 1)) != 0;
			bool hasTesselationControlProgram		= (availableProgramFlags & (1 << 3 - 1)) != 0;
			bool hasTesselationEvaluationProgram	= (availableProgramFlags & (1 << 4 - 1)) != 0;
			bool hasFragmentProgram					= (availableProgramFlags & (1 << 5 - 1)) != 0;
			bool hasComputeProgram					= (availableProgramFlags & (1 << 6 - 1)) != 0;

			//Count the number of shaders
			if (hasVertexProgram) { shaderCount++; }
			if (hasGeometryProgram) { shaderCount++; }
			if (hasTesselationControlProgram) { shaderCount++; }
			if (hasTesselationEvaluationProgram) { shaderCount++; }
			if (hasFragmentProgram) { shaderCount++; }
			if (hasComputeProgram) { shaderCount++; }

			byte[] vertShader = new byte[] { };
			byte[] fragShader = new byte[] { };
			byte[] computeShader = new byte[] { };

			ulong vertShaderPtr = ulong.MaxValue;
			ulong fragShaderPtr = ulong.MaxValue;
			ulong compShaderPtr = ulong.MaxValue;

			//Determine the addresses of the shader programs included in the shader binary
			if (hasVertexProgram)
			{
				vertShaderPtr = BinaryUtils.ReadIntFromByteArray(bytes, pointer, 8, true);
				pointer += 8;
			}
			if (hasFragmentProgram)
			{
				fragShaderPtr = BinaryUtils.ReadIntFromByteArray(bytes, pointer, 8, true);
				pointer += 8;
			}
			if (hasComputeProgram)
			{
				compShaderPtr = BinaryUtils.ReadIntFromByteArray(bytes, pointer, 8, true);
				pointer += 8;
			}
			
			//Loads a SPIR-V binary from the byte[], and attempts to use SPIR-V cross to cross compile it for other platforms if the current platform isn't Vulkan
			Programs = device.veldridGraphicsDevice.ResourceFactory.CreateFromSpirv(
				new ShaderDescription(ShaderStages.Vertex, vertShader, "main", VesselEngine.DebugMode),
				new ShaderDescription(ShaderStages.Fragment, fragShader, "main", VesselEngine.DebugMode));

			InternalVertexShader = Programs[0];
			InternalFragmentShader = Programs[1];

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
			pipelineDescription.Outputs = device.veldridGraphicsDevice.SwapchainFramebuffer.OutputDescription;

			

			pipelineDescription.ShaderSet = new ShaderSetDescription(
				vertexLayouts: new VertexLayoutDescription[]
				{
					VertexPositionColor.VertexLayout ,
				},
				shaders: Programs);

			shaderPipeline = device.veldridGraphicsDevice.ResourceFactory.CreateGraphicsPipeline(pipelineDescription);
		}

		/// <summary>
		/// Disposes the shaders
		/// </summary>
		public void Dispose()
		{
			foreach(var shader in Programs)
			{
				if (!shader.IsDisposed)
					shader.Dispose();
			}
		}

		public void Apply()
		{
			//TODO: Pipeline is constructed with the shader technique
			//TODO: Set pipeline from shader

			GraphicsDevice.pipelineDescription.ShaderSet = new ShaderSetDescription(
				vertexLayouts: new VertexLayoutDescription[]
				{
					VertexPositionColor.VertexLayout,
				},
				shaders: Programs);

			//TODO: Check if the pipeline needs to be invalidated
			GraphicsDevice.RegeneratePipeline();

			//TODO: Apply shader to pipeline and regenerate or pull from cache
			GraphicsDevice.veldridCommandList.SetPipeline(GraphicsDevice.veldridPipeline);
		}

		public void Unload()
		{

		}
	}
}
