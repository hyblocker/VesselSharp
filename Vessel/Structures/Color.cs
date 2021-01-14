using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Diagnostics;
using System.Numerics;
using System.Text;
using System;

namespace Vessel
{
	/// <summary>
	/// Describes a 32-bit packed color.
	/// </summary>
	[DataContract]
	[DebuggerDisplay("{DebugDisplayString,nq}")]
	public struct Color : IEquatable<Color>
	{
		private Vector4 _channels;

		#region Constructors

		/// <summary>
		/// Constructs an RGBA color from a packed value.
		/// The value is a 32-bit unsigned integer, with R in the least significant octet.
		/// </summary>
		/// <param name="packedValue">The packed value.</param>
		public Color(uint packedValue)
		{
			_channels = new Vector4(
				 (byte) (packedValue) / 255f,
				((byte) (packedValue >> 8)) / 255f,
				((byte) (packedValue >> 16)) / 255f, 
				((byte) (packedValue >> 24)) / 255f);
		}

		/// <summary>
		/// Constructs an RGBA color from the XYZW unit length components of a vector.
		/// </summary>
		/// <param name="color">A <see cref="Vector4"/> representing color.</param>
		public Color(Vector4 color)
		{
			_channels = color;
		}

		/// <summary>
		/// Constructs an RGBA color from the XYZ unit length components of a vector. Alpha value will be opaque.
		/// </summary>
		/// <param name="color">A <see cref="Vector3"/> representing color.</param>
		public Color(Vector3 color)
		{
			_channels = new Vector4(color, 1f);
		}

		/// <summary>
		/// Constructs an RGBA color from a <see cref="Color"/> and an alpha value.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> for RGB values of new <see cref="Color"/> instance.</param>
		/// <param name="alpha">The alpha component value from 0 to 255.</param>
		public Color(Color color, int alpha)
		{
			_channels = color._channels;

			if ((alpha & 0xFFFFFF00) != 0)
			{
				var clampedA = (uint)Mathf.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

				_channels.W = clampedA / 255f;
			}
			else
			{
				_channels.W = alpha / 255f;
			}
		}

		/// <summary>
		/// Constructs an RGBA color from color and alpha value.
		/// </summary>
		/// <param name="color">A <see cref="Color"/> for RGB values of new <see cref="Color"/> instance.</param>
		/// <param name="alpha">Alpha component value from 0.0f to 1.0f.</param>
		public Color(Color color, float alpha) :
			this(color, (int)(alpha * 255))
		{
		}

		/// <summary>
		/// Constructs an RGBA color from scalars representing red, green and blue values. Alpha value will be opaque.
		/// </summary>
		/// <param name="r">Red component value from 0.0f to 1.0f.</param>
		/// <param name="g">Green component value from 0.0f to 1.0f.</param>
		/// <param name="b">Blue component value from 0.0f to 1.0f.</param>
		public Color(float r, float g, float b)
		{
			_channels = new Vector4(r, g, b, 1f);
		}

		/// <summary>
		/// Constructs an RGBA color from scalars representing red, green, blue and alpha values.
		/// </summary>
		/// <param name="r">Red component value from 0.0f to 1.0f.</param>
		/// <param name="g">Green component value from 0.0f to 1.0f.</param>
		/// <param name="b">Blue component value from 0.0f to 1.0f.</param>
		/// <param name="alpha">Alpha component value from 0.0f to 1.0f.</param>
		public Color(float r, float g, float b, float alpha)
		{
			_channels = new Vector4(r, g, b, alpha);
		}

		/// <summary>
		/// Constructs an RGBA color from scalars representing red, green and blue values. Alpha value will be opaque.
		/// </summary>
		/// <param name="r">Red component value from 0 to 255.</param>
		/// <param name="g">Green component value from 0 to 255.</param>
		/// <param name="b">Blue component value from 0 to 255.</param>
		public Color(int r, int g, int b)
		{
			if (((r | g | b) & 0xFFFFFF00) != 0)
			{
				var clampedR = (uint)Mathf.Clamp(r, Byte.MinValue, Byte.MaxValue);
				var clampedG = (uint)Mathf.Clamp(g, Byte.MinValue, Byte.MaxValue);
				var clampedB = (uint)Mathf.Clamp(b, Byte.MinValue, Byte.MaxValue);

				_channels = new Vector4(
					(byte)clampedR / 255f,
					(byte)clampedG / 255f,
					(byte)clampedB / 255f,
					1f);
			}
			else
			{
				_channels = new Vector4(
					(byte)r / 255f,
					(byte)g / 255f,
					(byte)b / 255f,
					1f);
			}
		}

		/// <summary>
		/// Constructs an RGBA color from scalars representing red, green, blue and alpha values.
		/// </summary>
		/// <param name="r">Red component value from 0 to 255.</param>
		/// <param name="g">Green component value from 0 to 255.</param>
		/// <param name="b">Blue component value from 0 to 255.</param>
		/// <param name="alpha">Alpha component value from 0 to 255.</param>
		public Color(int r, int g, int b, int alpha)
		{
			if (((r | g | b | alpha) & 0xFFFFFF00) != 0)
			{
				var clampedR = (uint)Mathf.Clamp(r, Byte.MinValue, Byte.MaxValue);
				var clampedG = (uint)Mathf.Clamp(g, Byte.MinValue, Byte.MaxValue);
				var clampedB = (uint)Mathf.Clamp(b, Byte.MinValue, Byte.MaxValue);
				var clampedA = (uint)Mathf.Clamp(alpha, Byte.MinValue, Byte.MaxValue);

				_channels = new Vector4(
					(byte)clampedR / 255f,
					(byte)clampedG / 255f,
					(byte)clampedB / 255f,
					(byte)clampedA / 255f);
			}
			else
			{
				_channels = new Vector4(
					(byte)r / 255f,
					(byte)g / 255f,
					(byte)b / 255f,
					(byte)alpha / 255f);
			}
		}

