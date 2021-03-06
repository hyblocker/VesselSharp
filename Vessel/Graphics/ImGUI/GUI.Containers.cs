﻿using ImGuiNET;
using System.Numerics;

namespace Vessel
{
	// An immediate GUI class.
	// Essentially is a copy paste implementation of ImGuiNET.ImGui, with a decent number of changes since the code is autogenerated
	// And is not easy to use from within a managed context without manipulating pointers.
	// The first section of this class is a lot of wrapper functions around ImGuiNET that are designed to make life easier or to write a proper .NET
	//			binding for the function. Some functions expect a float* for instance as an array, and the autogenerated code expects a float, and not
	//			a float array
	// The second section of this class is a reimplementation of ImGuiNET

	public static partial class Gui
	{
		#region Containers
		
		#region Windows

		/// <summary>
		/// Constructs an ImGUI window
		/// </summary>
		/// <param name="name">The name of the window</param>
		/// <returns>Whether the window is open or not</returns>
		public static bool Begin(string name)
		{
			return ImGui.Begin(name);
		}
		
		public static bool Begin(string name, ImGuiWindowFlags flags)
		{
			return ImGui.Begin(name, flags);
		}

		public static bool Begin(string name, ref bool p_open)
		{
			return ImGui.Begin(name, ref p_open);
		}

		public static bool Begin(string name, ref bool p_open, ImGuiWindowFlags flags)
		{
			return ImGui.Begin(name, ref p_open, flags);
		}

		public static void End()
		{
			ImGui.End();
		}

		#endregion

		#region Children

		public static bool BeginChild(string str_id)
		{
			return ImGui.BeginChild(str_id);
		}

		public static bool BeginChild(string str_id, Vector2 size)
		{
			return ImGui.BeginChild(str_id, size);
		}

		public static bool BeginChild(string str_id, Vector2 size, bool border)
		{
			return ImGui.BeginChild(str_id, size, border);
		}

		public static bool BeginChild(string str_id, Vector2 size, bool border, ImGuiWindowFlags flags)
		{
			return ImGui.BeginChild(str_id, size, border, flags);
		}

		public static bool BeginChild(uint id)
		{
			return ImGui.BeginChild(id);
		}

		public static bool BeginChild(uint id, Vector2 size)
		{
			return ImGui.BeginChild(id, size);
		}

		public static bool BeginChild(uint id, Vector2 size, bool border)
		{
			return ImGui.BeginChild(id, size, border);
		}

		public static bool BeginChild(uint id, Vector2 size, bool border, ImGuiWindowFlags flags)
		{
			return ImGui.BeginChild(id, size, border, flags);
		}

		#endregion

		#region Child Frames

		public static bool BeginChildFrame(uint id, Vector2 size)
		{
			return ImGui.BeginChildFrame(id, size);
		}

		public static bool BeginChildFrame(uint id, Vector2 size, ImGuiWindowFlags flags)
		{
			return ImGui.BeginChildFrame(id, size, flags);
		}

		#endregion

		#region Combos

		public static bool BeginCombo(string label, string preview_value)
		{
			return ImGui.BeginCombo(label, preview_value);
		}

		public static bool BeginCombo(string label, string preview_value, ImGuiComboFlags flags)
		{
			return ImGui.BeginCombo(label, preview_value, flags);
		}

		#endregion

		#region Drag Drop

		public static bool BeginDragDropSource()
		{
			return ImGui.BeginDragDropSource();
		}

		public static bool BeginDragDropSource(ImGuiDragDropFlags flags)
		{
			return ImGui.BeginDragDropSource(flags);
		}

		public static bool BeginDragDropTarget()
		{
			return ImGui.BeginDragDropTarget();
		}

		#endregion

		#region Menus

		public static bool BeginMainMenuBar()
		{
			return ImGui.BeginMainMenuBar();
		}

		public static bool BeginMenu(string label)
		{
			return ImGui.BeginMenu(label);
		}

		public static bool BeginMenu(string label, bool enabled)
		{
			return ImGui.BeginMenu(label, enabled);
		}

		public static bool BeginMenuBar()
		{
			return ImGui.BeginMenuBar();
		}

		#endregion

		#region Popup

		public static bool BeginPopup(string str_id)
		{
			return ImGui.BeginPopup(str_id);
		}

		public static bool BeginPopup(string str_id, ImGuiWindowFlags flags)
		{
			return ImGui.BeginPopup(str_id, flags);
		}


		public static bool BeginPopupModal(string name, ref bool p_open, ImGuiWindowFlags flags)
		{
			return ImGui.BeginPopupModal(name, ref p_open, flags);
		}

		public static bool BeginPopupModal(string name)
		{
			return ImGui.BeginPopupModal(name);
		}

		public static bool BeginPopupModal(string name, ref bool p_open)
		{
			return ImGui.BeginPopupModal(name, ref p_open);
		}

		#endregion

		#region Popup Context

		public static bool BeginPopupContextItem()
		{
			return ImGui.BeginPopupContextItem();
		}

		public static bool BeginPopupContextItem(string str_id)
		{
			return ImGui.BeginPopupContextItem(str_id);
		}

		public static bool BeginPopupContextItem(string str_id, ImGuiPopupFlags popup_flags)
		{
			return ImGui.BeginPopupContextItem(str_id, popup_flags);
		}

		public static bool BeginPopupContextVoid()
		{
			return ImGui.BeginPopupContextVoid();
		}

		public static bool BeginPopupContextVoid(string str_id)
		{
			return ImGui.BeginPopupContextVoid(str_id);
		}

		public static bool BeginPopupContextVoid(string str_id, ImGuiPopupFlags popup_flags)
		{
			return ImGui.BeginPopupContextVoid(str_id, popup_flags);
		}

		public static bool BeginPopupContextWindow(string str_id)
		{
			return ImGui.BeginPopupContextWindow(str_id);
		}

		public static bool BeginPopupContextWindow()
		{
			return ImGui.BeginPopupContextWindow();
		}

		#endregion

		#region Tabs

		public static bool BeginTabBar(string str_id)
		{
			return ImGui.BeginTabBar(str_id);
		}

		public static bool BeginTabBar(string str_id, ImGuiTabBarFlags flags)
		{
			return ImGui.BeginTabBar(str_id, flags);
		}

		public static bool BeginTabItem(string label)
		{
			return ImGui.BeginTabItem(label);
		}

		public static bool BeginTabItem(string label, ref bool p_open)
		{
			return ImGui.BeginTabItem(label, ref p_open);
		}

		public static bool BeginTabItem(string label, ref bool p_open, ImGuiTabItemFlags flags)
		{
			return ImGui.BeginTabItem(label, ref p_open, flags);
		}

		#endregion

		#region Other

		public static void BeginTooltip()
		{
			ImGui.BeginTooltip();
		}

		public static void BeginGroup()
		{
			ImGui.BeginGroup();
		}

		#endregion

		#endregion
	}
}
