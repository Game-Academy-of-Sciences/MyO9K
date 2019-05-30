using System;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Metadata;
using O9K.Core.Managers.Context;

namespace O9K.AIO.Heroes.Tusk
{
	// Token: 0x02000062 RID: 98
	[HeroId(HeroId.npc_dota_hero_tusk)]
	internal class TuskBase : BaseHero
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00002E76 File Offset: 0x00001076
		public TuskBase(IContext9 context) : base(context)
		{
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000035E0 File Offset: 0x000017E0
		protected override void CreateComboMenus()
		{
			base.CreateComboMenus();
			base.ComboMenus.Add(new ComboModeMenu(base.Menu.RootMenu, "Kick to ally combo"));
		}
	}
}
