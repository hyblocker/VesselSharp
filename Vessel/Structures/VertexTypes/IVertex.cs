namespace Vessel
{
	interface IVertex
	{
		uint SizeInBytes { get; }
		Veldrid.VertexLayoutDescription VertexLayout { get; }
	}
}
