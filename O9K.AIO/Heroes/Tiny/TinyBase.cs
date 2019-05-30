using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Tiny.Modes;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Tiny
{
	// Token: 0x0200006C RID: 108
	[HeroId(HeroId.npc_dota_hero_tiny)]
	internal class TinyBase : BaseHero
	{
		// Token: 0x0600023F RID: 575 RVA: 0x000036D9 File Offset: 0x000018D9
		public TinyBase(IContext9 context) : base(context)
		{
			this.tossToTower = new TossUnderTowerMode(this, new KeyPressModeMenu(base.Menu.RootMenu, "Toss to tower", "Toss enemy to ally tower"));
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00003708 File Offset: 0x00001908
		public override void Dispose()
		{
			base.Dispose();
			this.tossToTower.Dispose();
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000371B File Offset: 0x0000191B
		protected override void DisableCustomModes()
		{
			this.tossToTower.Disable();
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00003728 File Offset: 0x00001928
		protected override void EnableCustomModes()
		{
			this.tossToTower.Enable();
		}

		// Token: 0x04000137 RID: 311
		private readonly TossUnderTowerMode tossToTower;
	}
}
