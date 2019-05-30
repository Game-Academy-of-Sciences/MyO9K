using System;
using System.Drawing;
using Ensage.SDK.Renderer;
using SharpDX;

namespace O9K.Core.Managers.Renderer
{
	// Token: 0x02000025 RID: 37
	public interface IRenderer : IDisposable
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A7 RID: 167
		// (remove) Token: 0x060000A8 RID: 168
		event RendererManager9.EventHandler Draw;

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000A9 RID: 169
		ITextureManager TextureManager { get; }

		// Token: 0x060000AA RID: 170
		void DrawCircle(Vector2 center, float radius, Color color, float width = 1f);

		// Token: 0x060000AB RID: 171
		void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1f);

		// Token: 0x060000AC RID: 172
		void DrawFilledRectangle(RectangleF rect, Color color);

		// Token: 0x060000AD RID: 173
		void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1f);

		// Token: 0x060000AE RID: 174
		void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1f);

		// Token: 0x060000AF RID: 175
		void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri");

		// Token: 0x060000B0 RID: 176
		void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri");

		// Token: 0x060000B1 RID: 177
		void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0f, float opacity = 1f);

		// Token: 0x060000B2 RID: 178
		void DrawTexture(string textureKey, RectangleF rect, float rotation = 0f, float opacity = 1f);

		// Token: 0x060000B3 RID: 179
		Vector2 MeasureText(string text, float fontSize = 13f, string fontFamily = "Calibri");
	}
}
