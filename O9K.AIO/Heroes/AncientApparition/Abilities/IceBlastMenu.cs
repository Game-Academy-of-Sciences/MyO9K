using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.AncientApparition.Abilities
{
	// Token: 0x020001F1 RID: 497
	internal class IceBlastMenu : UsableAbilityMenu
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x0002AE58 File Offset: 0x00029058
		public IceBlastMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.StunOnly = new MenuSwitcher("Stun only", "stunOnly" + simplifiedName, false, false);
			this.StunOnly.Tooltip = "Use ice blast only when enemy is stunned/rooted";
			base.Menu.Add<MenuSwitcher>(this.StunOnly);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00006DD9 File Offset: 0x00004FD9
		public MenuSwitcher StunOnly { get; }
	}
}
