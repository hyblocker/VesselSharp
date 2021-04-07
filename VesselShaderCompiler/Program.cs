using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;

namespace VesselShaderCompiler
{
	/// <summary>
	/// This is literally a program whose sole purpose is to compile shaders
	/// </summary>
	public class Program
	{
		static void Main(string[] args)
		{
			CommandLineApplication.Execute<Program>(args);
		}

		[Option("--search-path", "The set of directories to search for shader source files.", CommandOptionType.MultipleValue)]
		public string[] SearchPaths { get; }

		[Option("--output-path", "The directory where compiled files are placed.", CommandOptionType.SingleValue)]
		public string OutputPath { get; }

		[Option("--debug")]
		public bool Debug { get; } = true;

		public void OnExecute()
		{
			VesselShaderUtil.debugMode = Debug;

			// TODO: Compile shaders or something idfk
			if (!Directory.Exists(OutputPath))
			{
				Directory.CreateDirectory(OutputPath);
			}

			// Get the shader descriptions using smart ass reflection
			VesselShaderDescription[] descs = VesselShaderUtil.GetShaderList(SearchPaths);

			HashSet<string> generatedPaths = new HashSet<string>();

			VesselShaderCompiler compiler = new VesselShaderCompiler(descs, OutputPath);
			foreach (VesselShaderDescription desc in descs)
			{
				string[] newPaths = compiler.Compile(desc);
				foreach (string s in newPaths)
				{
					generatedPaths.Add(s);
				}
			}

		}
	}
}
