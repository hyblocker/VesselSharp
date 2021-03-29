using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Vessel.Assets
{
	/// <summary>
	/// This class managers fetching and caching of assets, along with asset unloading
	/// </summary>
	public class AssetManager
	{
		/// <summary>
		/// The base path for all assets
		/// </summary>
		public static string AssetPath { get; private set; }

		/// <summary>
		/// Initialises the <see cref="AssetManager"/>, using the <c>Assets</c> directory as the root directory
		/// </summary>
		public static void Initialise()
		{
			Initialise(Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Assets"));
		}

		/// <summary>
		/// Initialises the <see cref="AssetManager"/>, using the <paramref name="path"/> value as the root directory
		/// </summary>
		/// <param name="path">The root directory as an absolute path</param>
		public static void Initialise(string path)
		{
			AssetPath = path;
		}

		/// <summary>
		/// Loads an the specified asset into a byte[]
		/// </summary>
		/// <param name="path"></param>
		/// <returns></returns>
		public static byte[] FetchAssetBytes(string path)
		{
			// TODO: Resolve from asset bank or path
			return File.ReadAllBytes(Path.Combine(AssetPath, path));
		}
	}
}
