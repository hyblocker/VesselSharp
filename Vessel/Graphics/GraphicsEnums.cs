using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public enum GraphicsAPI
	{
		/// <summary>
		/// No backend given.
		/// </summary>
		Noop,

		/// <summary>
		/// Direct3D 9
		/// </summary>
		Direct3D9,

		/// <summary>
		/// Direct3D 11
		/// </summary>
		Direct3D11,

		/// <summary>
		/// Direct3D 12
		/// </summary>
		Direct3D12,

		/// <summary>
		/// PlayStation 4's GNM
		/// </summary>
		GNM,

		/// <summary>
		/// Apple Metal.
		/// </summary>
		Metal,

		/// <summary>
		/// OpenGL ES
		/// </summary>
		OpenGLES,

		/// <summary>
		/// OpenGL
		/// </summary>
		OpenGL,

		/// <summary>
		/// Vulkan
		/// </summary>
		Vulkan,

		/// <summary>
		/// Used during initialization; specifies that the library should
		/// pick the best renderer for the running hardware and OS.
		/// </summary>
		Default
	}
}
