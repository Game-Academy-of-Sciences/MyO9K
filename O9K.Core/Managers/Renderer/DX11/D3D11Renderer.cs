using System;
using System.ComponentModel.Composition;
using System.Drawing;
using Ensage;
using Ensage.SDK.Renderer;
using Ensage.SDK.Renderer.DX11;
using O9K.Core.Managers.Renderer.Metadata;
using SharpDX;
using SharpDX.Direct2D1;
using SharpDX.DirectWrite;
using SharpDX.Mathematics.Interop;

namespace O9K.Core.Managers.Renderer.DX11
{
	// Token: 0x0200003D RID: 61
	[ExportRenderer(RenderMode.Dx11)]
	public sealed class D3D11Renderer : IDisposable, IRenderer
	{
		// Token: 0x0600018F RID: 399 RVA: 0x000030ED File Offset: 0x000012ED
		[ImportingConstructor]
		public D3D11Renderer(ID3D11Context context, BrushCache brushCache, TextFormatCache textFormatCache, D3D11TextureManager9 textureManager)
		{
			this.context = context;
			this.brushCache = brushCache;
			this.textFormatCache = textFormatCache;
			this.textureManager = textureManager;
			context.Draw += this.OnDraw;
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000190 RID: 400 RVA: 0x00003124 File Offset: 0x00001324
		// (remove) Token: 0x06000191 RID: 401 RVA: 0x0000313D File Offset: 0x0000133D
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

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003156 File Offset: 0x00001356
		public ITextureManager TextureManager
		{
			get
			{
				return this.textureManager;
			}
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000315E File Offset: 0x0000135E
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000316D File Offset: 0x0000136D
		public void DrawCircle(Vector2 center, float radius, Color color, float width = 1f)
		{
			this.context.RenderTarget.DrawEllipse(new Ellipse(center, radius, radius), this.brushCache.GetOrCreate(color), width);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000141FC File Offset: 0x000123FC
		public void DrawFilledRectangle(RectangleF rect, Color color, Color borderColor, float borderWidth = 1f)
		{
			RenderTarget renderTarget = this.context.RenderTarget;
			SolidColorBrush orCreate = this.brushCache.GetOrCreate(color);
			if (borderWidth > 0f)
			{
				SolidColorBrush orCreate2 = this.brushCache.GetOrCreate(borderColor);
				int num = 1;
				while ((float)num < borderWidth + 1f)
				{
					renderTarget.DrawRectangle(new RawRectangleF(rect.Left - (float)num, rect.Top - (float)num, rect.Right + (float)num, rect.Bottom + (float)num), orCreate2);
					num++;
				}
			}
			renderTarget.FillRectangle(rect, orCreate);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0001428C File Offset: 0x0001248C
		public void DrawFilledRectangle(RectangleF rect, Color color)
		{
			RenderTarget renderTarget = this.context.RenderTarget;
			SolidColorBrush orCreate = this.brushCache.GetOrCreate(color);
			renderTarget.FillRectangle(rect, orCreate);
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000319A File Offset: 0x0000139A
		public void DrawLine(Vector2 start, Vector2 end, Color color, float width = 1f)
		{
			this.context.RenderTarget.DrawLine(start, end, this.brushCache.GetOrCreate(color), width);
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000031C6 File Offset: 0x000013C6
		public void DrawRectangle(RectangleF rect, Color color, float borderWidth = 1f)
		{
			this.context.RenderTarget.DrawRectangle(rect, this.brushCache.GetOrCreate(color), borderWidth);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000142C0 File Offset: 0x000124C0
		public void DrawText(Vector2 position, string text, Color color, float fontSize = 13f, string fontFamily = "Calibri")
		{
			TextFormat orCreate = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
			SolidColorBrush orCreate2 = this.brushCache.GetOrCreate(color);
			using (TextLayout textLayout = new TextLayout(this.context.DirectWrite, text, orCreate, float.MaxValue, float.MaxValue))
			{
				this.context.RenderTarget.DrawTextLayout(position, textLayout, orCreate2);
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0001433C File Offset: 0x0001253C
		public void DrawText(RectangleF position, string text, Color color, RendererFontFlags flags = RendererFontFlags.Left, float fontSize = 13f, string fontFamily = "Calibri")
		{
			TextFormat orCreate = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
			SolidColorBrush orCreate2 = this.brushCache.GetOrCreate(color);
			using (TextLayout textLayout = new TextLayout(this.context.DirectWrite, text, orCreate, position.Width, position.Height))
			{
				if ((flags & RendererFontFlags.Center) == RendererFontFlags.Center)
				{
					textLayout.TextAlignment = TextAlignment.Center;
				}
				else if ((flags & RendererFontFlags.Right) == RendererFontFlags.Right)
				{
					textLayout.TextAlignment = TextAlignment.Trailing;
				}
				if ((flags & RendererFontFlags.VerticalCenter) == RendererFontFlags.VerticalCenter)
				{
					position.Y += position.Height / 2f - orCreate.FontSize * 0.6f;
				}
				this.context.RenderTarget.DrawTextLayout(position.Location, textLayout, orCreate2);
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000031EB File Offset: 0x000013EB
		public void DrawTexture(string textureKey, Vector2 position, Vector2 size, float rotation = 0f, float opacity = 1f)
		{
			this.DrawTexture(textureKey, new RectangleF(position.X, position.Y, size.X, size.Y), rotation, opacity);
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00014410 File Offset: 0x00012610
		public void DrawTexture(string textureKey, RectangleF rect, float rotation = 0f, float opacity = 1f)
		{
			D3D11Texture texture = this.textureManager.GetTexture(textureKey);
			if (rotation == 0f)
			{
				this.context.RenderTarget.DrawBitmap(texture.Bitmap, rect, opacity, BitmapInterpolationMode.Linear);
				return;
			}
			RawMatrix3x2 transform = this.context.RenderTarget.Transform;
			Vector2 vector;
			vector..ctor(rect.Width / texture.Bitmap.Size.Width, rect.Height / texture.Bitmap.Size.Height);
			this.context.RenderTarget.Transform = Matrix3x2.Translation(-texture.Center) * Matrix3x2.Rotation(rotation) * Matrix3x2.Translation(texture.Center) * Matrix3x2.Scaling(vector) * Matrix3x2.Translation(rect.Location);
			this.context.RenderTarget.DrawBitmap(texture.Bitmap, opacity, BitmapInterpolationMode.Linear);
			this.context.RenderTarget.Transform = transform;
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00014520 File Offset: 0x00012720
		public Vector2 MeasureText(string text, float fontSize = 13f, string fontFamily = "Calibri")
		{
			TextFormat orCreate = this.textFormatCache.GetOrCreate(fontFamily, fontSize);
			Vector2 result;
			using (TextLayout textLayout = new TextLayout(this.context.DirectWrite, text, orCreate, float.MaxValue, float.MaxValue))
			{
				TextMetrics metrics = textLayout.Metrics;
				result = new Vector2(metrics.Width, metrics.Height);
			}
			return result;
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00014590 File Offset: 0x00012790
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
				this.textFormatCache.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00003215 File Offset: 0x00001415
		private void OnDraw(object sender, EventArgs e)
		{
			RendererManager9.EventHandler eventHandler = this.renderEventHandler;
			if (eventHandler == null)
			{
				return;
			}
			eventHandler(this);
		}

		// Token: 0x040000A9 RID: 169
		private readonly BrushCache brushCache;

		// Token: 0x040000AA RID: 170
		private readonly ID3D11Context context;

		// Token: 0x040000AB RID: 171
		private readonly TextFormatCache textFormatCache;

		// Token: 0x040000AC RID: 172
		private readonly D3D11TextureManager9 textureManager;

		// Token: 0x040000AD RID: 173
		private bool disposed;

		// Token: 0x040000AE RID: 174
		private RendererManager9.EventHandler renderEventHandler;
	}
}
