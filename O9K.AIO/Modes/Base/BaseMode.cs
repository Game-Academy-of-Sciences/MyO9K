using System;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Menu;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Heroes;

namespace O9K.AIO.Modes.Base
{
	// Token: 0x0200002C RID: 44
	internal abstract class BaseMode : IDisposable
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00002AEA File Offset: 0x00000CEA
		protected BaseMode(BaseHero baseHero)
		{
			this.BaseHero = baseHero;
			this.Owner = baseHero.Owner;
			this.Menu = baseHero.Menu;
			this.TargetManager = baseHero.TargetManager;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00002B1D File Offset: 0x00000D1D
		protected BaseHero BaseHero { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00002B25 File Offset: 0x00000D25
		protected MenuManager Menu { get; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00002B2D File Offset: 0x00000D2D
		protected Owner Owner { get; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00002B35 File Offset: 0x00000D35
		protected TargetManager TargetManager { get; }

		// Token: 0x060000F9 RID: 249 RVA: 0x00002B3D File Offset: 0x00000D3D
		public virtual void Dispose()
		{
		}
	}
}