		/// <summary>
		/// Translate a non-premultipled alpha <see cref="Color"/> to a <see cref="Color"/> that contains premultiplied alpha.
		/// </summary>
		/// <param name="vector">A <see cref="Vector4"/> representing color.</param>
		/// <returns>A <see cref="Color"/> which contains premultiplied alpha data.</returns>
		public static Color FromNonPremultiplied(Vector4 vector)
		{
			return new Color(vector.X * vector.W, vector.Y * vector.W, vector.Z * vector.W, vector.W);
		}

		/// <summary>
		/// Translate a non-premultipled alpha <see cref="Color"/> to a <see cref="Color"/> that contains premultiplied alpha.
		/// </summary>
		/// <param name="r">Red component value.</param>
		/// <param name="g">Green component value.</param>
		/// <param name="b">Blue component value.</param>
		/// <param name="a">Alpha component value.</param>
		/// <returns>A <see cref="Color"/> which contains premultiplied alpha data.</returns>
		public static Color FromNonPremultiplied(int r, int g, int b, int a)
		{
			return new Color(r * a / 255, g * a / 255, b * a / 255, a);
		}

		#endregion

		#region Public Accessors

		/// <summary>
		/// Gets or sets the blue component.
		/// </summary>
		[DataMember]
		public byte B
		{
			get { return (byte)(255 * _channels.Z); }
			set { _channels.Z = value / 255f; }
		}

		/// <summary>
		/// Gets or sets the green component.
		/// </summary>
		[DataMember]
		public byte G
		{
			get { return (byte)(255 * _channels.Y); }
			set { _channels.Y = value / 255f; }
		}

		/// <summary>
		/// Gets or sets the red component.
		/// </summary>
		[DataMember]
		public byte R
		{
			get { return (byte)(255 * _channels.X); }
			set { _channels.X = value / 255f; }
		}

		/// <summary>
		/// Gets or sets the alpha component.
		/// </summary>
		[DataMember]
		public byte A
		{
			get { return (byte)(255 * _channels.W); }
			set { _channels.W = value / 255f; }
		}

		#endregion

		#region IEquatable Implementation

		/// <summary>
		/// Compares whether current instance is equal to specified <see cref="Color"/>.
		/// </summary>
		/// <param name="other">The <see cref="Color"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Color other)
		{
			return _channels.Equals(other._channels);
		}

		/// <summary>
		/// Compares whether two <see cref="Color"/> instances are equal.
		/// </summary>
		/// <param name="a"><see cref="Color"/> instance on the left of the equal sign.</param>
		/// <param name="b"><see cref="Color"/> instance on the right of the equal sign.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Color a, Color b)
		{
			return a.Equals(b);
		}

		/// <summary>
		/// Compares whether two <see cref="Color"/> instances are not equal.
		/// </summary>
		/// <param name="a"><see cref="Color"/> instance on the left of the not equal sign.</param>
		/// <param name="b"><see cref="Color"/> instance on the right of the not equal sign.</param>
		/// <returns><c>true</c> if the instances are not equal; <c>false</c> otherwise.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Color a, Color b)
		{
			return !a.Equals(b);
		}

		/// <summary>
		/// Gets the hash code of this <see cref="Color"/>.
		/// </summary>
		/// <returns>Hash code of this <see cref="Color"/>.</returns>
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			uint rol5 = ((uint)B.GetHashCode() << 5) | ((uint)B.GetHashCode() >> 27);
			int val = ((int)rol5 + B.GetHashCode()) ^ A.GetHashCode();

			rol5 = ((uint)G.GetHashCode() << 5) | ((uint)G.GetHashCode() >> 27);
			val = ((int)rol5 + G.GetHashCode()) ^ val;

			rol5 = ((uint)R.GetHashCode() << 5) | ((uint)R.GetHashCode() >> 27);
			return ((int)rol5 + R.GetHashCode()) ^ val;

			//return HashHelper.Combine(R.GetHashCode(), G.GetHashCode(), B.GetHashCode(), A.GetHashCode());
		}

		/// <summary>
		/// Compares whether current instance is equal to specified object.
		/// </summary>
		/// <param name="obj">The <see cref="Color"/> to compare.</param>
		/// <returns><c>true</c> if the instances are equal; <c>false</c> otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object obj)
		{
			return ((obj is Color) && this.Equals((Color)obj));
		}

		#endregion

		#region Util Functions

		internal string DebugDisplayString
		{
			get
			{
				return string.Concat(
					this.R.ToString(), "  ",
					this.G.ToString(), "  ",
					this.B.ToString(), "  ",
					this.A.ToString()
				);
			}
		}

		/// <summary>
		/// Returns a <see cref="String"/> representation of this <see cref="Color"/> in the format:
		/// {R:[red] G:[green] B:[blue] A:[alpha]}
		/// </summary>
		/// <returns><see cref="String"/> representation of this <see cref="Color"/>.</returns>
		public override string ToString()
		{
			StringBuilder sb = new StringBuilder(25);
			sb.Append("{R:");
			sb.Append(R);
			sb.Append(" G:");
			sb.Append(G);
			sb.Append(" B:");
			sb.Append(B);
			sb.Append(" A:");
			sb.Append(A);
			sb.Append("}");
			return sb.ToString();
		}

		public Veldrid.RgbaFloat ToVeldrid()
		{
			return new Veldrid.RgbaFloat(_channels);
		}

		/// <summary>
		/// Deconstruction method for <see cref="Color"/>.
		/// </summary>
		/// <param name="r">Red component value from 0 to 255.</param>
		/// <param name="g">Green component value from 0 to 255.</param>
		/// <param name="b">Blue component value from 0 to 255.</param>
		public void Deconstruct(out byte r, out byte g, out byte b)
		{
			r = R;
			g = G;
			b = B;
		}

