using McMaster.Extensions.CommandLineUtils;
using System;
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

		[Option("--set", "The path to the JSON file containing shader variant definitions to compile.", CommandOptionType.SingleValue)]
		public string SetDefinitionPath { get; }

		public void OnExecute()
		{
			// TODO: Compile shaders or something idfk
			if (!Directory.Exists(OutputPath))
			{
				Directory.CreateDirectory(OutputPath);
			}


		}
	}
}
