using System;
using System.Collections.Generic;
using System.Text;

namespace Vessel.Assets
{
	/// <summary>
	/// A generic resource that Vessel can load
	/// </summary>
	public interface IResource : IDisposable
	{
		string AssetPath { get; }
		bool IsLoaded { get; }

		/// <summary>
		/// Unloads the resource
		/// </summary>
		void Unload();
	}
}
