using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Abilities.Menus
{
	// Token: 0x02000209 RID: 521
	internal class UsableAbilityMenu
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x00007395 File Offset: 0x00005595
		public UsableAbilityMenu(Ability9 ability, string simplifiedName)
		{
			this.Menu = new Menu(ability.DisplayName, "settings" + ability.Name + simplifiedName);
			this.Menu.TextureKey = ability.Name;
		}

		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x000073D0 File Offset: 0x000055D0
		public Menu Menu { get; }
	}
}
