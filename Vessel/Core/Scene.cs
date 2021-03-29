using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	/// <summary>
	/// A hierarchy that contains all entites in a scene
	/// </summary>
	public class Scene : IDisposable
	{
		public void Dispose()
		{
			//Dispose all entities
		}

		#region Overridables
		/// <summary>
		/// Called when the scene is loaded
		/// </summary>
		public virtual void Initialise()
		{

		}

		/// <summary>
		/// Called when the scene is unloaded
		/// </summary>
		public virtual void End()
		{

		}

		public virtual void Update()
		{

		}

		public virtual void PreUpdate()
		{

		}

		public virtual void PostUpdate()
		{

		}

		public virtual void Draw()
		{

		}

		public virtual void PreDraw()
		{

		}

		public virtual void PostDraw()
		{

		}
		#endregion
	}
}
