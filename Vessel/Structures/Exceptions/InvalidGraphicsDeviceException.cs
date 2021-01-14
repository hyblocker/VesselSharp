using System;

namespace Vessel
{
	[Serializable]
	class InvalidGraphicsDeviceException : Exception
	{
		public InvalidGraphicsDeviceException()
			: base("Invalid GraphicsDevice!")
		{

		}

		public InvalidGraphicsDeviceException(string desc)
			: base(string.Format("Invalid GraphicsDevice! {0}", desc))
		{

		}
	}
}
