using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vessel
{
	public static class Mathf
	{

		#region Clamping
		
		/// <summary>
		/// Restricts a value to be within a specified range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
		/// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
		/// <returns>The clamped value.</returns>
		public static float Clamp(float value, float min, float max)
		{
			// First we check to see if we're greater than the max
			value = (value > max) ? max : value;

			// Then we check to see if we're less than the min.
			value = (value < min) ? min : value;

			// There's no check to see if min > max.
			return value;
		}

		/// <summary>
		/// Restricts a value to be within a specified range.
		/// </summary>
		/// <param name="value">The value to clamp.</param>
		/// <param name="min">The minimum value. If <c>value</c> is less than <c>min</c>, <c>min</c> will be returned.</param>
		/// <param name="max">The maximum value. If <c>value</c> is greater than <c>max</c>, <c>max</c> will be returned.</param>
		/// <returns>The clamped value.</returns>
		public static int Clamp(int value, int min, int max)
		{
			value = (value > max) ? max : value;
			value = (value < min) ? min : value;
			return value;
		}

		#endregion

		#region Lerping

		/// <summary>
		/// Linearly interpolates between two values.
		/// </summary>
		/// <param name="value1">Source value.</param>
		/// <param name="value2">Destination value.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
		/// <returns>Interpolated value.</returns> 
		/// <remarks>This method performs the linear interpolation based on the following formula:
		/// <code>value1 + (value2 - value1) * amount</code>.
		/// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
		/// See <see cref="MathHelper.LerpPrecise"/> for a less efficient version with more precision around edge cases.
		/// </remarks>
		public static float Lerp(float value1, float value2, float amount)
		{
			return value1 + (value2 - value1) * amount;
		}


		/// <summary>
		/// Linearly interpolates between two values.
		/// This method is a less efficient, more precise version of <see cref="Mathf.Lerp"/>.
		/// See remarks for more info.
		/// </summary>
		/// <param name="value1">Source value.</param>
		/// <param name="value2">Destination value.</param>
		/// <param name="amount">Value between 0 and 1 indicating the weight of value2.</param>
		/// <returns>Interpolated value.</returns>
		/// <remarks>This method performs the linear interpolation based on the following formula:
		/// <code>((1 - amount) * value1) + (value2 * amount)</code>.
		/// Passing amount a value of 0 will cause value1 to be returned, a value of 1 will cause value2 to be returned.
		/// This method does not have the floating point precision issue that <see cref="Mathf.Lerp"/> has.
		/// i.e. If there is a big gap between value1 and value2 in magnitude (e.g. value1=10000000000000000, value2=1),
		/// right at the edge of the interpolation range (amount=1), <see cref="Mathf.Lerp"/> will return 0 (whereas it should return 1).
		/// This also holds for value1=10^17, value2=10; value1=10^18,value2=10^2... so on.
		/// For an in depth explanation of the issue, see below references:
		/// Relevant Wikipedia Article: https://en.wikipedia.org/wiki/Linear_interpolation#Programming_language_support
		/// Relevant StackOverflow Answer: http://stackoverflow.com/questions/4353525/floating-point-linear-interpolation#answer-23716956
		/// </remarks>
		public static float LerpPrecise(float value1, float value2, float amount)
		{
			return ((1 - amount) * value1) + (value2 * amount);
		}

		#endregion
	}
}
