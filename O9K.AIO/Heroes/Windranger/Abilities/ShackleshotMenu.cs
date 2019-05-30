using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x02000042 RID: 66
	internal class ShackleshotMenu : UsableAbilityMenu
	{
		// Token: 0x06000179 RID: 377 RVA: 0x0000D530 File Offset: 0x0000B730
		public ShackleshotMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.MoveToShackle = new MenuSwitcher("Move to shackle", "shackleMove" + simplifiedName, true, false);
			this.MoveToShackle.Tooltip = "Auto position your hero to shackle the enemy";
			base.Menu.Add<MenuSwitcher>(this.MoveToShackle);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600017A RID: 378 RVA: 0x000030CF File Offset: 0x000012CF
		public MenuSwitcher MoveToShackle { get; }
	}
}
