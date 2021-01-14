using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Veldrid;

namespace Vessel
{
	public class IndexBuffer : IDisposable
	{
		public GraphicsDevice GraphicsDevice;
		public uint Size;
		public IndexFormat Format;

		internal Veldrid.IndexFormat _vdFormat;
		internal DeviceBuffer _indexBuffer;

		public IndexBuffer(GraphicsDevice graphicsDevice, int bufferSize, IndexFormat indexFormat)
			: this(graphicsDevice, Convert.ToUInt32(bufferSize), indexFormat)
		{
		}

		public IndexBuffer(GraphicsDevice graphicsDevice, uint bufferSize, IndexFormat indexFormat)
		{
			if (graphicsDevice == null)
				throw new InvalidGraphicsDeviceException();

			GraphicsDevice = graphicsDevice;
			Size = bufferSize;
			Format = indexFormat;
			//Cast from Vessel.IndexFormat to Veldrid.Indexformat
			_vdFormat = (Veldrid.IndexFormat)((int)indexFormat);

			BufferDescription ibDescription = new BufferDescription(
				Convert.ToUInt32(bufferSize * (indexFormat == IndexFormat.UInt16 ? sizeof(ushort) : sizeof(uint))),
				BufferUsage.IndexBuffer);
			_indexBuffer = graphicsDevice.veldridGraphicsDevice.ResourceFactory.CreateBuffer(ibDescription);
		}

		public void SetData(int offsetInBytes, uint[] indices, int startIndex = 0)
		{
			GraphicsDevice.veldridGraphicsDevice.UpdateBuffer(_indexBuffer,
				Convert.ToUInt32(offsetInBytes) + Convert.ToUInt32(Format == IndexFormat.UInt16 ? sizeof(ushort) : sizeof(uint)), indices);
		}

		public void SetData(int offsetInBytes, ushort[] indices, int startIndex = 0)
		{
			GraphicsDevice.veldridGraphicsDevice.UpdateBuffer(_indexBuffer,
				Convert.ToUInt32(offsetInBytes) + Convert.ToUInt32(startIndex * (Format == IndexFormat.UInt16 ? sizeof(ushort) : sizeof(uint))), indices);
		}

		public void Dispose()
		{
			_indexBuffer.Dispose();
		}
	}
}

/// <summary>
/// The index format of an <see cref="IndexBuffer"/>.
/// </summary>
public enum IndexFormat : byte
{
	/// <summary>
	/// Each index is a 16-bit unsigned integer (System.UInt16).
	/// </summary>
	UInt16,
	/// <summary>
	/// Each index is a 32-bit unsigned integer (System.UInt32).
	/// </summary>
	UInt32,
}
