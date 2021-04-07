using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Veldrid;
using Veldrid.SPIRV;

namespace VesselShaderCompiler
{
	/// <summary>
	/// A single shader stage
	/// </summary>
	public struct ShaderStageDescription
	{
		public ShaderStages Stage { get; }
		public string FileName { get; }

		public ShaderStageDescription(ShaderStages stage, string fileName)
		{
			Stage = stage;
			FileName = fileName;
		}
	}

	/// <summary>
	/// A complete shader
	/// </summary>
	public struct VesselShaderDescription
	{
		public string Name { get; }
		public ShaderStageDescription[] Shaders { get; }

		public VesselShaderDescription(string name,
			ShaderStageDescription[] shaders)
		{
			Name = name;
			Shaders = shaders;
		}
	}

	/// <summary>
	/// A class responsible for loading and compiling vulkan shaders, while retaining reflection data
	/// </summary>
	public class VesselShaderCompiler
	{
		private readonly VesselShaderDescription[] _shaderSearchPaths;
		private readonly string _outputPath;

		public VesselShaderCompiler(VesselShaderDescription[] shaderSearchPaths, string outputPath)
		{
			_shaderSearchPaths = shaderSearchPaths;
			_outputPath = outputPath;
		}

		public string[] Compile(VesselShaderDescription shader)
		{
			if (shader.Shaders.Length == 1)
			{
				if (shader.Shaders[0].Stage == ShaderStages.Vertex) { return CompileVertexFragment(shader); }
				if (shader.Shaders[0].Stage == ShaderStages.Compute) { return CompileCompute(shader); }
			}
			if (shader.Shaders.Length == 2)
			{
				bool hasVertex = false;
				bool hasFragment = false;
				foreach (var shaderStage in shader.Shaders)
				{
					hasVertex |= shaderStage.Stage == ShaderStages.Vertex;
					hasFragment |= shaderStage.Stage == ShaderStages.Fragment;
				}

				if (!hasVertex)
				{
					throw new SpirvCompilationException($"Variant \"{shader.Name}\" is missing a vertex shader.");
				}
				if (!hasFragment)
				{
					throw new SpirvCompilationException($"Variant \"{shader.Name}\" is missing a fragment shader.");
				}

				return CompileVertexFragment(shader);
			}
			else
			{
				throw new SpirvCompilationException(
					$"Variant \"{shader.Name}\" has an unsupported combination of shader stages.");
			}
		}

		private byte[] CompileToSpirv(
			VesselShaderDescription shader,
			string fileName,
			ShaderStages stage)
		{
			GlslCompileOptions glslOptions = VesselShaderUtil.GetOptions(shader);
			string glsl = VesselShaderUtil.LoadGlsl(fileName);
			SpirvCompilationResult result = SpirvCompilation.CompileGlslToSpirv(
				glsl,
				fileName,
				stage,
				glslOptions);
			return result.SpirvBytes;
		}

		private string[] CompileVertexFragment(VesselShaderDescription shader)
		{
			List<string> generatedFiles = new List<string>();
			List<Exception> compilationExceptions = new List<Exception>();
			byte[] vsBytes = null;
			byte[] fsBytes = null;

			// Compile vertex shader
			string vertexFileName = shader.Shaders.FirstOrDefault(vsd => vsd.Stage == ShaderStages.Vertex).FileName;
			if (vertexFileName != null)
			{
				try
				{
					vsBytes = CompileToSpirv(shader, vertexFileName, ShaderStages.Vertex);
				}
				catch (Exception e)
				{
					compilationExceptions.Add(e);
				}
			}

			// Compile fragment shader
			string fragmentFileName = shader.Shaders.FirstOrDefault(vsd => vsd.Stage == ShaderStages.Fragment).FileName;
			if (fragmentFileName != null)
			{
				try
				{
					fsBytes = CompileToSpirv(shader, fragmentFileName, ShaderStages.Fragment);
				}
				catch (Exception e)
				{
					compilationExceptions.Add(e);
				}
			}

			if (compilationExceptions.Count > 0)
			{
				throw new AggregateException(
					$"Errors were encountered when compiling from GLSL to SPIR-V.",
					compilationExceptions);
			}

			try
			{
				// Generate reflection data
				VertexFragmentCompilationResult result = SpirvCompilation.CompileVertexFragment(vsBytes, fsBytes, CrossCompileTarget.HLSL);

				WriteShaderBinary(
					shader.Name + ".shdr",
					new byte[][] { vsBytes, fsBytes },
					result.Reflection);
			}
			catch (Exception e)
			{
				compilationExceptions.Add(e);
			}

			if (compilationExceptions.Count > 0)
			{
				throw new AggregateException($"Errors were encountered when compiling shader variant(s).", compilationExceptions);
			}

			Console.WriteLine($"Successfully compiled \"{Path.GetFileNameWithoutExtension(shader.Shaders[0].FileName)}\" as a Vertex, Fragment pair shader!");

			return generatedFiles.ToArray();
		}

		private string[] CompileCompute(VesselShaderDescription shader)
		{
			List<string> generatedFiles = new List<string>();
			var csBytes = CompileToSpirv(shader, shader.Shaders[0].FileName, ShaderStages.Compute);

			// Generate metadata for HLSL, cuz we want to know the semantics since theyre important on HLSL
			var crossData = SpirvCompilation.CompileCompute(csBytes, CrossCompileTarget.HLSL);

			WriteShaderBinary(
				shader.Name + ".shdr",
				new byte[][] { csBytes },
				crossData.Reflection);

			Console.WriteLine($"Successfully compiled \"{Path.GetFileNameWithoutExtension(shader.Shaders[0].FileName)}\" as a Compute shader!");

			return generatedFiles.ToArray();
		}

		private void WriteShaderBinary(
			string file,
			byte[][] result,
			SpirvReflection reflectionData)
		{
			// Resolve absolute path
			file = Path.Combine(_outputPath, file);

			// FILE STRUCTURE

			// INT HEADER SIZE
			// BYTE[] HEADER => BSON DESERIALIZE INTO VESSEL.SHADERMETADATA ; includes reflection data

			// INT ENTRIES
			// INT [] SIZES

			// STAGE ARRAY {
			//		
			//		BYTE[] DATA  =>  a single shader program
			//		
			// }

			int streamPos = 0;

			using (var fs = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Formatting = Formatting.Indented;
				StringEnumConverter enumConverter = new StringEnumConverter();
				serializer.Converters.Add(enumConverter);
				using (var mem = new MemoryStream())
				{
					using (BsonWriter jtw = new BsonWriter(mem))
					{
						serializer.Serialize(jtw, reflectionData);
					}

					var memArr = mem.ToArray();

					// meta length
					streamPos += fs.Write((int)memArr.Length);

					// data
					//mem.WriteTo(fs);
					fs.Write(memArr);

					streamPos += (int)memArr.Length;
				}

				// entries
				streamPos += fs.Write(result.Length);

				// write the compiled shader code to the binary
				for (int i = 0; i < result.Length; i++)
				{
					// length
					streamPos += fs.Write(result[i].Length);

					// bytecode
					fs.Write(result[i]);
					streamPos += result[i].Length;
				}

				fs.Flush();
			}


		}
	}
}
