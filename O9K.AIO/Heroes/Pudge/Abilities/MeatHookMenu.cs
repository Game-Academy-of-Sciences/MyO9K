using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Pudge.Abilities
{
	// Token: 0x020000CE RID: 206
	internal class MeatHookMenu : UsableAbilityMenu
	{
		// Token: 0x06000439 RID: 1081 RVA: 0x00017314 File Offset: 0x00015514
		public MeatHookMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.Delay = new MenuSlider("Delay (ms)", ability.DefaultName + simplifiedName, 100, 0, 500, false);
			this.Delay.Tooltip = "Use ability only when enemy is moving in the same direction";
			base.Menu.Add<MenuSlider>(this.Delay);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600043A RID: 1082 RVA: 0x00004377 File Offset: 0x00002577
		public MenuSlider Delay { get; }
	}
}
