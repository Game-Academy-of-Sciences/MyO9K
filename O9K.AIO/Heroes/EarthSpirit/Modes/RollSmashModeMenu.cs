using System;
using O9K.AIO.Modes.KeyPress;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Heroes.EarthSpirit.Modes
{
	// Token: 0x0200018C RID: 396
	internal class RollSmashModeMenu : KeyPressModeMenu
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00024F14 File Offset: 0x00023114
		public RollSmashModeMenu(Menu rootMenu, string displayName) : base(rootMenu, displayName, null)
		{
			this.allyToggler = new MenuHeroToggler("Smash to ally", "smashAllies" + base.SimplifiedName, true, false, true, false);
			base.Menu.Add<MenuHeroToggler>(this.allyToggler);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00006086 File Offset: 0x00004286
		public bool IsAllyEnabled(string heroName)
		{
			return this.allyToggler.IsEnabled(heroName);
		}

		// Token: 0x04000474 RID: 1140
		private readonly MenuHeroToggler allyToggler;
	}
}
