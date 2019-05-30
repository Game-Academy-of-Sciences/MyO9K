using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Tusk.Abilities
{
	// Token: 0x02000068 RID: 104
	internal class KickMenu : UsableAbilityMenu
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00003692 File Offset: 0x00001892
		public KickMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.KickToAlly = new MenuSwitcher("Kick to ally", ability.DefaultName + "kick" + simplifiedName, true, false);
			base.Menu.Add<MenuSwitcher>(this.KickToAlly);
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600022C RID: 556 RVA: 0x000036D1 File Offset: 0x000018D1
		public MenuSwitcher KickToAlly { get; }
	}
}
