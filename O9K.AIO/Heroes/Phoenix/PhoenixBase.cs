using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Phoenix.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Phoenix
{
	// Token: 0x020000D4 RID: 212
	[HeroId(HeroId.npc_dota_hero_phoenix)]
	internal class PhoenixBase : BaseHero
	{
		// Token: 0x0600044E RID: 1102 RVA: 0x00004471 File Offset: 0x00002671
		public PhoenixBase(IContext9 context) : base(context)
		{
			this.sunRayAllyMode = new SunRayAllyMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Sun ray ally", null));
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x0000449C File Offset: 0x0000269C
		public override void Dispose()
		{
			base.Dispose();
			this.sunRayAllyMode.Dispose();
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000044AF File Offset: 0x000026AF
		protected override void DisableCustomModes()
		{
			this.sunRayAllyMode.Disable();
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x000044BC File Offset: 0x000026BC
		protected override void EnableCustomModes()
		{
			this.sunRayAllyMode.Enable();
		}

		// Token: 0x04000264 RID: 612
		private readonly SunRayAllyMode sunRayAllyMode;
	}
}
