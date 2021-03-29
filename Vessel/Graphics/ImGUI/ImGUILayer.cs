using ImGuiNET;
using System;
using Veldrid;
using Vessel.Graphics;

namespace Vessel
{
	/// <summary>
	/// A <see cref="RenderLayer"/> which allows you to use ImGUI in Vessel
	/// </summary>
	public class ImGUILayer : RenderLayer
	{
		/// <summary>
		/// The draw GUI event
		/// </summary>
		public static Action OnGUI;

		// The actual ImGUI renderer
		private ImGuiRenderer ImGuiRenderer;
		private GraphicsDevice Graphics;

		private ImGuiIOPtr io;

		/// <summary>
		/// Creates a new ImGUI Render Layer
		/// </summary>
		/// <param name="graphicsDevice">The graphics device ImGUI will draw with</param>
		public ImGUILayer(GraphicsDevice graphicsDevice) : base()
		{
			Graphics = graphicsDevice;

			// Create an ImGUI renderer
			ImGuiRenderer = new ImGuiRenderer(
				graphicsDevice.veldridGraphicsDevice,
				graphicsDevice.veldridGraphicsDevice.MainSwapchain.Framebuffer.OutputDescription,
				VesselEngine.Instance.Window.Width,
				VesselEngine.Instance.Window.Height);

			// Viewport support veldrid pls 🙏
			io = ImGui.GetIO();
			io.ConfigFlags |= ImGuiConfigFlags.ViewportsEnable;

			// Resizing callbacks
			VesselEngine.Instance.Window.Resized += () =>
			{
				ImGuiRenderer.WindowResized(
					VesselEngine.Instance.Window.Width,
					VesselEngine.Instance.Window.Height);
			};
		}

		/// <summary>
		/// Handles ImGUI
		/// </summary>
		public override void Draw()
		{
			Graphics.Debug.PushGroup("ImGUI");

			// ImGUI Loop
			ImGuiRenderer.Update(VesselEngine.Instance.DeltaTime, VesselEngine.Instance.Window.InputSnapshot);
			OnGUI?.Invoke();
			ImGuiRenderer.Render(Graphics.veldridGraphicsDevice, Graphics.veldridCommandList);

			// Update viewports
			if ((io.ConfigFlags & ImGuiConfigFlags.ViewportsEnable) != 0)
			{
				ImGui.UpdatePlatformWindows();
				ImGui.RenderPlatformWindowsDefault();
			}

			Graphics.Debug.PopGroup();
		}
	}
}
