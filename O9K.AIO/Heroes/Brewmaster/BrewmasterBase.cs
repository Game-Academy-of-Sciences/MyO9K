using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Brewmaster.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Brewmaster
{
	// Token: 0x020001D5 RID: 469
	[HeroId(HeroId.npc_dota_hero_brewmaster)]
	internal class BrewmasterBase : BaseHero
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00006B16 File Offset: 0x00004D16
		public BrewmasterBase(IContext9 context) : base(context)
		{
			this.cycloneMode = new CycloneMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Cyclone enemy", "Use storm's cyclone on the target"));
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x00006B45 File Offset: 0x00004D45
		public override void Dispose()
		{
			base.Dispose();
			this.cycloneMode.Dispose();
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00006B58 File Offset: 0x00004D58
		protected override void DisableCustomModes()
		{
			this.cycloneMode.Disable();
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x00006B65 File Offset: 0x00004D65
		protected override void EnableCustomModes()
		{
			this.cycloneMode.Enable();
		}

		// Token: 0x040004F6 RID: 1270
		private readonly CycloneMode cycloneMode;
	}
}
