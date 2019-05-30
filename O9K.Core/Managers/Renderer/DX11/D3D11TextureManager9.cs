using System;
using System.ComponentModel.Composition;
using System.IO;
using Ensage;
using Ensage.SDK.Renderer.DX11;
using O9K.Core.Logger;
using O9K.Core.Managers.Renderer.VPK;
using SharpDX.Direct2D1;
using SharpDX.WIC;

namespace O9K.Core.Managers.Renderer.DX11
{
	// Token: 0x0200003F RID: 63
	[Export]
	public sealed class D3D11TextureManager9 : D3TextureManager<D3D11Texture>
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x00003250 File Offset: 0x00001450
		[ImportingConstructor]
		public D3D11TextureManager9(ID3D11Context renderContext, VpkBrowser9 vpkBrowser) : base(vpkBrowser)
		{
			base.ChangeBrightness = true;
			this.renderContext = renderContext;
			this.imagingFactory = new ImagingFactory();
			base.LoadFromDota(AbilityId.invoker_empty1, false);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0001465C File Offset: 0x0001285C
		public override void LoadFromStream(string textureKey, Stream stream)
		{
			try
			{
				using (BitmapDecoder bitmapDecoder = new BitmapDecoder(this.imagingFactory, stream, DecodeOptions.CacheOnDemand))
				{
					BitmapFrameDecode frame = bitmapDecoder.GetFrame(0);
					using (FormatConverter formatConverter = new FormatConverter(this.imagingFactory))
					{
						formatConverter.Initialize(frame, SharpDX.WIC.PixelFormat.Format32bppPRGBA);
						SharpDX.Direct2D1.Bitmap bitmap = SharpDX.Direct2D1.Bitmap.FromWicBitmap(this.renderContext.RenderTarget, formatConverter);
						base.TextureCache[textureKey] = new D3D11Texture(bitmap, null);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, textureKey);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00014708 File Offset: 0x00012908
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (D3D11Texture d3D11Texture in base.TextureCache.Values)
				{
					if (d3D11Texture != null)
					{
						d3D11Texture.Dispose();
					}
				}
				this.imagingFactory.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x040000B1 RID: 177
		private readonly ImagingFactory imagingFactory;

		// Token: 0x040000B2 RID: 178
		private readonly ID3D11Context renderContext;

		// Token: 0x040000B3 RID: 179
		private bool disposed;
	}
}
