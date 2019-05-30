using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Riki.Abilities
{
	// Token: 0x020000BF RID: 191
	internal class TricksOfTheTradeMenu : UsableAbilityMenu
	{
		// Token: 0x060003D6 RID: 982 RVA: 0x0001582C File Offset: 0x00013A2C
		public TricksOfTheTradeMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.SmartUsage = new MenuSwitcher("Smart usage", "smart" + simplifiedName, true, false);
			this.SmartUsage.Tooltip = "Use ability only when target is immobile or has low health";
			base.Menu.Add<MenuSwitcher>(this.SmartUsage);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060003D7 RID: 983 RVA: 0x000041F6 File Offset: 0x000023F6
		public MenuSwitcher SmartUsage { get; }
	}
}
