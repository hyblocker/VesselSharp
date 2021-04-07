using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using Veldrid.SPIRV;

namespace VesselShaderCompiler
{
	public static class VesselShaderUtil
	{
		public static VesselShaderDescription[] GetShaderList(string[] searchPaths)
		{
			List<VesselShaderDescription> descs = new List<VesselShaderDescription>();

			List<string> vertexShaders = new List<string>();
			List<string> fragmentShaders = new List<string>();

			List<(string, string)> vsShaderPairs = new List<(string, string)>();
			List<string> computeShaders = new List<string>();

			// Get all files in the search dirs and sort them by type into arrays
			foreach (var path in searchPaths)
			{
				vertexShaders.AddRange(Directory.GetFiles(path, "*.vert", SearchOption.AllDirectories));
				fragmentShaders.AddRange(Directory.GetFiles(path, "*.frag", SearchOption.AllDirectories));
				computeShaders.AddRange(Directory.GetFiles(path, "*.comp", SearchOption.AllDirectories));
			}

			// Verify that vertex and fragment shaders are in pairs
			for (int i = 0; i < vertexShaders.Count; i++)
			{
				for (int j = 0; j < fragmentShaders.Count; j++)
				{
					if (vertexShaders[i].ToLower().Substring(0, vertexShaders[i].Length - 5) ==
						fragmentShaders[j].ToLower().Substring(0, fragmentShaders[j].Length - 5))
					{
						// Matching pair
						vsShaderPairs.Add((vertexShaders[i], fragmentShaders[j]));
					}
				}
			}

			// Form the shader descriptions
			string frag_buf;
			string vert_buf;
			string shader_name;

			// Vertex Fragment pairs
			foreach(var vsShaderPair in vsShaderPairs)
			{
				vert_buf = vsShaderPair.Item1;
				frag_buf = vsShaderPair.Item2;

				shader_name = Path.GetFileNameWithoutExtension(vert_buf);

				descs.Add(new VesselShaderDescription(shader_name, new ShaderStageDescription[]
				{
					new ShaderStageDescription(Veldrid.ShaderStages.Vertex, vert_buf),
					new ShaderStageDescription(Veldrid.ShaderStages.Fragment, frag_buf)
				}));
			}
			
			// Compute shader
			foreach(var computeShader in computeShaders)
			{
				shader_name = Path.GetFileNameWithoutExtension(computeShader);

				descs.Add(new VesselShaderDescription(shader_name, new ShaderStageDescription[]
				{
					new ShaderStageDescription(Veldrid.ShaderStages.Compute, computeShader)
				}));
			}

			// Return
			return descs.ToArray();
		}

		private static IEnumerable<string> ReadGlsl(string shaderName)
		{
			if (File.Exists(shaderName))
			{
				foreach (var line in File.ReadAllLines(shaderName))
					yield return line;
				yield break;
			}
		}

		private static readonly Regex IncludeRegex = new Regex("^#include\\s+\"([^\"]+)\"");
		public static string LoadGlsl(string shaderName)
		{
			var lines = ReadGlsl(shaderName);

			// Substitute include files
			var sb = new StringBuilder();
			foreach (var line in lines)
			{
				var match = IncludeRegex.Match(line);
				if (match.Success)
				{
					var filename = Path.GetFullPath(Path.Combine(Path.GetDirectoryName(shaderName), match.Groups[1].Value));
					var includedContent = LoadGlsl(filename);
					sb.AppendLine(includedContent);
				}
				else sb.AppendLine(line);
			}

			return sb.ToString();
		}

		internal static bool debugMode;

		public static GlslCompileOptions GetOptions(VesselShaderDescription shader)
		{
			//return new GlslCompileOptions(false, shader.Macros);
			return new GlslCompileOptions(debugMode);
		}
	}
}
