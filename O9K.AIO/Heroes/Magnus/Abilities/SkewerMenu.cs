using System;
using O9K.AIO.Abilities.Menus;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Magnus.Abilities
{
	// Token: 0x02000117 RID: 279
	internal class SkewerMenu : UsableAbilityMenu
	{
		// Token: 0x06000587 RID: 1415 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public SkewerMenu(Ability9 ability, string simplifiedName) : base(ability, simplifiedName)
		{
			this.allyToggler = new MenuHeroToggler("Skewer to ally", "skewerAllies" + simplifiedName, true, false, true, false);
			base.Menu.Add<MenuHeroToggler>(this.allyToggler);
			this.SkewerToTower = new MenuSwitcher("Tower", "skewerTower" + simplifiedName, true, false);
			this.SkewerToTower.Tooltip = "Skewer to tower if no allies";
			base.Menu.Add<MenuSwitcher>(this.SkewerToTower);
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x00004E9E File Offset: 0x0000309E
		public MenuSwitcher SkewerToTower { get; }

		// Token: 0x06000589 RID: 1417 RVA: 0x00004EA6 File Offset: 0x000030A6
		public bool IsAllyEnabled(string heroName)
		{
			return this.allyToggler.IsEnabled(heroName);
		}

		// Token: 0x0400030E RID: 782
		private readonly MenuHeroToggler allyToggler;
	}
}
