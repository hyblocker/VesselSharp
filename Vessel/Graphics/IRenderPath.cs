namespace Vessel.Graphics
{
	/// <summary>
	/// Represents a render path
	/// </summary>
	public interface IRenderPath
	{
		/// <summary>
		/// Draws a sceme using this render path. 
		/// For a base renderer see <see cref="RendererForward"/>
		/// </summary>
		/// <param name="scene"></param>
		void Draw(Scene scene);
	}
}