		/// <summary>
		/// Deconstruction method for <see cref="Color"/>.
		/// </summary>
		/// <param name="r">Red component value from 0.0f to 1.0f.</param>
		/// <param name="g">Green component value from 0.0f to 1.0f.</param>
		/// <param name="b">Blue component value from 0.0f to 1.0f.</param>
		public void Deconstruct(out float r, out float g, out float b)
		{
			r = R / 255f;
			g = G / 255f;
			b = B / 255f;
		}

		/// <summary>
		/// Deconstruction method for <see cref="Color"/> with Alpha.
		/// </summary>
		/// <param name="r">Red component value from 0 to 255.</param>
		/// <param name="g">Green component value from 0 to 255.</param>
		/// <param name="b">Blue component value from 0 to 255.</param>
		/// <param name="a">Alpha component value from 0 to 255.</param>
		public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
		{
			r = R;
			g = G;
			b = B;
			a = A;
		}

		/// <summary>
		/// Deconstruction method for <see cref="Color"/> with Alpha.
		/// </summary>
		/// <param name="r">Red component value from 0.0f to 1.0f.</param>
		/// <param name="g">Green component value from 0.0f to 1.0f.</param>
		/// <param name="b">Blue component value from 0.0f to 1.0f.</param>
		/// <param name="a">Alpha component value from 0.0f to 1.0f.</param>
		public void Deconstruct(out float r, out float g, out float b, out float a)
		{
			r = R / 255f;
			g = G / 255f;
			b = B / 255f;
			a = A / 255f;
		}

		/// <summary>
		/// Gets a <see cref="Vector3"/> representation for this object.
		/// </summary>
		/// <returns>A <see cref="Vector3"/> representation for this object.</returns>
		public Vector3 ToVector3()
		{
			return new Vector3(R / 255.0f, G / 255.0f, B / 255.0f);
		}

		/// <summary>
		/// Gets a <see cref="Vector4"/> representation for this object.
		/// </summary>
		/// <returns>A <see cref="Vector4"/> representation for this object.</returns>
		public Vector4 ToVector4()
		{
			return new Vector4(R / 255.0f, G / 255.0f, B / 255.0f, A / 255.0f);
		}

		#endregion

		#region Math Functions

