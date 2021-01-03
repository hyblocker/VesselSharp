using Veldrid;

namespace Vessel
{
	public struct VertexPositionColor : IVertex
	{
		public Vector2 Position;
		public Color Color;
		public VertexPositionColor(Vector2 position, Color color)
		{
			Position = position;
			Color = color;
		}

		public uint SizeInBytes => 
			2 * sizeof(float)		//UV
			+ 4 * sizeof(float);    //Color (4 x channels => 4 x float)

		public static VertexLayoutDescription VertexLayout = new VertexLayoutDescription(
			new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
			new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4));

		VertexLayoutDescription IVertex.VertexLayout { get { return VertexLayout; } }
	}
}
