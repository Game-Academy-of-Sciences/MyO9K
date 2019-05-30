using System;
using O9K.AIO.Modes.Combo;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Earthshaker.Modes
{
	// Token: 0x02000145 RID: 325
	internal class EarthshakerComboModeMenu : ComboModeMenu
	{
		// Token: 0x06000670 RID: 1648 RVA: 0x0001F588 File Offset: 0x0001D788
		public EarthshakerComboModeMenu(Menu rootMenu, string displayName) : base(rootMenu, displayName)
		{
			this.PreferEnchantTotem = new MenuSwitcher("Prioritize enchant totem", "comboShakerTotem" + base.SimplifiedName, false, false);
			this.PreferEnchantTotem.Tooltip = "Prioritize enchant totem in combo";
			base.Menu.Add<MenuSwitcher>(this.PreferEnchantTotem);
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x00005475 File Offset: 0x00003675
		public MenuSwitcher PreferEnchantTotem { get; }
	}
}
