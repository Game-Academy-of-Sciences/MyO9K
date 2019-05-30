using System;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.StormSpirit.Modes
{
	// Token: 0x02000098 RID: 152
	internal class ManaCalculatorModeMenu : PermanentModeMenu
	{
		// Token: 0x060002FE RID: 766 RVA: 0x000123DC File Offset: 0x000105DC
		public ManaCalculatorModeMenu(Menu rootMenu, string displayName, string tooltip = null) : base(rootMenu, displayName, tooltip)
		{
			this.ShowRemainingMp = base.Menu.Add<MenuSwitcher>(new MenuSwitcher("Remaining MP", "remainingMp" + base.SimplifiedName, true, false));
			this.ShowRemainingMp.SetTooltip("Show remaining or required MP");
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00003E2C File Offset: 0x0000202C
		public MenuSwitcher ShowRemainingMp { get; }
	}
}
