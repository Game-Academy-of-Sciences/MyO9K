using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using Ensage;
using Ensage.SDK.Extensions;
using Ensage.SDK.Renderer;
using Ensage.SDK.Renderer.DX9;
using O9K.Core.Managers.Renderer.Metadata;
using SharpDX;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;

namespace O9K.Core.Managers.Renderer.DX9
{
	// Token: 0x0200003A RID: 58
	[ExportRenderer(RenderMode.Dx9)]
	public sealed class D3D9Renderer : IDisposable, IRenderer
	{
		// Token: 0x06000175 RID: 373 RVA: 0x000138D8 File Offset: 0x00011AD8
		[ImportingConstructor]
		public D3D9Renderer(ID3D9Context context, FontCache fontCache, D3D9TextureManager9 textureManager)
		{
			this.context = context;
			this.fontCache = fontCache;
			this.textureManager = textureManager;
			this.line = new Line(this.context.Device);
			this.sprite = new Sprite(this.context.Device);
			context.PreReset += this.OnPreReset;
			context.PostReset += this.OnPostReset;
			context.Draw += this.OnDraw;
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000176 RID: 374 RVA: 0x00002FE0 File Offset: 0x000011E0
		// (remove) Token: 0x06000177 RID: 375 RVA: 0x00002FF9 File Offset: 0x000011F9
		public event RendererManager9.EventHandler Draw
		{
			add
			{
				this.renderEventHandler = (RendererManager9.EventHandler)Delegate.Combine(this.renderEventHandler, value);
			}
			remove
			{
				this.renderEventHandler = (RendererManager9.EventHandler)Delegate.Remove(this.renderEventHandler, value);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00003012 File Offset: 0x00001212
		public ITextureManager TextureManager
		{
			get
			{
				return this.textureManager;
			}
		}

		// Token: 0x06000179 RID: 377 RVA: 0x0000301A File Offset: 0x0000121A
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00013964 File Offset: 0x00011B64
		public void DrawCircle(Vector2 center, float radius, Color color, float width = 1f)
		{
			float num = 120f;
			float num2 = radius / (float)Math.Cos(6.2831853071795862 / (double)num);
			List<Vector2> list = new List<Vector2>();
			ColorBGRA colorBGRA;
			colorBGRA..ctor(color.R, color.G, color.B, color.A);
			int num3 = 1;
			while ((float)num3 <= num)
			{
				double num4 = (double)(num3 * 2) * 3.1415926535897931 / (double)num;
				Vector2 item;
				item..ctor(center.X + num2 * (float)Math.Cos(num4), center.Y + num2 * (float)Math.Sin(num4));
				list.Add(item);
				num3++;
			}
			this.line.Width = width;
			this.line.Begin();
			try
			{
				for (int i = 0; i <= list.Count - 1; i++)
				{
					int index = (list.Count - 1 == i) ? 0 : (i + 1);
					this.line.Draw(new RawVector2[]
					{
						list[i],
						list[index]
					}, colorBGRA);
				}
			}
			finally
			{
				this.line.End();
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00013AAC File Offset: 0x00011CAC
		public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1f)
		{
			this.DrawFilledRectangle(new RectangleF(rect.X - borderWidth, rect.Y - borderWidth, rect.Width + borderWidth * 2f, rect.Height + borderWidth * 2f), borderColor);
			this.DrawFilledRectangle(rect, color);
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00013B04 File Offset: 0x00011D04
		public void DrawFilledRectangle(RectangleF rect, Color color)
		{
			ColorBGRA colorBGRA;
			colorBGRA..ctor(color.R, color.G, color.B, color.A);
			Vector2 vector;
			vector..ctor(0f, rect.Height / 2f);
			this.line.Width = rect.Height;
			this.line.Begin();
			try
			{
				this.line.Draw(new RawVector2[]
				{
					rect.TopLeft + vector,
					rect.TopRight + vector
				}, colorBGRA);
			}
			finally
			{
				this.line.End();
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00013BD4 File Offset: 0x00011DD4
		public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1f)
		{
			this.line.Width = width;
			this.line.Begin();
			try
			{
				this.line.Draw(new RawVector2[]
				{
					start,
					end
				}, new ColorBGRA(color.R, color.G, color.B, color.A));
			}
			finally
			{
				this.line.End();
			}
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00013C68 File Offset: 0x00011E68
		public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1f)
		{
			ColorBGRA colorBGRA;
			colorBGRA..ctor(color.R, color.G, color.B, color.A);
			this.line.Width = borderWidth;
			this.line.Begin();
			try
			{
				this.line.Draw(new RawVector2[]
				{
					rect.TopLeft,
					rect.TopRight
				}, colorBGRA);
				this.line.Draw(new RawVector2[]
				{
					rect.TopRight,
					rect.BottomRight
				}, colorBGRA);
				this.line.Draw(new RawVector2[]
				{
					rect.BottomRight,
					rect.BottomLeft
				}, colorBGRA);
				this.line.Draw(new RawVector2[]
				{
					rect.BottomLeft,
					rect.TopLeft
				}, colorBGRA);
			}
			finally
			{
				this.line.End();
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00013DC4 File Offset: 0x00011FC4
		public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
		{
			this.fontCache.GetOrCreate(fontFamily, fontSize).DrawText(null, text, (int)position.X, (int)position.Y, new ColorBGRA(color.R, color.G, color.B, color.A));
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00013E1C File Offset: 0x0001201C
		public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
		{
			this.fontCache.GetOrCreate(fontFamily, fontSize).DrawText(null, text, position, (FontDrawFlags)flags, new ColorBGRA(color.R, color.G, color.B, color.A));
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00003029 File Offset: 0x00001229
		public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0f, float opacity = 1f)
		{
			this.DrawTexture(textureKey, new RectangleF(position.X, position.Y, size.X, size.Y), rotation, opacity);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00013E70 File Offset: 0x00012070
		public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0f, float opacity = 1f)
		{
			D3D9Texture texture = this.textureManager.GetTexture(textureKey);
			this.sprite.Begin(SpriteFlags.AlphaBlend);
			RawMatrix transform = this.sprite.Transform;
			Vector2 vector;
			vector..ctor(rect.Width / (float)texture.Bitmap.Width, rect.Height / (float)texture.Bitmap.Height);
			if (rotation == 0f)
			{
				this.sprite.Transform = Matrix.Scaling(vector.X, vector.Y, 0f) * Matrix.Translation(rect.X, rect.Y, 0f);
				this.sprite.Draw(texture.Texture, Color.White);
			}
			else
			{
				Vector3 vector2 = texture.Center.ToVector3(0f);
				this.sprite.Transform = Matrix.Translation(-vector2) * Matrix.RotationZ(rotation) * Matrix.Translation(vector2) * Matrix.Scaling(vector.X, vector.Y, 0f) * Matrix.Translation(rect.X, rect.Y, 0f);
				this.sprite.Draw(texture.Texture, Color.White);
			}
			this.sprite.Transform = transform;
			this.sprite.End();
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00013FEC File Offset: 0x000121EC
		public Vector2 MeasureText(string text, float fontSize = 13f, string fontFamily = "Calibri")
		{
			RawRectangle rawRectangle = this.fontCache.GetOrCreate(fontFamily, fontSize).MeasureText(null, text, FontDrawFlags.Left);
			return new Vector2((float)(rawRectangle.Right - rawRectangle.Left), (float)(rawRectangle.Bottom - rawRectangle.Top));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00014030 File Offset: 0x00012230
		private void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.context.Draw -= this.OnDraw;
				this.context.Dispose();
				this.textureManager.Dispose();
				this.fontCache.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00003053 File Offset: 0x00001253
		private void OnDraw(object sender, EventArgs e)
		{
			RendererManager9.EventHandler eventHandler = this.renderEventHandler;
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this);
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00003066 File Offset: 0x00001266
		private void OnPostReset(object sender, EventArgs eventArgs)
		{
			this.line.OnResetDevice();
			this.sprite.OnResetDevice();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000307E File Offset: 0x0000127E
		private void OnPreReset(object sender, EventArgs eventArgs)
		{
			this.line.OnLostDevice();
			this.sprite.OnLostDevice();
		}

		// Token: 0x0400009D RID: 157
		private readonly ID3D9Context context;

		// Token: 0x0400009E RID: 158
		private readonly FontCache fontCache;

		// Token: 0x0400009F RID: 159
		private readonly Line line;

		// Token: 0x040000A0 RID: 160
		private readonly Sprite sprite;

		// Token: 0x040000A1 RID: 161
		private readonly D3D9TextureManager9 textureManager;

		// Token: 0x040000A2 RID: 162
		private bool disposed;

		// Token: 0x040000A3 RID: 163
		private RendererManager9.EventHandler renderEventHandler;
	}
}
