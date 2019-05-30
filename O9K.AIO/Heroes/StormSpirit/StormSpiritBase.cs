using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.StormSpirit.Modes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.StormSpirit
{
	// Token: 0x02000093 RID: 147
	[HeroId(HeroId.npc_dota_hero_storm_spirit)]
	internal class StormSpiritBase : BaseHero
	{
		// Token: 0x060002E1 RID: 737 RVA: 0x00003CCC File Offset: 0x00001ECC
		public StormSpiritBase(IContext9 context) : base(context)
		{
			this.manaCalculatorMode = new ManaCalculatorMode(this, new ManaCalculatorModeMenu(base.Menu.RootMenu, "Mana calculator", null), context.Renderer);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00003CFD File Offset: 0x00001EFD
		public override void Dispose()
		{
			base.Dispose();
			this.manaCalculatorMode.Dispose();
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00003D10 File Offset: 0x00001F10
		protected override void DisableCustomModes()
		{
			this.manaCalculatorMode.Disable();
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00003D1D File Offset: 0x00001F1D
		protected override void EnableCustomModes()
		{
			this.manaCalculatorMode.Enable();
		}

		// Token: 0x04000196 RID: 406
		private readonly ManaCalculatorMode manaCalculatorMode;
	}
}
