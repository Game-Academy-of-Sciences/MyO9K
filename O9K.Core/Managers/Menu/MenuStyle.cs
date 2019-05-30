using System;
using System.Drawing;
using Ensage;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.Core.Managers.Menu
{
	// Token: 0x02000044 RID: 68
	public class MenuStyle
	{
		// Token: 0x060001CD RID: 461 RVA: 0x0001586C File Offset: 0x00013A6C
		public MenuStyle()
		{
			if (Drawing.RenderMode == RenderMode.Dx11)
			{
				this.BackgroundColor = Color.FromArgb(175, 5, 5, 5);
				this.MenuSplitLineColor = Color.FromArgb(75, 255, 255, 255);
				this.HeaderBackgroundColor = Color.FromArgb(200, 3, 3, 3);
				this.MenuSplitLineSize = 0.1f;
				return;
			}
			this.BackgroundColor = Color.FromArgb(90, 5, 5, 5);
			this.MenuSplitLineColor = Color.FromArgb(10, 255, 255, 255);
			this.HeaderBackgroundColor = Color.FromArgb(140, 3, 3, 3);
			this.MenuSplitLineSize = 1f;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003344 File Offset: 0x00001544
		public float Height { get; } = 35f * Hud.Info.ScreenRatio;

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001CF RID: 463 RVA: 0x0000334C File Offset: 0x0000154C
		public float LeftIndent { get; } = 10f * Hud.Info.ScreenRatio;

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001D0 RID: 464 RVA: 0x00003354 File Offset: 0x00001554
		public float RightIndent { get; } = 10f * Hud.Info.ScreenRatio;

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000335C File Offset: 0x0000155C
		public float TextSize { get; } = 16f * Hud.Info.ScreenRatio;

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003364 File Offset: 0x00001564
		public float TooltipTextSize { get; } = 14f * Hud.Info.ScreenRatio;

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x0000336C File Offset: 0x0000156C
		public string Font { get; } = "Calibri";

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00003374 File Offset: 0x00001574
		public string TextureArrowKey { get; } = "menu_arrow";

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D5 RID: 469 RVA: 0x0000337C File Offset: 0x0000157C
		public string TextureIconKey { get; } = "menu_icon";

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00003384 File Offset: 0x00001584
		public string TextureLeftArrowKey { get; } = "menu_right_arrow";

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D7 RID: 471 RVA: 0x0000338C File Offset: 0x0000158C
		public float TextureArrowSize { get; } = 12f * Hud.Info.ScreenRatio;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00003394 File Offset: 0x00001594
		public float TextureAbilitySize { get; } = 27f * Hud.Info.ScreenRatio;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060001D9 RID: 473 RVA: 0x0000339C File Offset: 0x0000159C
		public Vector2 TextureHeroSize { get; } = new Vector2(35f, 25f) * Hud.Info.ScreenRatio;

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060001DA RID: 474 RVA: 0x000033A4 File Offset: 0x000015A4
		public Color HeaderBackgroundColor { get; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060001DB RID: 475 RVA: 0x000033AC File Offset: 0x000015AC
		public Color MenuSplitLineColor { get; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060001DC RID: 476 RVA: 0x000033B4 File Offset: 0x000015B4
		public float MenuSplitLineSize { get; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060001DD RID: 477 RVA: 0x000033BC File Offset: 0x000015BC
		public Color BackgroundColor { get; }
	}
}
