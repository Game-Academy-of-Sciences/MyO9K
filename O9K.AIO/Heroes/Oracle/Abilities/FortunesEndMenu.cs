using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Oracle.Abilities
{
	// Token: 0x0200004D RID: 77
	internal class FortunesEndMenu : UsableAbilityMenu
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x0000331A File Offset: 0x0000151A
		public FortunesEndMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.FullChannelTime = new MenuSwitcher("Full channel time", "fortunesEndFullTime" + simplifiedName, false, false);
			base.Menu.Add<MenuSwitcher>(this.FullChannelTime);
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003353 File Offset: 0x00001553
		public MenuSwitcher FullChannelTime { get; }
	}
}
