using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Ursa.Abilities
{
	// Token: 0x02000061 RID: 97
	internal class EnrageMenu : UsableAbilityMenu
	{
		// Token: 0x0600020E RID: 526 RVA: 0x0000EEF8 File Offset: 0x0000D0F8
		public EnrageMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.StacksCount = new MenuSlider("Stacks count", ability.DefaultName + "stacks" + simplifiedName, 3, 0, 10, false);
			this.StacksCount.Tooltip = "Use ability only if enemy has equals/more fury swipes stacks";
			base.Menu.Add<MenuSlider>(this.StacksCount);
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600020F RID: 527 RVA: 0x000035D8 File Offset: 0x000017D8
		public MenuSlider StacksCount { get; }
	}
}
