using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Earthshaker.Modes;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Earthshaker
{
	// Token: 0x02000140 RID: 320
	[HeroId(HeroId.npc_dota_hero_earthshaker)]
	internal class EarthshakerBase : BaseHero
	{
		// Token: 0x0600065E RID: 1630 RVA: 0x00002E76 File Offset: 0x00001076
		public EarthshakerBase(IContext9 context) : base(context)
		{
		}

		// Token: 0x0600065F RID: 1631 RVA: 0x0001EEF0 File Offset: 0x0001D0F0
		protected override void CreateComboMenus()
		{
			base.ComboMenus.Add(new EarthshakerComboModeMenu(base.Menu.RootMenu, "Combo"));
			base.ComboMenus.Add(new EarthshakerComboModeMenu(base.Menu.RootMenu, "Alternative combo"));
			base.ComboMenus.Add(new EarthshakerEchoSlamComboModeMenu(base.Menu.RootMenu, "Echo slam combo"));
		}
	}
}
