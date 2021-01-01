using System;
using System.Runtime.Serialization;
using System.Diagnostics;

namespace Vessel
{

	/// <summary>
	/// Describes a 4D-vector.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector4 : IEquatable<Vector4>
	{
		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector4"/>.
		/// </summary>
		[DataMember]
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Vector4"/>.
		/// </summary>
		[DataMember]
		public float Y;

		/// <summary>
		/// The z coordinate of this <see cref="Vector4"/>.
		/// </summary>
		[DataMember]
		public float Z;

		/// <summary>
		/// The w coordinate of this <see cref="Vector4"/>.
		/// </summary>
		[DataMember]
		public float W;

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					this.X.ToString(), "  ",
					this.Y.ToString(), "  ",
					this.Z.ToString(), "  ",
					this.W.ToString()
				);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a 3d vector with X, Y, Z and W from four values.
		/// </summary>
		/// <param name="x">The x coordinate in 4d-space.</param>
		/// <param name="y">The y coordinate in 4d-space.</param>
		/// <param name="z">The z coordinate in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(float x, float y, float z, float w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 3d vector with X and Z from <see cref="Vector2"/> and Z and W from the scalars.
		/// </summary>
		/// <param name="value">The x and y coordinates in 4d-space.</param>
		/// <param name="z">The z coordinate in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(Vector2 value, float z, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 3d vector with X, Y, Z from <see cref="Vector3"/> and W from a scalar.
		/// </summary>
		/// <param name="value">The x, y and z coordinates in 4d-space.</param>
		/// <param name="w">The w coordinate in 4d-space.</param>
		public Vector4(Vector3 value, float w)
		{
			this.X = value.X;
			this.Y = value.Y;
			this.Z = value.Z;
			this.W = w;
		}

		/// <summary>
		/// Constructs a 4d vector with X, Y, Z and W set to the same value.
		/// </summary>
		/// <param name="value">The x, y, z and w coordinates in 4d-space.</param>
		public Vector4(float value)
		{
			this.X = value;
			this.Y = value;
			this.Z = value;
			this.W = value;
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
			return (obj is Vector4) ? this == (Vector4)obj : false;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector4"/>.
		/// </summary>
		/// <param name="other">The <see cref="Vector4"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector4 other)
		{
			return this.W == other.W
				&& this.X == other.X
				&& this.Y == other.Y
				&& this.Z == other.Z;
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector4"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector4"/>.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				var hashCode = W.GetHashCode();
				hashCode = (hashCode * 397) ^ X.GetHashCode();
				hashCode = (hashCode * 397) ^ Y.GetHashCode();
				hashCode = (hashCode * 397) ^ Z.GetHashCode();
				return hashCode;
			}
		}

		#endregion

		#region Operators

		/// <summary>
		/// Compares whether two <see cref="Vector4"/> instances are equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector4"/> instance on the left of the equal sign.</param>
		/// <param name="value2"><see cref="Vector4"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public static bool operator ==(Vector4 value1, Vector4 value2)
		{
			return value1.W == value2.W
				&& value1.X == value2.X
				&& value1.Y == value2.Y
				&& value1.Z == value2.Z;
		}

		/// <summary>
		/// Compares whether two <see cref="Vector4"/> instances are not equal.
		/// </summary>
		/// <param name="value1"><see cref="Vector4"/> instance on the left of the not equal sign.</param>
		/// <param name="value2"><see cref="Vector4"/> instance on the right of the not equal sign.</param>
		/// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>	
		public static bool operator !=(Vector4 value1, Vector4 value2)
		{
			return !(value1 == value2);
		}

		#endregion
	}
}
