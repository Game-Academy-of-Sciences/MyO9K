using System;
using System.Drawing;
using O9K.Core.Managers.Renderer;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x0200009E RID: 158
	internal static class Drawer
	{
		// Token: 0x0600037F RID: 895 RVA: 0x0001C484 File Offset: 0x0001A684
		public static void DrawTextWithBackground(string text, float size, Vector2 position, IRenderer renderer)
		{
			Vector2 vector = renderer.MeasureText(text, size, "Calibri");
			position -= new Vector2(vector.X / 2f, 0f);
			Vector2 vector2 = position + new Vector2(0f, vector.Y / 2f);
			renderer.DrawLine(vector2 - new Vector2(2f, 0f), vector2 + new Vector2(vector.X + 2f, 0f), System.Drawing.Color.FromArgb(150, 25, 25, 25), vector.Y);
			renderer.DrawText(position, text, System.Drawing.Color.White, size, "Calibri");
		}
	}
}