		/// <summary>
		/// Multiply <see cref="Color"/> by value.
		/// </summary>
		/// <param name="value">Source <see cref="Color"/>.</param>
		/// <param name="scale">Multiplicator.</param>
		/// <returns>Multiplication result.</returns>
		public static Color Multiply(Color value, float scale)
		{
			return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), (int)(value.A * scale));
		}

		/// <summary>
		/// Multiply <see cref="Color"/> by value.
		/// </summary>
		/// <param name="value">Source <see cref="Color"/>.</param>
		/// <param name="scale">Multiplicator.</param>
		/// <returns>Multiplication result.</returns>
		public static Color operator *(Color value, float scale)
		{
			return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), (int)(value.A * scale));
		}

		public static Color operator *(float scale, Color value)
		{
			return new Color((int)(value.R * scale), (int)(value.G * scale), (int)(value.B * scale), (int)(value.A * scale));
		}

		/// <summary>
		/// Performs linear interpolation of <see cref="Color"/>.
		/// </summary>
		/// <param name="value1">Source <see cref="Color"/>.</param>
		/// <param name="value2">Destination <see cref="Color"/>.</param>
		/// <param name="amount">Interpolation factor.</param>
		/// <returns>Interpolated <see cref="Color"/>.</returns>
		public static Color Lerp(Color value1, Color value2, Single amount)
		{
			amount = Mathf.Clamp(amount, 0, 1);
			return new Color(
				(int)Mathf.Lerp(value1.R, value2.R, amount),
				(int)Mathf.Lerp(value1.G, value2.G, amount),
				(int)Mathf.Lerp(value1.B, value2.B, amount),
				(int)Mathf.Lerp(value1.A, value2.A, amount));
		}

		/// <summary>
		/// <see cref="Color.Lerp"/> should be used instead of this function.
		/// </summary>
		/// <returns>Interpolated <see cref="Color"/>.</returns>
		[Obsolete("Color.Lerp should be used instead of this function.")]
		public static Color LerpPrecise(Color value1, Color value2, Single amount)
		{
			amount = Mathf.Clamp(amount, 0, 1);
			return new Color(
				(int)Mathf.LerpPrecise(value1.R, value2.R, amount),
				(int)Mathf.LerpPrecise(value1.G, value2.G, amount),
				(int)Mathf.LerpPrecise(value1.B, value2.B, amount),
				(int)Mathf.LerpPrecise(value1.A, value2.A, amount));
		}

		#endregion

		#region Commonly Used Colors

		//USING XNA COLOR CHART:
		//REF: http://www.foszor.com/blog/xna-color-chart/

		static Color()
		{
			Transparent = new Color(0, 0, 0, 0);
			AliceBlue = new Color(240, 248, 255, 255);
			AntiqueWhite = new Color(250, 235, 215, 255);
			Aqua = new Color(0, 255, 255, 255);
			Aquamarine = new Color(127, 255, 212, 255);
			Azure = new Color(240, 255, 255, 255);
			Beige = new Color(245, 245, 220, 255);
			Bisque = new Color(255, 228, 196, 255);
			Black = new Color(0, 0, 0, 255);
			BlanchedAlmond = new Color(255, 235, 205, 255);
			Blue = new Color(0, 0, 255, 255);
			BlueViolet = new Color(138, 43, 226, 255);
			Brown = new Color(165, 42, 42, 255);
			BurlyWood = new Color(222, 184, 135, 255);
			CadetBlue = new Color(95, 158, 160, 255);
			Chartreuse = new Color(127, 255, 0, 255);
			Chocolate = new Color(210, 105, 30, 255);
			Coral = new Color(255, 127, 80, 255);
			CornflowerBlue = new Color(100, 149, 237, 255);
			Cornsilk = new Color(255, 248, 220, 255);
			Crimson = new Color(220, 20, 60, 255);
			Cyan = new Color(0, 255, 255, 255);
			DarkBlue = new Color(0, 0, 139, 255);
			DarkCyan = new Color(0, 139, 139, 255);
			DarkGoldenrod = new Color(184, 134, 11, 255);
			DarkGray = new Color(169, 169, 169, 255);
			DarkGreen = new Color(0, 100, 0, 255);
			DarkKhaki = new Color(189, 183, 107, 255);
			DarkMagenta = new Color(139, 0, 139, 255);
			DarkOliveGreen = new Color(85, 107, 47, 255);
			DarkOrange = new Color(255, 140, 0, 255);
			DarkOrchid = new Color(153, 50, 204, 255);
			DarkRed = new Color(139, 0, 0, 255);
			DarkSalmon = new Color(233, 150, 122, 255);
			DarkSeaGreen = new Color(143, 188, 139, 255);
			DarkSlateBlue = new Color(72, 61, 139, 255);
			DarkSlateGray = new Color(47, 79, 79, 255);
			DarkTurquoise = new Color(0, 206, 209, 255);
			DarkViolet = new Color(148, 0, 211, 255);
			DeepPink = new Color(255, 20, 147, 255);
			DeepSkyBlue = new Color(0, 191, 255, 255);
			DimGray = new Color(105, 105, 105, 255);
			DodgerBlue = new Color(30, 144, 255, 255);
			Firebrick = new Color(178, 34, 34, 255);
			FloralWhite = new Color(255, 250, 240, 255);
			ForestGreen = new Color(34, 139, 34, 255);
			Fuchsia = new Color(255, 0, 255, 255);
			Gainsboro = new Color(220, 220, 220, 255);
			GhostWhite = new Color(248, 248, 255, 255);
			Gold = new Color(255, 215, 0, 255);
			Goldenrod = new Color(218, 165, 32, 255);
			Gray = new Color(128, 128, 128, 255);
			Green = new Color(0, 128, 0, 255);
			GreenYellow = new Color(173, 255, 47, 255);
			Honeydew = new Color(240, 255, 240, 255);
			HotPink = new Color(255, 105, 180, 255);
			IndianRed = new Color(205, 92, 92, 255);
			Indigo = new Color(75, 0, 130, 255);
			Ivory = new Color(255, 255, 240, 255);
			Khaki = new Color(240, 230, 140, 255);
			Lavender = new Color(230, 230, 250, 255);
			LavenderBlush = new Color(255, 240, 245, 255);
			LawnGreen = new Color(124, 252, 0, 255);
			LemonChiffon = new Color(255, 250, 205, 255);
			LightBlue = new Color(173, 216, 230, 255);
			LightCoral = new Color(240, 128, 128, 255);
			LightCyan = new Color(224, 255, 255, 255);
			LightGoldenrodYellow = new Color(250, 250, 210, 255);
			LightGreen = new Color(144, 238, 144, 255);
			LightGray = new Color(211, 211, 211, 255);
			LightPink = new Color(255, 182, 193, 255);
			LightSalmon = new Color(255, 160, 122, 255);
			LightSeaGreen = new Color(32, 178, 170, 255);
			LightSkyBlue = new Color(135, 206, 250, 255);
			LightSlateGray = new Color(119, 136, 153, 255);
			LightSteelBlue = new Color(176, 196, 222, 255);
			LightYellow = new Color(255, 255, 224, 255);
			Lime = new Color(0, 255, 0, 255);
			LimeGreen = new Color(50, 205, 50, 255);
			Linen = new Color(250, 240, 230, 255);
			Magenta = new Color(255, 0, 255, 255);
			Maroon = new Color(128, 0, 0, 255);
			MediumAquamarine = new Color(102, 205, 170, 255);
			MediumBlue = new Color(0, 0, 205, 255);
			MediumOrchid = new Color(186, 85, 211, 255);
			MediumPurple = new Color(147, 112, 219, 255);
			MediumSeaGreen = new Color(60, 179, 113, 255);
			MediumSlateBlue = new Color(123, 104, 238, 255);
			MediumSpringGreen = new Color(0, 250, 154, 255);
			MediumTurquoise = new Color(72, 209, 204, 255);
			MediumVioletRed = new Color(199, 21, 133, 255);
			MidnightBlue = new Color(25, 25, 112, 255);
			MintCream = new Color(245, 255, 250, 255);
			MistyRose = new Color(255, 228, 225, 255);
			Moccasin = new Color(255, 228, 181, 255);
			NavajoWhite = new Color(255, 222, 173, 255);
			Navy = new Color(0, 0, 128, 255);
			OldLace = new Color(253, 245, 230, 255);
			Olive = new Color(128, 128, 0, 255);
			OliveDrab = new Color(107, 142, 35, 255);
			Orange = new Color(255, 165, 0, 255);
			OrangeRed = new Color(255, 69, 0, 255);
			Orchid = new Color(218, 112, 214, 255);
			PaleGoldenrod = new Color(238, 232, 170, 255);
			PaleGreen = new Color(152, 251, 152, 255);
			PaleTurquoise = new Color(175, 238, 238, 255);
			PaleVioletRed = new Color(219, 112, 147, 255);
			PapayaWhip = new Color(255, 239, 213, 255);
			PeachPuff = new Color(255, 218, 185, 255);
			Peru = new Color(205, 133, 63, 255);
			Pink = new Color(255, 192, 203, 255);
			Plum = new Color(221, 160, 221, 255);
			PowderBlue = new Color(176, 224, 230, 255);
			Purple = new Color(128, 0, 128, 255);
			Red = new Color(255, 0, 0, 255);
			RosyBrown = new Color(188, 143, 143, 255);
			RoyalBlue = new Color(65, 105, 225, 255);
			SaddleBrown = new Color(139, 69, 19, 255);
			Salmon = new Color(250, 128, 114, 255);
			SandyBrown = new Color(244, 164, 96, 255);
			SeaGreen = new Color(46, 139, 87, 255);
			SeaShell = new Color(255, 245, 238, 255);
			Sienna = new Color(160, 82, 45, 255);
			Silver = new Color(192, 192, 192, 255);
			SkyBlue = new Color(135, 206, 235, 255);
			SlateBlue = new Color(106, 90, 205, 255);
			SlateGray = new Color(112, 128, 144, 255);
			Snow = new Color(255, 250, 250, 255);
			SpringGreen = new Color(0, 255, 127, 255);
			SteelBlue = new Color(70, 130, 180, 255);
			Tan = new Color(210, 180, 140, 255);
			Teal = new Color(0, 128, 128, 255);
			Thistle = new Color(216, 191, 216, 255);
			Tomato = new Color(255, 99, 71, 255);
			Turquoise = new Color(64, 224, 208, 255);
			Violet = new Color(238, 130, 238, 255);
			Wheat = new Color(245, 222, 179, 255);
			White = new Color(255, 255, 255, 255);
			WhiteSmoke = new Color(245, 245, 245, 255);
			Yellow = new Color(255, 255, 0, 255);
			YellowGreen = new Color(154, 205, 50, 255);
		}

		/// <summary>
		/// Transparent color (R:0,G:0,B:0,A:0).
		/// </summary>
		public static Color Transparent { get; private set; }

		/// <summary>
		/// AliceBlue color (R:240,G:248,B:255,A:255).
		/// </summary>
		public static Color AliceBlue { get; private set; }

		/// <summary>
		/// AntiqueWhite color (R:250,G:235,B:215,A:255).
		/// </summary>
		public static Color AntiqueWhite { get; private set; }

		/// <summary>
		/// Aqua color (R:0,G:255,B:255,A:255).
		/// </summary>
		public static Color Aqua { get; private set; }

		/// <summary>
		/// Aquamarine color (R:127,G:255,B:212,A:255).
		/// </summary>
		public static Color Aquamarine { get; private set; }

		/// <summary>
		/// Azure color (R:240,G:255,B:255,A:255).
		/// </summary>
		public static Color Azure { get; private set; }

		/// <summary>
		/// Beige color (R:245,G:245,B:220,A:255).
		/// </summary>
		public static Color Beige { get; private set; }

		/// <summary>
		/// Bisque color (R:255,G:228,B:196,A:255).
		/// </summary>
		public static Color Bisque { get; private set; }

		/// <summary>
		/// Black color (R:0,G:0,B:0,A:255).
		/// </summary>
		public static Color Black { get; private set; }

		/// <summary>
		/// BlanchedAlmond color (R:255,G:235,B:205,A:255).
		/// </summary>
		public static Color BlanchedAlmond { get; private set; }

		/// <summary>
		/// Blue color (R:0,G:0,B:255,A:255).
		/// </summary>
		public static Color Blue { get; private set; }

		/// <summary>
		/// BlueViolet color (R:138,G:43,B:226,A:255).
		/// </summary>
		public static Color BlueViolet { get; private set; }

		/// <summary>
		/// Brown color (R:165,G:42,B:42,A:255).
		/// </summary>
		public static Color Brown { get; private set; }

		/// <summary>
		/// BurlyWood color (R:222,G:184,B:135,A:255).
		/// </summary>
		public static Color BurlyWood { get; private set; }

		/// <summary>
		/// CadetBlue color (R:95,G:158,B:160,A:255).
		/// </summary>
		public static Color CadetBlue { get; private set; }

		/// <summary>
		/// Chartreuse color (R:127,G:255,B:0,A:255).
		/// </summary>
		public static Color Chartreuse { get; private set; }

		/// <summary>
		/// Chocolate color (R:210,G:105,B:30,A:255).
		/// </summary>
		public static Color Chocolate { get; private set; }

		/// <summary>
		/// Coral color (R:255,G:127,B:80,A:255).
		/// </summary>
		public static Color Coral { get; private set; }

		/// <summary>
		/// CornflowerBlue color (R:100,G:149,B:237,A:255).
		/// </summary>
		public static Color CornflowerBlue { get; private set; }

		/// <summary>
		/// Cornsilk color (R:255,G:248,B:220,A:255).
		/// </summary>
		public static Color Cornsilk { get; private set; }

		/// <summary>
		/// Crimson color (R:220,G:20,B:60,A:255).
		/// </summary>
		public static Color Crimson { get; private set; }

		/// <summary>
		/// Cyan color (R:0,G:255,B:255,A:255).
		/// </summary>
		public static Color Cyan { get; private set; }

		/// <summary>
		/// DarkBlue color (R:0,G:0,B:139,A:255).
		/// </summary>
		public static Color DarkBlue { get; private set; }

		/// <summary>
		/// DarkCyan color (R:0,G:139,B:139,A:255).
		/// </summary>
		public static Color DarkCyan { get; private set; }

		/// <summary>
		/// DarkGoldenrod color (R:184,G:134,B:11,A:255).
		/// </summary>
		public static Color DarkGoldenrod { get; private set; }

		/// <summary>
		/// DarkGray color (R:169,G:169,B:169,A:255).
		/// </summary>
		public static Color DarkGray { get; private set; }

		/// <summary>
		/// DarkGreen color (R:0,G:100,B:0,A:255).
		/// </summary>
		public static Color DarkGreen { get; private set; }

		/// <summary>
		/// DarkKhaki color (R:189,G:183,B:107,A:255).
		/// </summary>
		public static Color DarkKhaki { get; private set; }

		/// <summary>
		/// DarkMagenta color (R:139,G:0,B:139,A:255).
		/// </summary>
		public static Color DarkMagenta { get; private set; }

		/// <summary>
		/// DarkOliveGreen color (R:85,G:107,B:47,A:255).
		/// </summary>
		public static Color DarkOliveGreen { get; private set; }

		/// <summary>
		/// DarkOrange color (R:255,G:140,B:0,A:255).
		/// </summary>
		public static Color DarkOrange { get; private set; }

		/// <summary>
		/// DarkOrchid color (R:153,G:50,B:204,A:255).
		/// </summary>
		public static Color DarkOrchid { get; private set; }

		/// <summary>
		/// DarkRed color (R:139,G:0,B:0,A:255).
		/// </summary>
		public static Color DarkRed { get; private set; }

		/// <summary>
		/// DarkSalmon color (R:233,G:150,B:122,A:255).
		/// </summary>
		public static Color DarkSalmon { get; private set; }

		/// <summary>
		/// DarkSeaGreen color (R:143,G:188,B:139,A:255).
		/// </summary>
		public static Color DarkSeaGreen { get; private set; }

		/// <summary>
		/// DarkSlateBlue color (R:72,G:61,B:139,A:255).
		/// </summary>
		public static Color DarkSlateBlue { get; private set; }

		/// <summary>
		/// DarkSlateGray color (R:47,G:79,B:79,A:255).
		/// </summary>
		public static Color DarkSlateGray { get; private set; }

		/// <summary>
		/// DarkTurquoise color (R:0,G:206,B:209,A:255).
		/// </summary>
		public static Color DarkTurquoise { get; private set; }

		/// <summary>
		/// DarkViolet color (R:148,G:0,B:211,A:255).
		/// </summary>
		public static Color DarkViolet { get; private set; }

		/// <summary>
		/// DeepPink color (R:255,G:20,B:147,A:255).
		/// </summary>
		public static Color DeepPink { get; private set; }

		/// <summary>
		/// DeepSkyBlue color (R:0,G:191,B:255,A:255).
		/// </summary>
		public static Color DeepSkyBlue { get; private set; }

		/// <summary>
		/// DimGray color (R:105,G:105,B:105,A:255).
		/// </summary>
		public static Color DimGray { get; private set; }

		/// <summary>
		/// DodgerBlue color (R:30,G:144,B:255,A:255).
		/// </summary>
		public static Color DodgerBlue { get; private set; }

		/// <summary>
		/// Firebrick color (R:178,G:34,B:34,A:255).
		/// </summary>
		public static Color Firebrick { get; private set; }

		/// <summary>
		/// FloralWhite color (R:255,G:250,B:240,A:255).
		/// </summary>
		public static Color FloralWhite { get; private set; }

		/// <summary>
		/// ForestGreen color (R:34,G:139,B:34,A:255).
		/// </summary>
		public static Color ForestGreen { get; private set; }

		/// <summary>
		/// Fuchsia color (R:255,G:0,B:255,A:255).
		/// </summary>
		public static Color Fuchsia { get; private set; }

		/// <summary>
		/// Gainsboro color (R:220,G:220,B:220,A:255).
		/// </summary>
		public static Color Gainsboro { get; private set; }

		/// <summary>
		/// GhostWhite color (R:248,G:248,B:255,A:255).
		/// </summary>
		public static Color GhostWhite { get; private set; }
		/// <summary>
		/// Gold color (R:255,G:215,B:0,A:255).
		/// </summary>
		public static Color Gold { get; private set; }

		/// <summary>
		/// Goldenrod color (R:218,G:165,B:32,A:255).
		/// </summary>
		public static Color Goldenrod { get; private set; }

		/// <summary>
		/// Gray color (R:128,G:128,B:128,A:255).
		/// </summary>
		public static Color Gray { get; private set; }

		/// <summary>
		/// Green color (R:0,G:128,B:0,A:255).
		/// </summary>
		public static Color Green { get; private set; }

		/// <summary>
		/// GreenYellow color (R:173,G:255,B:47,A:255).
		/// </summary>
		public static Color GreenYellow { get; private set; }

		/// <summary>
		/// Honeydew color (R:240,G:255,B:240,A:255).
		/// </summary>
		public static Color Honeydew { get; private set; }

		/// <summary>
		/// HotPink color (R:255,G:105,B:180,A:255).
		/// </summary>
		public static Color HotPink { get; private set; }

		/// <summary>
		/// IndianRed color (R:205,G:92,B:92,A:255).
		/// </summary>
		public static Color IndianRed { get; private set; }

		/// <summary>
		/// Indigo color (R:75,G:0,B:130,A:255).
		/// </summary>
		public static Color Indigo { get; private set; }

		/// <summary>
		/// Ivory color (R:255,G:255,B:240,A:255).
		/// </summary>
		public static Color Ivory { get; private set; }

		/// <summary>
		/// Khaki color (R:240,G:230,B:140,A:255).
		/// </summary>
		public static Color Khaki { get; private set; }

		/// <summary>
		/// Lavender color (R:230,G:230,B:250,A:255).
		/// </summary>
		public static Color Lavender { get; private set; }

		/// <summary>
		/// LavenderBlush color (R:255,G:240,B:245,A:255).
		/// </summary>
		public static Color LavenderBlush { get; private set; }

		/// <summary>
		/// LawnGreen color (R:124,G:252,B:0,A:255).
		/// </summary>
		public static Color LawnGreen { get; private set; }

		/// <summary>
		/// LemonChiffon color (R:255,G:250,B:205,A:255).
		/// </summary>
		public static Color LemonChiffon { get; private set; }

		/// <summary>
		/// LightBlue color (R:173,G:216,B:230,A:255).
		/// </summary>
		public static Color LightBlue { get; private set; }

		/// <summary>
		/// LightCoral color (R:240,G:128,B:128,A:255).
		/// </summary>
		public static Color LightCoral { get; private set; }

		/// <summary>
		/// LightCyan color (R:224,G:255,B:255,A:255).
		/// </summary>
		public static Color LightCyan { get; private set; }

		/// <summary>
		/// LightGoldenrodYellow color (R:250,G:250,B:210,A:255).
		/// </summary>
		public static Color LightGoldenrodYellow { get; private set; }

		/// <summary>
		/// LightGray color (R:211,G:211,B:211,A:255).
		/// </summary>
		public static Color LightGray { get; private set; }

		/// <summary>
		/// LightGreen color (R:144,G:238,B:144,A:255).
		/// </summary>
		public static Color LightGreen { get; private set; }

		/// <summary>
		/// LightPink color (R:255,G:182,B:193,A:255).
		/// </summary>
		public static Color LightPink { get; private set; }

		/// <summary>
		/// LightSalmon color (R:255,G:160,B:122,A:255).
		/// </summary>
		public static Color LightSalmon { get; private set; }

		/// <summary>
		/// LightSeaGreen color (R:32,G:178,B:170,A:255).
		/// </summary>
		public static Color LightSeaGreen { get; private set; }

		/// <summary>
		/// LightSkyBlue color (R:135,G:206,B:250,A:255).
		/// </summary>
		public static Color LightSkyBlue { get; private set; }

		/// <summary>
		/// LightSlateGray color (R:119,G:136,B:153,A:255).
		/// </summary>
		public static Color LightSlateGray { get; private set; }

		/// <summary>
		/// LightSteelBlue color (R:176,G:196,B:222,A:255).
		/// </summary>
		public static Color LightSteelBlue { get; private set; }

		/// <summary>
		/// LightYellow color (R:255,G:255,B:224,A:255).
		/// </summary>
		public static Color LightYellow { get; private set; }

		/// <summary>
		/// Lime color (R:0,G:255,B:0,A:255).
		/// </summary>
		public static Color Lime { get; private set; }

		/// <summary>
		/// LimeGreen color (R:50,G:205,B:50,A:255).
		/// </summary>
		public static Color LimeGreen { get; private set; }

		/// <summary>
		/// Linen color (R:250,G:240,B:230,A:255).
		/// </summary>
		public static Color Linen { get; private set; }

		/// <summary>
		/// Magenta color (R:255,G:0,B:255,A:255).
		/// </summary>
		public static Color Magenta { get; private set; }

		/// <summary>
		/// Maroon color (R:128,G:0,B:0,A:255).
		/// </summary>
		public static Color Maroon { get; private set; }

		/// <summary>
		/// MediumAquamarine color (R:102,G:205,B:170,A:255).
		/// </summary>
		public static Color MediumAquamarine { get; private set; }

		/// <summary>
		/// MediumBlue color (R:0,G:0,B:205,A:255).
		/// </summary>
		public static Color MediumBlue { get; private set; }

		/// <summary>
		/// MediumOrchid color (R:186,G:85,B:211,A:255).
		/// </summary>
		public static Color MediumOrchid { get; private set; }

		/// <summary>
		/// MediumPurple color (R:147,G:112,B:219,A:255).
		/// </summary>
		public static Color MediumPurple { get; private set; }

		/// <summary>
		/// MediumSeaGreen color (R:60,G:179,B:113,A:255).
		/// </summary>
		public static Color MediumSeaGreen { get; private set; }

		/// <summary>
		/// MediumSlateBlue color (R:123,G:104,B:238,A:255).
		/// </summary>
		public static Color MediumSlateBlue { get; private set; }

		/// <summary>
		/// MediumSpringGreen color (R:0,G:250,B:154,A:255).
		/// </summary>
		public static Color MediumSpringGreen { get; private set; }

		/// <summary>
		/// MediumTurquoise color (R:72,G:209,B:204,A:255).
		/// </summary>
		public static Color MediumTurquoise { get; private set; }

		/// <summary>
		/// MediumVioletRed color (R:199,G:21,B:133,A:255).
		/// </summary>
		public static Color MediumVioletRed { get; private set; }

		/// <summary>
		/// MidnightBlue color (R:25,G:25,B:112,A:255).
		/// </summary>
		public static Color MidnightBlue { get; private set; }

		/// <summary>
		/// MintCream color (R:245,G:255,B:250,A:255).
		/// </summary>
		public static Color MintCream { get; private set; }

		/// <summary>
		/// MistyRose color (R:255,G:228,B:225,A:255).
		/// </summary>
		public static Color MistyRose { get; private set; }

		/// <summary>
		/// Moccasin color (R:255,G:228,B:181,A:255).
		/// </summary>
		public static Color Moccasin { get; private set; }

		/// <summary>
		/// MonoGame orange theme color (R:231,G:60,B:0,A:255).
		/// </summary>
		public static Color MonoGameOrange { get; private set; }

		/// <summary>
		/// NavajoWhite color (R:255,G:222,B:173,A:255).
		/// </summary>
		public static Color NavajoWhite { get; private set; }

		/// <summary>
		/// Navy color (R:0,G:0,B:128,A:255).
		/// </summary>
		public static Color Navy { get; private set; }

		/// <summary>
		/// OldLace color (R:253,G:245,B:230,A:255).
		/// </summary>
		public static Color OldLace { get; private set; }

		/// <summary>
		/// Olive color (R:128,G:128,B:0,A:255).
		/// </summary>
		public static Color Olive { get; private set; }

		/// <summary>
		/// OliveDrab color (R:107,G:142,B:35,A:255).
		/// </summary>
		public static Color OliveDrab { get; private set; }

		/// <summary>
		/// Orange color (R:255,G:165,B:0,A:255).
		/// </summary>
		public static Color Orange { get; private set; }

		/// <summary>
		/// OrangeRed color (R:255,G:69,B:0,A:255).
		/// </summary>
		public static Color OrangeRed { get; private set; }

		/// <summary>
		/// Orchid color (R:218,G:112,B:214,A:255).
		/// </summary>
		public static Color Orchid { get; private set; }

		/// <summary>
		/// PaleGoldenrod color (R:238,G:232,B:170,A:255).
		/// </summary>
		public static Color PaleGoldenrod { get; private set; }

		/// <summary>
		/// PaleGreen color (R:152,G:251,B:152,A:255).
		/// </summary>
		public static Color PaleGreen { get; private set; }

		/// <summary>
		/// PaleTurquoise color (R:175,G:238,B:238,A:255).
		/// </summary>
		public static Color PaleTurquoise { get; private set; }
		/// <summary>
		/// PaleVioletRed color (R:219,G:112,B:147,A:255).
		/// </summary>
		public static Color PaleVioletRed { get; private set; }

		/// <summary>
		/// PapayaWhip color (R:255,G:239,B:213,A:255).
		/// </summary>
		public static Color PapayaWhip { get; private set; }

		/// <summary>
		/// PeachPuff color (R:255,G:218,B:185,A:255).
		/// </summary>
		public static Color PeachPuff { get; private set; }

		/// <summary>
		/// Peru color (R:205,G:133,B:63,A:255).
		/// </summary>
		public static Color Peru { get; private set; }

		/// <summary>
		/// Pink color (R:255,G:192,B:203,A:255).
		/// </summary>
		public static Color Pink { get; private set; }

		/// <summary>
		/// Plum color (R:221,G:160,B:221,A:255).
		/// </summary>
		public static Color Plum { get; private set; }

		/// <summary>
		/// PowderBlue color (R:176,G:224,B:230,A:255).
		/// </summary>
		public static Color PowderBlue { get; private set; }

		/// <summary>
		///  Purple color (R:128,G:0,B:128,A:255).
		/// </summary>
		public static Color Purple { get; private set; }

		/// <summary>
		/// Red color (R:255,G:0,B:0,A:255).
		/// </summary>
		public static Color Red { get; private set; }

		/// <summary>
		/// RosyBrown color (R:188,G:143,B:143,A:255).
		/// </summary>
		public static Color RosyBrown { get; private set; }

		/// <summary>
		/// RoyalBlue color (R:65,G:105,B:225,A:255).
		/// </summary>
		public static Color RoyalBlue { get; private set; }

		/// <summary>
		/// SaddleBrown color (R:139,G:69,B:19,A:255).
		/// </summary>
		public static Color SaddleBrown { get; private set; }

		/// <summary>
		/// Salmon color (R:250,G:128,B:114,A:255).
		/// </summary>
		public static Color Salmon { get; private set; }

		/// <summary>
		/// SandyBrown color (R:244,G:164,B:96,A:255).
		/// </summary>
		public static Color SandyBrown { get; private set; }

		/// <summary>
		/// SeaGreen color (R:46,G:139,B:87,A:255).
		/// </summary>
		public static Color SeaGreen { get; private set; }

		/// <summary>
		/// SeaShell color (R:255,G:245,B:238,A:255).
		/// </summary>
		public static Color SeaShell { get; private set; }

		/// <summary>
		/// Sienna color (R:160,G:82,B:45,A:255).
		/// </summary>
		public static Color Sienna { get; private set; }

		/// <summary>
		/// Silver color (R:192,G:192,B:192,A:255).
		/// </summary>
		public static Color Silver { get; private set; }

		/// <summary>
		/// SkyBlue color (R:135,G:206,B:235,A:255).
		/// </summary>
		public static Color SkyBlue { get; private set; }

		/// <summary>
		/// SlateBlue color (R:106,G:90,B:205,A:255).
		/// </summary>
		public static Color SlateBlue { get; private set; }

		/// <summary>
		/// SlateGray color (R:112,G:128,B:144,A:255).
		/// </summary>
		public static Color SlateGray { get; private set; }

		/// <summary>
		/// Snow color (R:255,G:250,B:250,A:255).
		/// </summary>
		public static Color Snow { get; private set; }

		/// <summary>
		/// SpringGreen color (R:0,G:255,B:127,A:255).
		/// </summary>
		public static Color SpringGreen { get; private set; }

		/// <summary>
		/// SteelBlue color (R:70,G:130,B:180,A:255).
		/// </summary>
		public static Color SteelBlue { get; private set; }

		/// <summary>
		/// Tan color (R:210,G:180,B:140,A:255).
		/// </summary>
		public static Color Tan { get; private set; }

		/// <summary>
		/// Teal color (R:0,G:128,B:128,A:255).
		/// </summary>
		public static Color Teal { get; private set; }

		/// <summary>
		/// Thistle color (R:216,G:191,B:216,A:255).
		/// </summary>
		public static Color Thistle { get; private set; }

		/// <summary>
		/// Tomato color (R:255,G:99,B:71,A:255).
		/// </summary>
		public static Color Tomato { get; private set; }

		/// <summary>
		/// Turquoise color (R:64,G:224,B:208,A:255).
		/// </summary>
		public static Color Turquoise { get; private set; }

		/// <summary>
		/// Violet color (R:238,G:130,B:238,A:255).
		/// </summary>
		public static Color Violet { get; private set; }

		/// <summary>
		/// Wheat color (R:245,G:222,B:179,A:255).
		/// </summary>
		public static Color Wheat { get; private set; }

		/// <summary>
		/// White color (R:255,G:255,B:255,A:255).
		/// </summary>
		public static Color White { get; private set; }

		/// <summary>
		/// WhiteSmoke color (R:245,G:245,B:245,A:255).
		/// </summary>
		public static Color WhiteSmoke { get; private set; }

		/// <summary>
		/// Yellow color (R:255,G:255,B:0,A:255).
		/// </summary>
		public static Color Yellow { get; private set; }

		/// <summary>
		/// YellowGreen color (R:154,G:205,B:50,A:255).
		/// </summary>
		public static Color YellowGreen { get; private set; }

		#endregion
	}
}
