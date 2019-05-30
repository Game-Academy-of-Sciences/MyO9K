using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Mars.Abilities
{
	// Token: 0x02000109 RID: 265
	internal class SpearOfMarsMenu : UsableAbilityMenu
	{
		// Token: 0x06000542 RID: 1346 RVA: 0x0001B174 File Offset: 0x00019374
		public SpearOfMarsMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.StunOnly = new MenuSwitcher("Stun only", "stunOnly" + simplifiedName, true, false);
			this.StunOnly.Tooltip = "Use spear only when it will stun the enemy";
			base.Menu.Add<MenuSwitcher>(this.StunOnly);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x00004B8B File Offset: 0x00002D8B
		public MenuSwitcher StunOnly { get; }
	}
}
