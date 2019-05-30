using System;
using System.Windows.Input;
using O9K.AIO.Modes.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.KeyPress
{
	// Token: 0x02000025 RID: 37
	internal class KeyPressModeMenu : BaseModeMenu
	{
		// Token: 0x060000CE RID: 206 RVA: 0x0000A1B0 File Offset: 0x000083B0
		public KeyPressModeMenu(Menu rootMenu, string displayName, string tooltip = null) : base(rootMenu, displayName)
		{
			this.Key = new MenuHoldKey("Key", "key" + base.SimplifiedName, System.Windows.Input.Key.None, true);
			if (tooltip != null)
			{
				this.Key.Tooltip = tooltip;
			}
			base.Menu.Add<MenuHoldKey>(this.Key);
			rootMenu.Add<Menu>(base.Menu);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000CF RID: 207 RVA: 0x000029D6 File Offset: 0x00000BD6
		public MenuHoldKey Key { get; }
	}
}
