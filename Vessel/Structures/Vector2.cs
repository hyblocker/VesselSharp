using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Vessel
{
	/// <summary>
	/// Describes a 2D-vector.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Vector2 : IEquatable<Vector2>
	{
		#region Public Fields

		/// <summary>
		/// The x coordinate of this <see cref="Vector2"/>.
		/// </summary>
		[DataMember]
		public float X;

		/// <summary>
		/// The y coordinate of this <see cref="Vector2"/>.
		/// </summary>
		[DataMember]
		public float Y;

		#endregion

		#region Internal Properties

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					this.X.ToString(), "  ",
					this.Y.ToString()
				);
			}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Constructs a 2d vector with X and Y from two values.
		/// </summary>
		/// <param name="x">The x coordinate in 2d-space.</param>
		/// <param name="y">The y coordinate in 2d-space.</param>
		public Vector2(float x, float y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Constructs a 2d vector with X and Y set to the same value.
		/// </summary>
		/// <param name="value">The x and y coordinates in 2d-space.</param>
		public Vector2(float value)
		{
			this.X = value;
			this.Y = value;
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
			if (obj is Vector2)
			{
				return Equals((Vector2)obj);
			}

			return false;
		}

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Vector2"/>.
		/// </summary>
		/// <param name="other">The <see cref="Vector2"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		public bool Equals(Vector2 other)
		{
			return (X == other.X) && (Y == other.Y);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Vector2"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Vector2"/>.</returns>
		public override int GetHashCode()
		{
			unchecked
			{
				return (X.GetHashCode() * 397) ^ Y.GetHashCode();
			}
		}

		#endregion

	}
}
