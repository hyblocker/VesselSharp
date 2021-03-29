using ImGuiNET;
using System;
using System.Numerics;

namespace Vessel
{
	// Drag shit

	/// <summary>
	/// An immediate mode GUI class.
	/// </summary>;
	public static partial class Gui
	{
		#region Floats

		public static bool DragFloat(string label, ref float v, float v_speed, float v_min, float v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragFloat(label, ref v, v_speed, v_min, v_max, format, flags);
		}

		public static bool DragFloat(string label, ref float v, float v_speed, float v_min, float v_max, string format)
		{
			return ImGui.DragFloat(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragFloat(string label, ref float v, float v_speed, float v_min, float v_max)
		{
			return ImGui.DragFloat(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragFloat(string label, ref float v, float v_speed)
		{
			return ImGui.DragFloat(label, ref v, v_speed);
		}

		public static bool DragFloat(string label, ref float v, float v_speed, float v_min)
		{
			return ImGui.DragFloat(label, ref v, v_speed, v_min);
		}

		public static bool DragFloat(string label, ref float v)
		{
			return ImGui.DragFloat(label, ref v);
		}


		#endregion

		#region 2D Floats

		public static bool DragFloat2(string label, ref Vector2 v)
		{
			return ImGui.DragFloat2(label, ref v);
		}

		public static bool DragFloat2(string label, ref Vector2 v, float v_speed)
		{
			return ImGui.DragFloat2(label, ref v, v_speed);
		}

		public static bool DragFloat2(string label, ref Vector2 v, float v_speed, float v_min)
		{
			return ImGui.DragFloat2(label, ref v, v_speed, v_min);
		}

		public static bool DragFloat2(string label, ref Vector2 v, float v_speed, float v_min, float v_max)
		{
			return ImGui.DragFloat2(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragFloat2(string label, ref Vector2 v, float v_speed, float v_min, float v_max, string format)
		{
			return ImGui.DragFloat2(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragFloat2(string label, ref Vector2 v, float v_speed, float v_min, float v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragFloat2(label, ref v, v_speed, v_min, v_max, format, flags);
		}

		#endregion

		#region 3D Floats

		public static bool DragFloat3(string label, ref Vector3 v, float v_speed)
		{
			return ImGui.DragFloat3(label, ref v, v_speed);
		}

		public static bool DragFloat3(string label, ref Vector3 v, float v_speed, float v_min)
		{
			return ImGui.DragFloat3(label, ref v, v_speed, v_min);
		}

		public static bool DragFloat3(string label, ref Vector3 v, float v_speed, float v_min, float v_max)
		{
			return ImGui.DragFloat3(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragFloat3(string label, ref Vector3 v, float v_speed, float v_min, float v_max, string format)
		{
			return ImGui.DragFloat3(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragFloat3(string label, ref Vector3 v, float v_speed, float v_min, float v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragFloat3(label, ref v, v_speed, v_min, v_max, format, flags);
		}

		public static bool DragFloat3(string label, ref Vector3 v)
		{
			return ImGui.DragFloat3(label, ref v);
		}

		#endregion

		#region 4D Floats

		public static bool DragFloat4(string label, ref Vector4 v, float v_speed, float v_min, float v_max, string format)
		{
			return ImGui.DragFloat4(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragFloat4(string label, ref Vector4 v, float v_speed, float v_min, float v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragFloat4(label, ref v, v_speed, v_min, v_max, format, flags);
		}

		public static bool DragFloat4(string label, ref Vector4 v, float v_speed, float v_min, float v_max)
		{
			return ImGui.DragFloat4(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragFloat4(string label, ref Vector4 v, float v_speed)
		{
			return ImGui.DragFloat4(label, ref v, v_speed);
		}

		public static bool DragFloat4(string label, ref Vector4 v)
		{
			return ImGui.DragFloat4(label, ref v);
		}

		public static bool DragFloat4(string label, ref Vector4 v, float v_speed, float v_min)
		{
			return ImGui.DragFloat4(label, ref v, v_speed, v_min);
		}

		#endregion

		#region Float Range 2

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed, float v_min)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed, float v_min, float v_max)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed, float v_min, float v_max, string format)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed, float v_min, float v_max, string format, string format_max)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format, format_max);
		}

		public static bool DragFloatRange2(string label, ref float v_current_min, ref float v_current_max, float v_speed, float v_min, float v_max, string format, string format_max, ImGuiSliderFlags flags)
		{
			return ImGui.DragFloatRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format, format_max, flags);
		}

		#endregion

		#region Integers

		public static bool DragInt(string label, ref int v)
		{
			return ImGui.DragInt(label, ref v);
		}

		public static bool DragInt(string label, ref int v, float v_speed)
		{
			return ImGui.DragInt(label, ref v, v_speed);
		}

		public static bool DragInt(string label, ref int v, float v_speed, int v_min)
		{
			return ImGui.DragInt(label, ref v, v_speed, v_min);
		}

		public static bool DragInt(string label, ref int v, float v_speed, int v_min, int v_max)
		{
			return ImGui.DragInt(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragInt(string label, ref int v, float v_speed, int v_min, int v_max, string format)
		{
			return ImGui.DragInt(label, ref v, v_speed, v_min, v_max, format);
		}

		#endregion

		#region 2D Integers

		public static bool DragInt2(string label, ref int v, float v_speed, int v_min, int v_max)
		{
			return ImGui.DragInt2(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragInt2(string label, ref int v, float v_speed, int v_min, int v_max, string format)
		{
			return ImGui.DragInt2(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragInt2(string label, ref int v, float v_speed)
		{
			return ImGui.DragInt2(label, ref v, v_speed);
		}

		public static bool DragInt2(string label, ref int v)
		{
			return ImGui.DragInt2(label, ref v);
		}

		public static bool DragInt2(string label, ref int v, float v_speed, int v_min)
		{
			return ImGui.DragInt2(label, ref v, v_speed, v_min);
		}

		#endregion

		#region 3D Integers

		public static bool DragInt3(string label, ref int v)
		{
			return ImGui.DragInt3(label, ref v);
		}

		public static bool DragInt3(string label, ref int v, float v_speed)
		{
			return ImGui.DragInt3(label, ref v, v_speed);
		}

		public static bool DragInt3(string label, ref int v, float v_speed, int v_min)
		{
			return ImGui.DragInt3(label, ref v, v_speed, v_min);
		}

		public static bool DragInt3(string label, ref int v, float v_speed, int v_min, int v_max)
		{
			return ImGui.DragInt3(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragInt3(string label, ref int v, float v_speed, int v_min, int v_max, string format)
		{
			return ImGui.DragInt3(label, ref v, v_speed, v_min, v_max, format);
		}

		#endregion

		#region 4D Integers

		public static bool DragInt4(string label, ref int v, float v_speed, int v_min, int v_max, string format)
		{
			return ImGui.DragInt4(label, ref v, v_speed, v_min, v_max, format);
		}

		public static bool DragInt4(string label, ref int v, float v_speed, int v_min, int v_max)
		{
			return ImGui.DragInt4(label, ref v, v_speed, v_min, v_max);
		}

		public static bool DragInt4(string label, ref int v, float v_speed, int v_min)
		{
			return ImGui.DragInt4(label, ref v, v_speed, v_min);
		}

		public static bool DragInt4(string label, ref int v)
		{
			return ImGui.DragInt4(label, ref v);
		}

		public static bool DragInt4(string label, ref int v, float v_speed)
		{
			return ImGui.DragInt4(label, ref v, v_speed);
		}

		#endregion

		#region Int Range 2

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max);
		}

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed);
		}

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed, int v_min)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min);
		}

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed, int v_min, int v_max)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max);
		}

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed, int v_min, int v_max, string format)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format);
		}

		public static bool DragIntRange2(string label, ref int v_current_min, ref int v_current_max, float v_speed, int v_min, int v_max, string format, string format_max)
		{
			return ImGui.DragIntRange2(label, ref v_current_min, ref v_current_max, v_speed, v_min, v_max, format, format_max);
		}

		#endregion

		#region Scalar

		public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr v, float v_speed, IntPtr v_min, IntPtr v_max, string format)
		{
			return ImGui.DragScalar(label, data_type, v, v_speed, v_min, v_max, format);
		}

		public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr v, float v_speed, IntPtr v_min, IntPtr v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragScalar(label, data_type, v, v_speed, v_min, v_max, format, flags);
		}

		public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr v, float v_speed, IntPtr v_min)
		{
			return ImGui.DragScalar(label, data_type, v, v_speed, v_min);
		}

		public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr v, float v_speed, IntPtr v_min, IntPtr v_max)
		{
			return ImGui.DragScalar(label, data_type, v, v_speed, v_min, v_max);
		}

		public static bool DragScalar(string label, ImGuiDataType data_type, IntPtr v, float v_speed)
		{
			return ImGui.DragScalar(label, data_type, v, v_speed);
		}

		#endregion

		#region Scalar N

		public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr v, int components, float v_speed, IntPtr v_min, IntPtr v_max, string format, ImGuiSliderFlags flags)
		{
			return ImGui.DragScalarN(label, data_type, v, components, v_speed, v_min, v_max, format, flags);
		}

		public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr v, int components, float v_speed, IntPtr v_min, IntPtr v_max, string format)
		{
			return ImGui.DragScalarN(label, data_type, v, components, v_speed, v_min, v_max, format);
		}

		public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr v, int components, float v_speed)
		{
			return ImGui.DragScalarN(label, data_type, v, components, v_speed);
		}

		public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr v, int components, float v_speed, IntPtr v_min)
		{
			return ImGui.DragScalarN(label, data_type, v, components, v_speed, v_min);
		}

		public static bool DragScalarN(string label, ImGuiDataType data_type, IntPtr v, int components, float v_speed, IntPtr v_min, IntPtr v_max)
		{
			return ImGui.DragScalarN(label, data_type, v, components, v_speed, v_min, v_max);
		}

		#endregion

	}
}
