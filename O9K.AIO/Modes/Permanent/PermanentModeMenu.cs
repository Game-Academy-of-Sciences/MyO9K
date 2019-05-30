using System;
using O9K.AIO.Modes.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.Permanent
{
	// Token: 0x02000020 RID: 32
	internal class PermanentModeMenu : BaseModeMenu
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x00009F34 File Offset: 0x00008134
		public PermanentModeMenu(Menu rootMenu, string displayName, string tooltip = null) : base(rootMenu, displayName)
		{
			this.Enabled = new MenuSwitcher("Enabled", "Enabled" + base.SimplifiedName, true, true);
			if (tooltip != null)
			{
				this.Enabled.Tooltip = tooltip;
			}
			base.Menu.Add<MenuSwitcher>(this.Enabled);
			rootMenu.Add<Menu>(base.Menu);
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x0000289C File Offset: 0x00000A9C
		public MenuSwitcher Enabled { get; }
	}
}
