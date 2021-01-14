using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace Vessel
{
	public class VertexBuffer<T> : IDisposable
	{
		//TODO: VertCount, VertexDecleration

		public GraphicsDevice GraphicsDevice { get; private set; }
		public uint SizeInBytes { get; private set; }

		public int VertexCount;

		internal DeviceBuffer _vertexBuffer;

		public VertexBuffer(GraphicsDevice graphicsDevice, T vertex, int vertexCount, uint SizeInBytes)
		{
			if (graphicsDevice == null)
			{
				throw new InvalidGraphicsDeviceException();
			}

			GraphicsDevice = graphicsDevice;
			VertexCount = vertexCount;
			this.SizeInBytes = SizeInBytes;

			//Create the buffer description
			BufferDescription vertexbufferDescription = new BufferDescription(
				Convert.ToUInt32(vertexCount) * SizeInBytes,
				BufferUsage.VertexBuffer);
			
			//Create the vertex buffer
			_vertexBuffer = graphicsDevice.veldridGraphicsDevice.ResourceFactory.CreateBuffer(vertexbufferDescription);
		}

		public unsafe void SetData<T>(int offsetInBytes, T[] vertices, int startIndex = 0) where T : unmanaged
		{
			SetData<T>(Convert.ToUInt32(offsetInBytes),
				vertices,
				Convert.ToUInt32(startIndex),
				SizeInBytes);
		}

		private void SetData<T>(uint offsetInBytes, T[] vertices, uint startIndex, uint size) where T : unmanaged
		{
			GraphicsDevice.veldridGraphicsDevice.UpdateBuffer(_vertexBuffer,
				offsetInBytes + (size * startIndex), vertices);
		}

		public void Dispose()
		{
			_vertexBuffer.Dispose();
		}
	}
}
