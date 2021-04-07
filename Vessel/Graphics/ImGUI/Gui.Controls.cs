using ImGuiNET;

namespace Vessel
{
	public static partial class Gui
	{
		public static void Indent(float indent_w)
		{
			ImGui.Indent(indent_w);
		}

		public static void Indent()
		{
			ImGui.Indent();
		}

		public static void LabelText(string label, string fmt)
		{
			ImGui.LabelText(label, fmt);
		}

		public static void NewFrame()
		{
			ImGui.NewFrame();
		}

		public static void NewLine()
		{
			ImGui.NewLine();
		}

		public static void NextColumn()
		{
			ImGui.NextColumn();
		}

		public static void Spacing()
		{
			ImGui.Spacing();
		}

		public static void Text(string fmt)
		{
			ImGui.Text(fmt);
		}

		public static void TextColored(Color col, string fmt)
		{
			ImGui.TextColored(col.ToVector4(), fmt);
		}

		public static void TextDisabled(string fmt)
		{
			ImGui.TextDisabled(fmt);
		}

		public static void TextUnformatted(string text)
		{
			ImGui.TextUnformatted(text);
		}

		public static void TextWrapped(string fmt)
		{
			ImGui.TextWrapped(fmt);
		}

		public static void Unindent(float indent_w)
		{
			ImGui.Unindent(indent_w);
		}

		public static void Unindent()
		{
			ImGui.Unindent();
		}

	}
}
