using System;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.TargetManager
{
	// Token: 0x0200001A RID: 26
	internal class TargetManagerMenu
	{
		// Token: 0x06000095 RID: 149 RVA: 0x0000979C File Offset: 0x0000799C
		public TargetManagerMenu(Menu rootMenu)
		{
			Menu menu = new Menu("Target selector");
			this.FocusTarget = new MenuSelector("Focus target", new string[]
			{
				"Near mouse",
				"Near hero",
				"Lowest health"
			}, true);
			menu.Add<MenuSelector>(this.FocusTarget);
			this.AdditionalTargets = new MenuSwitcher("Additional targets", true, false).SetTooltip("Allow to target units like spirit bear, wards, courier, tombstone etc.");
			menu.Add<MenuSwitcher>(this.AdditionalTargets);
			this.DrawTargetParticle = new MenuSwitcher("Draw target particle", "drawTarget", true, true);
			menu.Add<MenuSwitcher>(this.DrawTargetParticle);
			this.LockTarget = new MenuSwitcher("Lock target", "lockTarget", true, true);
			this.LockTarget.Tooltip = "Lock target while combo is active";
			menu.Add<MenuSwitcher>(this.LockTarget);
			this.DeathSwitch = new MenuSwitcher("Death switch", "deathSwitch", true, true);
			this.DeathSwitch.Tooltip = "Auto switch target if previous died";
			menu.Add<MenuSwitcher>(this.DeathSwitch);
			rootMenu.Add<Menu>(menu);
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000026E6 File Offset: 0x000008E6
		public MenuSelector FocusTarget { get; }

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000026EE File Offset: 0x000008EE
		public MenuSwitcher AdditionalTargets { get; }

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000026F6 File Offset: 0x000008F6
		public MenuSwitcher DrawTargetParticle { get; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000026FE File Offset: 0x000008FE
		public MenuSwitcher DeathSwitch { get; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00002706 File Offset: 0x00000906
		public MenuSwitcher LockTarget { get; }
	}
}
