using System;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.FailSafe
{
	// Token: 0x020001F8 RID: 504
	internal class FailSafeMenu
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x0002BAD4 File Offset: 0x00029CD4
		public FailSafeMenu(Menu rootMenu)
		{
			Menu menu = new Menu("Fail safe");
			this.FailSafeEnabled = new MenuSwitcher("Fail safe", "failSafeEnable", true, true);
			this.FailSafeEnabled.Tooltip = "Cancel ability if it won't hit the target";
			menu.Add<MenuSwitcher>(this.FailSafeEnabled);
			rootMenu.Add<Menu>(menu);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00006F0B File Offset: 0x0000510B
		public MenuSwitcher FailSafeEnabled { get; }
	}
}
