using System;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.Magnus.Modes
{
	// Token: 0x0200010F RID: 271
	internal class BlinkSkewerModeMenu : KeyPressModeMenu
	{
		// Token: 0x0600055E RID: 1374 RVA: 0x0001B838 File Offset: 0x00019A38
		public BlinkSkewerModeMenu(Menu rootMenu, string displayName) : base(rootMenu, displayName, null)
		{
			this.allyToggler = new MenuHeroToggler("Skewer to ally", "skewerAllies" + base.SimplifiedName, true, false, true, false);
			base.Menu.Add<MenuHeroToggler>(this.allyToggler);
		}

		// Token: 0x0600055F RID: 1375 RVA: 0x00004CE0 File Offset: 0x00002EE0
		public bool IsAllyEnabled(string heroName)
		{
			return this.allyToggler.IsEnabled(heroName);
		}

		// Token: 0x04000300 RID: 768
		private readonly MenuHeroToggler allyToggler;
	}
}
