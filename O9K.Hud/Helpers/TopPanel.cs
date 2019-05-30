using System;
using System.ComponentModel.Composition;

using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using SharpDX;

namespace O9K.Hud.Helpers
{
	// Token: 0x020000A6 RID: 166
	[Export(typeof(ITopPanel))]
	internal class TopPanel : IDisposable, ITopPanel, IHudModule
	{
		// Token: 0x060003AD RID: 941 RVA: 0x0001CD60 File Offset: 0x0001AF60
		[ImportingConstructor]
		public TopPanel(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
            float num = O9K.Core.Helpers.Hud.Info.ScreenSize.X * 0.053f;
			Size2F size2F=new Size2F(O9K.Core.Helpers.Hud.Info.ScreenSize.X * 0.165f, O9K.Core.Helpers.Hud.Info.ScreenSize.Y * 0.037f);
			Menu topPanelSettingsMenu = hudMenu.TopPanelSettingsMenu;
			this.debug = topPanelSettingsMenu.Add<MenuSwitcher>(new MenuSwitcher("Debug", false, false));
			this.debug.SetTooltip("Use this to adjust top panel position and size");
			this.centerPosition = topPanelSettingsMenu.Add<MenuSlider>(new MenuSlider("Center position", "center", (int)num, 0, (int)(O9K.Core.Helpers.Hud.Info.ScreenSize.X / 4f), false));
			this.sidePosition = topPanelSettingsMenu.Add<MenuSlider>(new MenuSlider("Side position", "side", (int)size2F.Width, 100, (int)(O9K.Core.Helpers.Hud.Info.ScreenSize.X / 2f), false));
			this.botPosition = topPanelSettingsMenu.Add<MenuSlider>(new MenuSlider("Bottom position", "bot", (int)size2F.Height, 25, 100, false));
			if (Game.ExpectedPlayers != 10)
			{
                O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // Top panel will not work correctly without 10 players", 10f);
			}
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001CE88 File Offset: 0x0001B088
		public void Activate()
		{
			this.debug.ValueChange += this.DebugOnValueChange;
			this.centerPosition.ValueChange += this.CenterPositionOnValueChange;
			this.sidePosition.ValueChange += this.SidePositionOnValueChange;
			this.botPosition.ValueChange += this.BotPositionOnValueChange;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001CEF4 File Offset: 0x0001B0F4
		public void Dispose()
		{
			this.context.Renderer.Draw -= this.OnDraw;
			this.debug.ValueChange -= this.DebugOnValueChange;
			this.sidePosition.ValueChange -= this.SidePositionOnValueChange;
			this.centerPosition.ValueChange -= this.CenterPositionOnValueChange;
			this.botPosition.ValueChange -= this.BotPositionOnValueChange;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0001CF7C File Offset: 0x0001B17C
		public Rectangle9 GetPlayerPosition(int id)
		{
			RectangleF rectangleF = (id < 5) ? this.leftPanel : this.rightPanel;
			Rectangle9 result = default(Rectangle9);
			if (id >= 5)
			{
				id -= 5;
			}
			result.Size = new Size2F(this.widthPerPlayer, rectangleF.Height);
			result.Location = new Vector2(rectangleF.Left + result.Width * (float)id, rectangleF.Y);
			return result;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001CFEC File Offset: 0x0001B1EC
		public Rectangle9 GetPlayersHealthBarPosition(int id, float height, float topIndent)
		{
			RectangleF rectangleF = (id < 5) ? this.leftPanel : this.rightPanel;
			Rectangle9 result = default(Rectangle9);
			if (id >= 5)
			{
				id -= 5;
			}
			result.Size = new Size2F(this.widthPerPlayer - 2f, height);
			result.Location = new Vector2(rectangleF.Left + (result.Width + 2f) * (float)id + 1f, rectangleF.Height + topIndent);
			return result;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0001D06C File Offset: 0x0001B26C
		public Rectangle9 GetPlayersUltimatePosition(int id, float size, float topIndent)
		{
			RectangleF rectangleF = (id < 5) ? this.leftPanel : this.rightPanel;
			Rectangle9 result = default(Rectangle9);
			if (id >= 5)
			{
				id -= 5;
			}
			result.Size = new Size2F(size, size);
			result.Location = new Vector2(rectangleF.Left + this.widthPerPlayer * (float)id + this.widthPerPlayer / 2f - size / 2f, rectangleF.Height - size / 2f + topIndent);
			return result;
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0001D0F0 File Offset: 0x0001B2F0
		public Rectangle9 GetScorePosition(Team team)
		{
			if (team == Team.Radiant)
			{
				return new Rectangle9(this.centerPanel.X, this.centerPanel.Y, this.centerPanel.Width * 0.32f, this.centerPanel.Height);
			}
			return new Rectangle9(this.centerPanel.Right - this.centerPanel.Width * 0.31f, this.centerPanel.Y, this.centerPanel.Width * 0.32f, this.centerPanel.Height);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x000045BD File Offset: 0x000027BD
		private void BotPositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.leftPanel.Height = (float)e.NewValue;
			this.rightPanel.Height = (float)e.NewValue;
			this.centerPanel.Height = (float)e.NewValue;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001D184 File Offset: 0x0001B384
		private void CenterPositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.centerPanel.X = O9K.Core.Helpers.Hud.Info.ScreenSize.X / 2f - (float)e.NewValue;
			this.centerPanel.Width = (float)(e.NewValue * 2);
			this.rightPanel.X = this.centerPanel.Right;
			this.leftPanel.X = O9K.Core.Helpers.Hud.Info.ScreenSize.X / 2f - this.centerPanel.Width / 2f - this.leftPanel.Width;
			this.widthPerPlayer = this.leftPanel.Width / 5f;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001D230 File Offset: 0x0001B430
		private void DebugOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001D280 File Offset: 0x0001B480
		private void OnDraw(IRenderer renderer)
		{
			try
			{

                renderer.DrawRectangle(this.leftPanel, System.Drawing.Color.White, 2f);
				renderer.DrawRectangle(this.rightPanel, System.Drawing.Color.White, 2f);
				renderer.DrawRectangle(this.centerPanel, System.Drawing.Color.White, 2f);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001D2EC File Offset: 0x0001B4EC
		private void SidePositionOnValueChange(object sender, SliderEventArgs e)
		{
			this.leftPanel.X = O9K.Core.Helpers.Hud.Info.ScreenSize.X / 2f - this.centerPanel.Width / 2f - (float)e.NewValue;
			this.leftPanel.Width = (float)e.NewValue;
			this.rightPanel.Width = (float)e.NewValue;
			this.widthPerPlayer = this.leftPanel.Width / 5f;
		}

		// Token: 0x0400025E RID: 606
		private const int PlayersPerPanel = 5;

		// Token: 0x0400025F RID: 607
		private readonly MenuSlider botPosition;

		// Token: 0x04000260 RID: 608
		private readonly MenuSlider centerPosition;

		// Token: 0x04000261 RID: 609
		private readonly IContext9 context;

		// Token: 0x04000262 RID: 610
		private readonly MenuSwitcher debug;

		// Token: 0x04000263 RID: 611
		private readonly MenuSlider sidePosition;

		// Token: 0x04000264 RID: 612
		private RectangleF centerPanel;

		// Token: 0x04000265 RID: 613
		private RectangleF leftPanel;

		// Token: 0x04000266 RID: 614
		private RectangleF rightPanel;

		// Token: 0x04000267 RID: 615
		private float widthPerPlayer;
	}
}
