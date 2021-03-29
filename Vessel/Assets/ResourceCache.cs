using System;
using System.Collections.Generic;
using System.Text;

namespace Vessel.Assets
{
	/// <summary>
	/// A <see cref="IResource"/> cache, which contains a cached list of resources previously loaded by Vessel.
	/// <para></para>
	/// Vessel will manage unloading of resource during stress automagically.
	/// </summary>
	public class ResourceCache
	{
		private static List<IResource> s_resources;

		//TODO: Implement this black magic fuckery
	}
}
