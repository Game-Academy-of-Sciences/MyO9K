using System;
using PlaySharp.Toolkit.Helper.Annotations;
using SharpDX;
using SharpDX.Direct2D1;

namespace O9K.Core.Managers.Renderer.DX11
{
	// Token: 0x0200003E RID: 62
	public sealed class D3D11Texture : D3Texture
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x000145E8 File Offset: 0x000127E8
		public D3D11Texture([NotNull] Bitmap bitmap, [CanBeNull] string file = null) : base(file)
		{
			this.Bitmap = bitmap;
			base.Size = new Vector2(this.Bitmap.Size.Width, this.Bitmap.Size.Height);
			base.Center = new Vector2(base.Size.X / 2f, base.Size.Y / 2f);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00003228 File Offset: 0x00001428
		[NotNull]
		public Bitmap Bitmap { get; }

		// Token: 0x060001A2 RID: 418 RVA: 0x00003230 File Offset: 0x00001430
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Bitmap.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x040000AF RID: 175
		private bool disposed;
	}
}
