using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Vessel
{
	public static class IOHelper
	{
		public static void EnsureDirectoryExists(string directory)
		{
			if (!Directory.Exists(directory))
			{
				Directory.CreateDirectory(directory);
			}
		}
	}
}
