using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Magnus.Modes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Magnus
{
	// Token: 0x0200010A RID: 266
	[HeroId(HeroId.npc_dota_hero_magnataur)]
	internal class MagnusBase : BaseHero
	{
		// Token: 0x06000544 RID: 1348 RVA: 0x00004B93 File Offset: 0x00002D93
		public MagnusBase(IContext9 context) : base(context)
		{
			this.blinkSkewerMode = new BlinkSkewerMode(this, new BlinkSkewerModeMenu(base.Menu.RootMenu, "Blink+Skewer combo"));
		}

		// Token: 0x06000545 RID: 1349 RVA: 0x00004BBD File Offset: 0x00002DBD
		public override void Dispose()
		{
			base.Dispose();
			this.blinkSkewerMode.Dispose();
		}

		// Token: 0x06000546 RID: 1350 RVA: 0x00004BD0 File Offset: 0x00002DD0
		protected override void DisableCustomModes()
		{
			this.blinkSkewerMode.Disable();
		}

		// Token: 0x06000547 RID: 1351 RVA: 0x00004BDD File Offset: 0x00002DDD
		protected override void EnableCustomModes()
		{
			this.blinkSkewerMode.Enable();
		}

		// Token: 0x040002EE RID: 750
		private readonly BlinkSkewerMode blinkSkewerMode;
	}
}
