using System;

namespace Vessel
{
	[Serializable]
	class VesselException : Exception
	{
		public VesselException()
		{

		}

		public VesselException(string description)
			: base(description)
		{

		}
	}
}
