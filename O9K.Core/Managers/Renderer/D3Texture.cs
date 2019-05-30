using System;
using PlaySharp.Toolkit.Helper.Annotations;
using SharpDX;

namespace O9K.Core.Managers.Renderer
{
	// Token: 0x0200002A RID: 42
	public abstract class D3Texture : IDisposable
	{
		// Token: 0x060000CB RID: 203 RVA: 0x0000293B File Offset: 0x00000B3B
		protected D3Texture(string file = null)
		{
			this.File = file;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000CC RID: 204 RVA: 0x0000294A File Offset: 0x00000B4A
		// (set) Token: 0x060000CD RID: 205 RVA: 0x00002952 File Offset: 0x00000B52
		public Vector2 Center { get; protected set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000295B File Offset: 0x00000B5B
		// (set) Token: 0x060000CF RID: 207 RVA: 0x00002963 File Offset: 0x00000B63
		[CanBeNull]
		public string File { get; internal set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000296C File Offset: 0x00000B6C
		// (set) Token: 0x060000D1 RID: 209 RVA: 0x00002974 File Offset: 0x00000B74
		public Vector2 Size { get; protected set; }

		// Token: 0x060000D2 RID: 210 RVA: 0x0000297D File Offset: 0x00000B7D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D3 RID: 211
		protected abstract void Dispose(bool disposing);
	}
}
