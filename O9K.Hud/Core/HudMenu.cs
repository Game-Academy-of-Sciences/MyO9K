using System;
using System.ComponentModel.Composition;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Menu.Items;

namespace O9K.Hud.Core
{
	// Token: 0x020000B2 RID: 178
	[Export(typeof(IHudMenu))]
	internal class HudMenu : IDisposable, IHudMenu, IHudModule
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x0001E068 File Offset: 0x0001C268
		[ImportingConstructor]
		public HudMenu(IContext9 context)
		{
			this.context = context;
			this.RootMenu = new Menu("Hud", "O9K.Hud").SetTexture("me");
			this.UnitsMenu = this.RootMenu.Add<Menu>(new Menu("Units"));
			this.TopPanelMenu = this.RootMenu.Add<Menu>(new Menu("Top panel"));
			this.MapMenu = this.RootMenu.Add<Menu>(new Menu("Map"));
			this.ParticlesMenu = this.RootMenu.Add<Menu>(new Menu("Particles"));
			this.NotificationsMenu = this.RootMenu.Add<Menu>(new Menu("Notifications"));
			this.ScreenMenu = this.RootMenu.Add<Menu>(new Menu("Screen"));
			this.MinimapSettingsMenu = this.MapMenu.Add<Menu>(new Menu("Minimap settings"));
			this.TopPanelSettingsMenu = this.TopPanelMenu.Add<Menu>(new Menu("Settings"));
			this.NotificationsSettingsMenu = this.NotificationsMenu.Add<Menu>(new Menu("Settings"));
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060003F9 RID: 1017 RVA: 0x0000473C File Offset: 0x0000293C
		public Menu RootMenu { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003FA RID: 1018 RVA: 0x00004744 File Offset: 0x00002944
		public Menu NotificationsMenu { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003FB RID: 1019 RVA: 0x0000474C File Offset: 0x0000294C
		public Menu NotificationsSettingsMenu { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003FC RID: 1020 RVA: 0x00004754 File Offset: 0x00002954
		public Menu MinimapSettingsMenu { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0000475C File Offset: 0x0000295C
		public Menu TopPanelSettingsMenu { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x00004764 File Offset: 0x00002964
		public Menu MapMenu { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003FF RID: 1023 RVA: 0x0000476C File Offset: 0x0000296C
		public Menu TopPanelMenu { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000400 RID: 1024 RVA: 0x00004774 File Offset: 0x00002974
		public Menu UnitsMenu { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000401 RID: 1025 RVA: 0x0000477C File Offset: 0x0000297C
		public Menu ParticlesMenu { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000402 RID: 1026 RVA: 0x00004784 File Offset: 0x00002984
		public Menu ScreenMenu { get; }

		// Token: 0x06000403 RID: 1027 RVA: 0x0001E194 File Offset: 0x0001C394
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("me", "panorama\\images\\textures\\minimap_hero_self_psd.vtex_c", 0, 0, false, 0, null);
			this.context.MenuManager.AddRootMenu(this.RootMenu);
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x0000478C File Offset: 0x0000298C
		public void Dispose()
		{
			this.context.MenuManager.AddRootMenu(this.RootMenu);
		}

		// Token: 0x04000281 RID: 641
		private readonly IContext9 context;
	}
}
