using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Text;
using Vessel;
using Vessel.Debug;

namespace VesselSandbox
{
	/// <summary>
	/// An example scene
	/// </summary>
	public class DummyScene : Scene
	{
		public override void Initialise()
		{
			base.Initialise();

			//Create an entity, attach a mesh and load a material onto it

			// Hook to ImGUI
			ImGUILayer.OnGUI += OnGUI;
		}

		public override void End()
		{
			base.End();
			// Unhook from ImGUI
			ImGUILayer.OnGUI -= OnGUI;
		}

		public void OnGUI()
		{
			ImGui.ShowDemoWindow();
			Gui.ShowRenderDocWindow();
			if (ImGui.Begin("Test Window"))
			{
				ImGui.Text("Hello");
				ImGui.End();
			}
		}
	}
}
