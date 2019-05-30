using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.EarthSpirit.Modes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.EarthSpirit
{
	// Token: 0x02000188 RID: 392
	[HeroId(HeroId.npc_dota_hero_earth_spirit)]
	internal class EarthSpiritBase : BaseHero
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00005FD1 File Offset: 0x000041D1
		public EarthSpiritBase(IContext9 context) : base(context)
		{
			this.rollSmashMode = new RollSmashMode(this, new RollSmashModeMenu(base.Menu.RootMenu, "Roll+Smash combo"));
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x00005FFB File Offset: 0x000041FB
		public override void Dispose()
		{
			base.Dispose();
			this.rollSmashMode.Dispose();
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0000600E File Offset: 0x0000420E
		protected override void DisableCustomModes()
		{
			this.rollSmashMode.Disable();
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0000601B File Offset: 0x0000421B
		protected override void EnableCustomModes()
		{
			this.rollSmashMode.Enable();
		}

		// Token: 0x0400045F RID: 1119
		private readonly RollSmashMode rollSmashMode;
	}
}
