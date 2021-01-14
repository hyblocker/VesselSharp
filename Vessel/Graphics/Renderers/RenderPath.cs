namespace Vessel
{
	/// <summary>
	/// A rendering path 
	/// </summary>
	public enum RenderPath
	{
		/// <summary>
		/// Forward rendering
		/// </summary>
		Forward,
		
		/// <summary>
		/// Deferred rendering
		/// </summary>
		//Deferred,
		
		/// <summary>
		/// Forward rendering using tile based culling
		/// </summary>
		//ForwardPlus,
		
		/// <summary>
		/// Forward rendering using clustered light culling
		/// </summary>
		//Clustered,

		/// <summary>
		/// A custom render path
		/// </summary>
		Custom,
	}
}
