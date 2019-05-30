using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Weaver.Modes;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Weaver
{
	// Token: 0x0200004F RID: 79
	[HeroId(HeroId.npc_dota_hero_weaver)]
	internal class WeaverBase : BaseHero
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000335B File Offset: 0x0000155B
		public WeaverBase(IContext9 context) : base(context)
		{
			this.healthTrackerMode = new HealthTrackerMode(this, new PermanentModeMenu(base.Menu.RootMenu, "Health tracker", null));
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00003386 File Offset: 0x00001586
		public override void Dispose()
		{
			base.Dispose();
			this.healthTrackerMode.Dispose();
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00003399 File Offset: 0x00001599
		protected override void DisableCustomModes()
		{
			this.healthTrackerMode.Disable();
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000033A6 File Offset: 0x000015A6
		protected override void EnableCustomModes()
		{
			this.healthTrackerMode.Enable();
		}

		// Token: 0x040000F2 RID: 242
		private readonly HealthTrackerMode healthTrackerMode;
	}
}
