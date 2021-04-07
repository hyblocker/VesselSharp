using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VesselShaderCompiler
{
	public static class FileStreamExtensions
	{
		public static int Write(this FileStream stream, int value)
		{
			var bytes = BitConverter.GetBytes(value);
			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			
			stream.Write(bytes, 0, bytes.Length);
			return 4;
		}

		public static int ReadInt(this FileStream fs, int offset)
		{
			byte[] bytes = fs.ReadBytes(4, offset);

			if (BitConverter.IsLittleEndian)
				Array.Reverse(bytes);
			return BitConverter.ToInt32(bytes, 0);
		}

		public static byte[] ReadBytes(this FileStream fs, int length, int offset)
		{
			// https://jonskeet.uk/csharp/readbinary.html

			byte[] data = new byte[length];
			while (length > 0)
			{
				int read = fs.Read(data, offset, length);
				if (read <= 0)
					throw new EndOfStreamException
						(String.Format("End of stream reached with {0} bytes left to read", length));
				length -= read;
				offset += read;
			}

			return data;
		}
	}
}
