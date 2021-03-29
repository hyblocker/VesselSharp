using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	/// <summary>
	/// An entity.
	/// </summary>
	public class Entity : IDisposable
	{
		/// <summary>
		/// The name of this entity
		/// </summary>
		public string Name = "Entity";
		/// <summary>
		/// Whether the entity and all of its components are active or not
		/// </summary>
		public bool Active = false;
		/// <summary>
		/// The scene to which this entity is registered to
		/// </summary>
		public Scene Scene;

		//TODO: LAYERS
		//TODO: TAGS
		//TODO: Transform?

		// The internal component array
		private List<Component> m_components;

		/// <summary>
		/// Creates an entity
		/// </summary>
		public Entity()
		{
			m_components = new List<Component>();
		}

		/// <summary>
		/// Returns the first <see cref="Component"/> of the specified type if its attached to this <see cref="Entity"/>.
		/// Returns null if it doesn't exist
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public T GetComponent<T>() where T : Component
		{
			// Iterate through all components registered in the entity
			for (int i = 0; i < m_components.Count; i++)
			{
				if (m_components[i].GetType().Equals(typeof(T)))
				{
					return (T) m_components[i];
				}
			}

			return null;
		}

		/// <summary>
		/// Attaches a component to this <see cref="Entity"/>
		/// </summary>
		/// <param name="component"></param>
		public void AddComponent(Component component)
		{
			component.Parent = this;
			m_components.Add(component);
			
			if (Scene != null)
			{
				component.Init();
			}
		}

		public void Dispose()
		{
			// Dispose children, they may depend on this entity's components to perform cleanup so

			// Iterate through all components registered in the entity
			for (int i = 0; i < m_components.Count; i++)
			{
				//Dispose them
				m_components[i].Dispose();
			}
		}
	}
}
