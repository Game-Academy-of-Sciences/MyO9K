using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Kunkka.Modes;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Kunkka
{
	// Token: 0x02000128 RID: 296
	[HeroId(HeroId.npc_dota_hero_kunkka)]
	internal class KunkkaBase : BaseHero
	{
		// Token: 0x060005E5 RID: 1509 RVA: 0x000050E1 File Offset: 0x000032E1
		public KunkkaBase(IContext9 context) : base(context)
		{
			this.autoReturnMode = new AutoReturnMode(this, new PermanentModeMenu(base.Menu.RootMenu, "Auto return", "Auto use \"X return\""));
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00005110 File Offset: 0x00003310
		public override void Dispose()
		{
			base.Dispose();
			this.autoReturnMode.Dispose();
		}

		// Token: 0x060005E7 RID: 1511 RVA: 0x00005123 File Offset: 0x00003323
		protected override void DisableCustomModes()
		{
			this.autoReturnMode.Disable();
		}

		// Token: 0x060005E8 RID: 1512 RVA: 0x00005130 File Offset: 0x00003330
		protected override void EnableCustomModes()
		{
			this.autoReturnMode.Enable();
		}

		// Token: 0x04000342 RID: 834
		private readonly AutoReturnMode autoReturnMode;
	}
}
