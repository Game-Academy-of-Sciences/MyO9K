using System;
using System.Windows.Input;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.EmberSpirit.Modes
{
	// Token: 0x0200017E RID: 382
	internal class AutoChainsModeMenu : PermanentModeMenu
	{
		// Token: 0x060007DC RID: 2012 RVA: 0x00005F6A File Offset: 0x0000416A
		public AutoChainsModeMenu(Menu rootMenu, string displayName, string tooltip = null) : base(rootMenu, displayName, tooltip)
		{
			this.fistKey = base.Menu.Add<MenuHoldKey>(new MenuHoldKey("Sleight of Fist key", Key.W, false).SetTooltip("Set to Dota's Sleight of Fist key"));
		}

		// Token: 0x04000453 RID: 1107
		public MenuHoldKey fistKey;
	}
}
