using System;
using Ensage;
using O9K.Core.Managers.Menu;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Menu
{
	// Token: 0x0200002E RID: 46
	internal class DisabledMenuManager : IDisposable
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public DisabledMenuManager(Entity owner, IMenuManager9 menuManager)
		{
			this.menuManager = menuManager;
			this.RootMenu = new Menu("AIO *", "O9K.AIO").SetTexture(owner.Name);
			this.RootMenu.Add<MenuText>(new MenuText("Current hero is paid"));
			this.RootMenu.Add<MenuText>(new MenuText("You need O9K.AIO.Unlocker"));
			menuManager.AddRootMenu(this.RootMenu);
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00002B6A File Offset: 0x00000D6A
		public Menu RootMenu { get; }

		// Token: 0x06000101 RID: 257 RVA: 0x00002B72 File Offset: 0x00000D72
		public void Dispose()
		{
			this.menuManager.RemoveRootMenu(this.RootMenu);
		}

		// Token: 0x04000091 RID: 145
		private readonly IMenuManager9 menuManager;
	}
}
