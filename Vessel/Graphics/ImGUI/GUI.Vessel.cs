using System.Numerics;
using ImGuiNET;
using Vessel.Debug;

namespace Vessel
{
	// Window helpers for debugging scenes using Vessel.
	// Contains debug windows such as Scene view and a Property editor

	/// <summary>
	/// An immediate mode GUI class.
	/// </summary>
	public static partial class Gui
	{
		private static bool bufferBool = false;

		public static void ShowSceneHierarchyWindow()
		{
			// TODO: Implement lmao
		}

		/// <summary>
		/// Draws a utility window for interacting with RenderDoc
		/// </summary>
		public static void ShowRenderDocWindow()
		{
			if (RenderDoc.IsLoaded)
			{
				ImGui.SetNextWindowSize(new Vector2(264, 487)); 
			}
			else
			{
				ImGui.SetNextWindowSize(new Vector2(264, 80)); 
			}
			if (ImGui.Begin("RenderDoc"))
			{
				if (!RenderDoc.IsLoaded)
				{
					if (ImGui.Button("Load RenderDoc"))
					{
						RenderDoc.Initialise();
					}
				}
				else
				{
					if (ImGui.Button("Start Replay UI"))
					{
						RenderDoc.LaunchReplayUI();
					}

					if (ImGui.Button("Capture Frame"))
					{
						RenderDoc.TriggerCapture();
					}

					ImGui.Text("Debug");
					ImGui.BeginChild("Debug", new Vector2(0, 82), true);

					bufferBool = RenderDoc.APIValidation;
					ImGui.Checkbox("API Validation", ref bufferBool);
					RenderDoc.APIValidation = bufferBool;
					
					bufferBool = RenderDoc.VerifyBufferAccess;
					ImGui.Checkbox("Verify Buffer Access", ref bufferBool);
					RenderDoc.VerifyBufferAccess = bufferBool;
					
					bufferBool = RenderDoc.DebugOutputMute;
					ImGui.Checkbox("Debug Output Mute", ref bufferBool);
					RenderDoc.DebugOutputMute = bufferBool;
					
					ImGui.EndChild();
					
					ImGui.Text("Behavior");
					ImGui.BeginChild("Behavior", new Vector2(0, 160 + (RenderDoc.CaptureCallstacks ? 26 : 0)), true);

					bufferBool = RenderDoc.AllowFullscreen;
					ImGui.Checkbox("Allow Fullscreen", ref bufferBool);
					RenderDoc.AllowFullscreen = bufferBool;
					
					bufferBool = RenderDoc.AllowVSync;
					ImGui.Checkbox("Allow V-Sync", ref bufferBool);
					RenderDoc.AllowVSync = bufferBool;
					
					bufferBool = RenderDoc.RefAllResources;
					ImGui.Checkbox("Reference All Resources", ref bufferBool);
					RenderDoc.RefAllResources = bufferBool;
					
					bufferBool = RenderDoc.CaptureAllCmdLists;
					ImGui.Checkbox("Capture All Command Lists", ref bufferBool);
					RenderDoc.CaptureAllCmdLists = bufferBool;
					
					bufferBool = RenderDoc.CaptureCallstacks;
					ImGui.Checkbox("Capture Callstacks", ref bufferBool);
					RenderDoc.CaptureCallstacks = bufferBool;

					if (RenderDoc.CaptureCallstacks)
					{
						bufferBool = RenderDoc.CaptureCallstacksOnlyDraws;
						ImGui.Checkbox("Capture Callstacks (Only Draws)", ref bufferBool);
						RenderDoc.CaptureCallstacksOnlyDraws = bufferBool;
					}

					bufferBool = RenderDoc.HookIntoChildren;
					ImGui.Checkbox("Hook Into Child Processes", ref bufferBool);
					RenderDoc.HookIntoChildren = bufferBool;

					ImGui.EndChild();

					ImGui.Text("Overlay");
					ImGui.BeginChild("Overlay", new Vector2(0, 26 + (RenderDoc.OverlayEnabled ? 26 * 3 : 10)), true);

					bufferBool = RenderDoc.OverlayEnabled;
					ImGui.Checkbox("Enable Overlay", ref bufferBool);
					RenderDoc.OverlayEnabled = bufferBool;

					if (RenderDoc.OverlayEnabled)
					{
						bufferBool = RenderDoc.OverlayFrameRate;
						ImGui.Checkbox("Overlay Framerate", ref bufferBool);
						RenderDoc.OverlayFrameRate = bufferBool;

						bufferBool = RenderDoc.OverlayFrameNumber;
						ImGui.Checkbox("Overlay Frame Number", ref bufferBool);
						RenderDoc.OverlayFrameNumber = bufferBool;

						bufferBool = RenderDoc.OverlayCaptureList;
						ImGui.Checkbox("Overlay Capture List", ref bufferBool);
						RenderDoc.OverlayCaptureList = bufferBool;
					}

					ImGui.EndChild();
				}
				ImGui.End();
			}
		}
	}
}
