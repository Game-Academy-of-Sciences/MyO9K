using System;
using O9K.Core.Entities;
using O9K.Core.Managers.Menu;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Menu
{
	// Token: 0x0200002D RID: 45
	internal class MenuManager : IDisposable
	{
		// Token: 0x060000FA RID: 250 RVA: 0x0000AB28 File Offset: 0x00008D28
		public MenuManager(Entity9 owner, IMenuManager9 menuManager)
		{
			this.menuManager = menuManager;
			this.RootMenu = new Menu("AIO", "O9K.AIO").SetTexture(owner.Name);
			this.GeneralSettingsMenu = new Menu("General settings");
			this.RootMenu.Add<Menu>(this.GeneralSettingsMenu);
			this.Enabled = new MenuSwitcher("Enabled", true, true).SetTooltip("Enable assembly for " + owner.DisplayName);
			this.GeneralSettingsMenu.Add<MenuSwitcher>(this.Enabled);
			menuManager.AddRootMenu(this.RootMenu);
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00002B3F File Offset: 0x00000D3F
		public MenuSwitcher Enabled { get; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00002B47 File Offset: 0x00000D47
		public Menu RootMenu { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FD RID: 253 RVA: 0x00002B4F File Offset: 0x00000D4F
		public Menu GeneralSettingsMenu { get; }

		// Token: 0x060000FE RID: 254 RVA: 0x00002B57 File Offset: 0x00000D57
		public void Dispose()
		{
			this.menuManager.RemoveRootMenu(this.RootMenu);
		}

		// Token: 0x0400008D RID: 141
		private readonly IMenuManager9 menuManager;
	}
}
