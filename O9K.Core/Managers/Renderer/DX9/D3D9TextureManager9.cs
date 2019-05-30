using System;
using System.ComponentModel.Composition;
using System.Drawing;
using System.IO;
using Ensage;
using Ensage.SDK.Renderer.DX9;
using O9K.Core.Logger;
using O9K.Core.Managers.Renderer.VPK;
using SharpDX.Direct3D9;

namespace O9K.Core.Managers.Renderer.DX9
{
	// Token: 0x0200003C RID: 60
	[Export]
	public sealed class D3D9TextureManager9 : D3TextureManager<D3D9Texture>
	{
		// Token: 0x0600018C RID: 396 RVA: 0x000030D1 File Offset: 0x000012D1
		[ImportingConstructor]
		public D3D9TextureManager9(ID3D9Context renderContext, VpkBrowser9 vpkBrowser) : base(vpkBrowser)
		{
			this.renderContext = renderContext;
			base.LoadFromDota(AbilityId.invoker_empty1, false);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0001410C File Offset: 0x0001230C
		public override void LoadFromStream(string textureKey, Stream stream)
		{
			try
			{
				Bitmap bitmap = (Bitmap)Image.FromStream(stream);
				byte[] buffer = (byte[])new ImageConverter().ConvertTo(bitmap, typeof(byte[]));
				Texture texture = Texture.FromMemory(this.renderContext.Device, buffer, bitmap.Width, bitmap.Height, 0, Usage.None, Format.A1, Pool.Default, Filter.Default, Filter.Default, 0);
				base.TextureCache[textureKey] = new D3D9Texture(texture, bitmap, null);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, textureKey);
			}
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00014194 File Offset: 0x00012394
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				foreach (D3D9Texture d3D9Texture in base.TextureCache.Values)
				{
					if (d3D9Texture != null)
					{
						d3D9Texture.Dispose();
					}
				}
			}
			this.disposed = true;
		}

		// Token: 0x040000A7 RID: 167
		private readonly ID3D9Context renderContext;

		// Token: 0x040000A8 RID: 168
		private bool disposed;
	}
}
