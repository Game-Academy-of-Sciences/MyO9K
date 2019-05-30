using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Renderer;
using Ensage.SDK.Renderer.Metadata;
using SharpDX;

namespace O9K.Core.Managers.Renderer
{
	// Token: 0x02000027 RID: 39
	[Export(typeof(IRendererManager9))]
	public sealed class RendererManager9 : IDisposable, IRenderer, IRendererManager9
	{
		// Token: 0x060000B4 RID: 180 RVA: 0x000027D6 File Offset: 0x000009D6
		[ImportingConstructor]
		public RendererManager9([ImportMany] IEnumerable<Lazy<IRenderer, IRendererMetadata>> renderers)
		{
			this.active = renderers.First((Lazy<IRenderer, IRendererMetadata> e) => e.Metadata.Mode == Drawing.RenderMode).Value;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060000B5 RID: 181 RVA: 0x0000280E File Offset: 0x00000A0E
		// (remove) Token: 0x060000B6 RID: 182 RVA: 0x0000281C File Offset: 0x00000A1C
		public event RendererManager9.EventHandler Draw
		{
			add
			{
				this.active.Draw += value;
			}
			remove
			{
				this.active.Draw -= value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x0000282A File Offset: 0x00000A2A
		public ITextureManager TextureManager
		{
			get
			{
				return this.active.TextureManager;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00002837 File Offset: 0x00000A37
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00002846 File Offset: 0x00000A46
		public void DrawCircle(Vector2 center, float radius, Color color, float width = 1f)
		{
			this.active.DrawCircle(center, radius, color, width);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00002858 File Offset: 0x00000A58
		public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1f)
		{
			this.active.DrawFilledRectangle(rect, color, borderColor, borderWidth);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x0000286A File Offset: 0x00000A6A
		public void DrawFilledRectangle(RectangleF rect, Color color)
		{
			this.active.DrawFilledRectangle(rect, color);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00002879 File Offset: 0x00000A79
		public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1f)
		{
			this.active.DrawLine(start, end, color, width);
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000288B File Offset: 0x00000A8B
		public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1f)
		{
			this.active.DrawRectangle(rect, color, borderWidth);
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000289B File Offset: 0x00000A9B
		public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
		{
			this.active.DrawText(position, text, color, fontSize, fontFamily);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000028AF File Offset: 0x00000AAF
		public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
		{
			this.active.DrawText(position, text, color, flags, fontSize, fontFamily);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000028C5 File Offset: 0x00000AC5
		public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0f, float opacity = 1f)
		{
			this.active.DrawTexture(textureKey, position, size, rotation, opacity);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x000028D9 File Offset: 0x00000AD9
		public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0f, float opacity = 1f)
		{
			this.active.DrawTexture(textureKey, rect, rotation, opacity);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000028EB File Offset: 0x00000AEB
		public Vector2 MeasureText(string text, float fontSize = 13f, string fontFamily = "Calibri")
		{
			return this.active.MeasureText(text, fontSize, fontFamily);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000028FB File Offset: 0x00000AFB
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.active.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x04000058 RID: 88
		private readonly IRenderer active;

		// Token: 0x04000059 RID: 89
		private bool disposed;

		// Token: 0x02000028 RID: 40
		// (Invoke) Token: 0x060000C5 RID: 197
		public delegate void EventHandler(IRenderer renderer);
	}
}
