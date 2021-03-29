using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public class Component : IDisposable
	{
		/// <summary>
		/// The entity this component belongs to
		/// </summary>
		public Entity Parent;

		/// <summary>
		/// Whether the component is active or not
		/// </summary>
		public bool Active = true;

		/// <summary>
		/// Called when the component is added to an entity and the entity this component is attached to is added to the scene
		/// </summary>
		public virtual void Init() {}

		/// <summary>
		/// Called every frame before rendering
		/// </summary>
		public virtual void Update() {}

		/// <summary>
		/// Called after rendering
		/// </summary>
		public virtual void PostDraw() {}

		/// <summary>
		/// Called upon destroying
		/// </summary>
		public virtual void Dispose() {}
	}
}
