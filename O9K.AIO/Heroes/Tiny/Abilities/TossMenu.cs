using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Tiny.Abilities
{
	// Token: 0x02000073 RID: 115
	internal class TossMenu : UsableAbilityMenu
	{
		// Token: 0x06000265 RID: 613 RVA: 0x00010208 File Offset: 0x0000E408
		public TossMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.TossToAlly = new MenuSwitcher("Toss to ally", "tossToAlly" + simplifiedName, false, false).SetTooltip("Toss enemy to ally");
			base.Menu.Add<MenuSwitcher>(this.TossToAlly);
			this.TossAlly = new MenuHeroToggler("Ally", true, false, true, false);
			base.Menu.Add<MenuHeroToggler>(this.TossAlly);
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000266 RID: 614 RVA: 0x00003857 File Offset: 0x00001A57
		public MenuSwitcher TossToAlly { get; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000267 RID: 615 RVA: 0x0000385F File Offset: 0x00001A5F
		public MenuHeroToggler TossAlly { get; }
	}
}
