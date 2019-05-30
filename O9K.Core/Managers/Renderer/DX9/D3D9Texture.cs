using System;
using System.Drawing;
using PlaySharp.Toolkit.Helper.Annotations;
using SharpDX;
using SharpDX.Direct3D9;

namespace O9K.Core.Managers.Renderer.DX9
{
	// Token: 0x0200003B RID: 59
	public sealed class D3D9Texture : D3Texture
	{
		// Token: 0x06000188 RID: 392 RVA: 0x00014088 File Offset: 0x00012288
		public D3D9Texture(Texture texture, Bitmap bitmap, string file = null) : base(file)
		{
			this.Bitmap = bitmap;
			this.Texture = texture;
			base.Size = new Vector2((float)this.Bitmap.Size.Width, (float)this.Bitmap.Size.Height);
			base.Center = new Vector2(base.Size.X / 2f, base.Size.Y / 2f);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003096 File Offset: 0x00001296
		public Bitmap Bitmap { get; }

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600018A RID: 394 RVA: 0x0000309E File Offset: 0x0000129E
		[NotNull]
		public Texture Texture { get; }

		// Token: 0x0600018B RID: 395 RVA: 0x000030A6 File Offset: 0x000012A6
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Texture.Dispose();
				this.Bitmap.Dispose();
			}
			this.disposed = true;
		}

		// Token: 0x040000A4 RID: 164
		private bool disposed;
	}
}
