using System;
using System.Diagnostics;
using System.Text;
using System.Runtime.Serialization;

namespace Vessel
{
	/// <summary>
	/// Describes a 3D-vector.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector3 : IEquatable<Vector3>
	{
		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector3"/>.
		/// </summary>
		[DataMember]
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Vector3"/>.
		/// </summary>
		[DataMember]
		public float Y;

		/// <summary>
		/// The z coordinate of this <see cref="Vector3"/>.
		/// </summary>
		[DataMember]
		public float Z;

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					this.X.ToString(), "  ",
					this.Y.ToString(), "  ",
					this.Z.ToString()
				);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a 3d vector with X, Y and Z from three values.
		/// </summary>
		/// <param name="x">The x coordinate in 3d-space.</param>
		/// <param name="y">The y coordinate in 3d-space.</param>
		/// <param name="z">The z coordinate in 3d-space.</param>
		public Vector3(float x, float y, float z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y and Z set to the same value.
		/// </summary>
		/// <param name="value">The x, y and z coordinates in 3d-space.</param>
		public Vector3(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y from <see cref="Vector2"/> and Z from a scalar.
		/// </summary>
		/// <param name="value">The x and y coordinates in 3d-space.</param>
		/// <param name="z">The z coordinate in 3d-space.</param>
		public Vector3(Vector2 value, float z)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
		}

		#endregion

		#region Public API

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Object"/>.
		/// </summary>
		/// <param name="obj">The <see cref="Object"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public override bool Equals(object obj)
		{
			if (!(obj is Vector3))
				return false;

			var other = (Vector3)obj;
			return X == other.X &&
					Y == other.Y &&
					Z == other.Z;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector3"/>.
		/// </summary>
		/// <param name="other">The <see cref="Vector3"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector3 other)
		{
			return X == other.X &&
					Y == other.Y &&
					Z == other.Z;
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector3"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector3"/>.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = X.GetHashCode();
				hashCode = (hashCode * 397) ^ Y.GetHashCode();
				hashCode = (hashCode * 397) ^ Z.GetHashCode();
				return hashCode;
			}
		}

		#endregion
	}
}
