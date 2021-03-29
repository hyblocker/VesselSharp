using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace Vessel
{
	/// <summary>
	/// A camera component. This component is what is used to project the world to the target framebuffer.
	/// This camera component essentially holds a projection matrix and view matrix, along with the frame buffer its attached to.
	/// For components like post processing, those hook via render order, using something like: 
	/// <code>
	/// 
	/// </code>
	/// </summary>
	public class Camera : Component
	{
		public Matrix4x4 ProjectionMatrix { get; private set; }
		public Matrix4x4 ViewMatrix { get; private set; }
		
		//public RenderTexture boundTexture;

		public Camera()
		{

		}

		/// <summary>
		/// Clear buffers
		/// </summary>
		public override void Dispose()
		{

		}
	}
}
