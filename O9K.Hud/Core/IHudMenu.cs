using System;
using O9K.Core.Managers.Menu.Items;

namespace O9K.Hud.Core
{
	// Token: 0x020000B0 RID: 176
	internal interface IHudMenu
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060003ED RID: 1005
		Menu MapMenu { get; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060003EE RID: 1006
		Menu TopPanelMenu { get; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060003EF RID: 1007
		Menu MinimapSettingsMenu { get; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060003F0 RID: 1008
		Menu TopPanelSettingsMenu { get; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060003F1 RID: 1009
		Menu NotificationsSettingsMenu { get; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060003F2 RID: 1010
		Menu UnitsMenu { get; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060003F3 RID: 1011
		Menu NotificationsMenu { get; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060003F4 RID: 1012
		Menu ScreenMenu { get; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060003F5 RID: 1013
		Menu RootMenu { get; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060003F6 RID: 1014
		Menu ParticlesMenu { get; }
	}
}
