using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
using Veldrid.SPIRV;

namespace Vessel
{
	/// <summary>
	/// Helper class to read a <see cref="Shader"/> from a binary blob
	/// </summary>
	public class VesselShaderReader
	{
		public static void BlobToShader(
			byte[] shaderBytes,
			out SpirvReflection reflection,
			out byte[][] shaderBytecode)
		{
			// FILE STRUCTURE

			// INT HEADER SIZE
			// BYTE[] HEADER => BSON DESERIALIZE INTO VESSEL.SHADERMETADATA ; includes reflection data

			// INT ENTRIES
			// INT [] SIZES

			// STAGE ARRAY {
			//		
			//		BYTE[] DATA  =>  a single shader program
			//		
			// }

			// Read the data

			// Header length
			int headerLen = ReadInt32(shaderBytes, 0);

			// Isolate the reflection data to a byte[]
			byte[] reflData = new byte[headerLen];
			Array.Copy(shaderBytes, 4, reflData, 0, headerLen);

			// Read the amount of entries in the shader
			int entryCount = ReadInt32(shaderBytes, headerLen + 4);
			int arrPos = headerLen + 8;

			// Create a buffer for shader bytecode
			shaderBytecode = new byte[entryCount][];

			// Read the SPIRV bytecode
			for (int i = 0; i < entryCount; i++)
			{
				int bytecodeSize = ReadInt32(shaderBytes, arrPos);
				arrPos += 4;
				shaderBytecode[i] = new byte[bytecodeSize];
				Array.Copy(shaderBytes, arrPos, shaderBytecode[i], 0, bytecodeSize);
				arrPos += bytecodeSize;
			}

			// Read the BSON Reflection data into a SPIRV Reflection object
			using (MemoryStream mem = new MemoryStream(reflData))
			{
				using (BsonReader jtr = new BsonReader(mem))
				{
					reflection = s_serializer.Value.Deserialize<SpirvReflection>(jtr);
				}
			}
		}

		private static int ReadInt32(byte[] shaderBytes, int offset)
		{
			// Reverse the 4 bytes if little endian
			if (BitConverter.IsLittleEndian)
			{
				byte buf;

				// 1 2 3 4
				// 4 3 2 1
				// the above shows that we can simply swap pos 1 and 4, and pos 2 and 3 to reverse the array

				//Swap 1 and 4
				buf = shaderBytes[offset + 0];
				shaderBytes[offset + 0] = shaderBytes[offset + 3];
				shaderBytes[offset + 3] = buf;

				// Swap 2 and 3
				buf = shaderBytes[offset + 1];
				shaderBytes[offset + 1] = shaderBytes[offset + 2];
				shaderBytes[offset + 2] = buf;
			}

			return BitConverter.ToInt32(shaderBytes, offset);
		}

		// Used for SPIRV-Reflection
		private static readonly Lazy<JsonSerializer> s_serializer = new Lazy<JsonSerializer>(CreateSerializer);

		private static JsonSerializer CreateSerializer()
		{
			JsonSerializer serializer = new JsonSerializer();
			serializer.Formatting = Formatting.Indented;
			StringEnumConverter enumConverter = new StringEnumConverter();
			serializer.Converters.Add(enumConverter);
			return serializer;
		}
	}
}
