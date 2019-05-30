using System;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.KillStealer
{
	// Token: 0x02000035 RID: 53
	internal class KillStealMenu
	{
		// Token: 0x06000135 RID: 309 RVA: 0x0000C148 File Offset: 0x0000A348
		public KillStealMenu(Menu rootMenu)
		{
			Menu menu = new Menu("Kill steal");
			this.KillStealEnabled = new MenuSwitcher("Kill steal", "killStealEnabled", true, true);
			this.KillStealEnabled.Tooltip = "Enable kill steal combo";
			menu.Add<MenuSwitcher>(this.KillStealEnabled);
			this.OverlayEnabled = new MenuSwitcher("Overlay", "killStealOverlayEnabled", true, true);
			this.OverlayEnabled.Tooltip = "Show damage overlay";
			menu.Add<MenuSwitcher>(this.OverlayEnabled);
			this.killStealAbilityToggler = new MenuAbilityToggler("Abilities", "killStealAbilities", null, true, true);
			menu.Add<MenuAbilityToggler>(this.killStealAbilityToggler);
			Menu menu2 = new Menu("Overlay settings");
			this.OverlayX = new MenuSlider("X", "xCoord", 0, -50, 50, false);
			menu2.Add<MenuSlider>(this.OverlayX);
			this.OverlayY = new MenuSlider("Y", "yCoord", 0, -50, 50, false);
			menu2.Add<MenuSlider>(this.OverlayY);
			this.OverlaySizeX = new MenuSlider("X size", "xSize", 0, -20, 20, false);
			menu2.Add<MenuSlider>(this.OverlaySizeX);
			this.OverlaySizeY = new MenuSlider("Y size", "ySize", 0, -20, 20, false);
			menu2.Add<MenuSlider>(this.OverlaySizeY);
			menu.Add<Menu>(menu2);
			rootMenu.Add<Menu>(menu);
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000136 RID: 310 RVA: 0x00002DD9 File Offset: 0x00000FD9
		public MenuSwitcher KillStealEnabled { get; }

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000137 RID: 311 RVA: 0x00002DE1 File Offset: 0x00000FE1
		public MenuSwitcher OverlayEnabled { get; }

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000138 RID: 312 RVA: 0x00002DE9 File Offset: 0x00000FE9
		public MenuSlider OverlayX { get; }

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000139 RID: 313 RVA: 0x00002DF1 File Offset: 0x00000FF1
		public MenuSlider OverlaySizeX { get; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600013A RID: 314 RVA: 0x00002DF9 File Offset: 0x00000FF9
		public MenuSlider OverlaySizeY { get; }

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600013B RID: 315 RVA: 0x00002E01 File Offset: 0x00001001
		public MenuSlider OverlayY { get; }

		// Token: 0x0600013C RID: 316 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public void AddKillStealAbility(ActiveAbility ability)
		{
			this.killStealAbilityToggler.AddAbility(ability.Id, null);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00002E09 File Offset: 0x00001009
		public bool IsAbilityEnabled(string abilityName)
		{
			return this.killStealAbilityToggler.IsEnabled(abilityName);
		}

		// Token: 0x040000B6 RID: 182
		private readonly MenuAbilityToggler killStealAbilityToggler;
	}
}
