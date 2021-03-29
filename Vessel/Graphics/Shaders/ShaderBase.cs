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
	/// <summary>
	/// A common base class for all shader types
	/// </summary>
	public abstract class ShaderBase : IResource
	{
		public GraphicsDevice GraphicsDevice;

		// Internal Core shit for Veldrid
		internal ShaderProgram[] Programs;
		internal Pipeline shaderPipeline;
		internal VGraphicsDevice veldridGraphicsDevice => GraphicsDevice.veldridGraphicsDevice;
		internal GraphicsPipelineDescription pipelineDescription;

		// TODO: Blend modes and other shit

		public ShaderBase(GraphicsDevice graphicsDevice)
		{
			GraphicsDevice = graphicsDevice;
		}

		// Whether the shader is dirty; regenerates the ShaderPipeline on next Apply
		internal bool isDirty = true;

		/// <summary>
		/// The path to the shader in the <see cref="AssetBank"/>
		/// </summary>
		public string AssetPath { get; internal set; }
		/// <summary>
		/// Whether the shader is loaded or not
		/// </summary>
		public bool IsLoaded { get; internal set; }

		/// <summary>
		/// Binds the shader to the GPU
		/// </summary>
		public abstract void Apply();

		/// <summary>
		/// Unloads the shader
		/// </summary>
		public abstract void Unload();

		/// <summary>
		/// Disposes the shader, destroying the resources associated with it, making it unusable
		/// </summary>
		public abstract void Dispose();
	}
}
