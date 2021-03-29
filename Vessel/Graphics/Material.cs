using System;
using System.Collections.Generic;
using System.Text;
using Veldrid;
using System.Numerics;

using VPipeline = Veldrid.Pipeline;

namespace Vessel
{
	public class Material
	{
		public Shader shader;

		public VPipeline pipeline;

		public void SetFloat(string name, float value)
		{

		}

		public void SetMatrix(string name, Matrix4x4 value)
		{

		}

		public void SetTexture(string name, float value) // TODO: ACTUALLY TEXTURE BRUH
		{

		}

		public void SetVector(string name, Vector2 value)
		{

		}

		public void SetVector(string name, Vector3 value)
		{

		}

		public void SetVector(string name, Vector4 value)
		{

		}
	}
}
