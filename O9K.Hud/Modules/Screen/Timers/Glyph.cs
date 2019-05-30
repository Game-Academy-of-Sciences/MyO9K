using System;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Screen.Timers
{
	// Token: 0x0200002C RID: 44
	internal class Glyph : IDisposable, IHudModule
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x000099D8 File Offset: 0x00007BD8
		[ImportingConstructor]
		public Glyph(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.ScreenMenu.GetOrAdd<Menu>(new Menu("Timers")).Add<Menu>(new Menu("Glyph"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.showRemaining = menu.Add<MenuSwitcher>(new MenuSwitcher("Remaining time", true, false)).SetTooltip("Show remaining time or ready time");
			this.hide = menu.Add<MenuSwitcher>(new MenuSwitcher("Auto hide", true, false)).SetTooltip("Hide timer if glyph is ready");
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.textSize = menu2.Add<MenuSlider>(new MenuSlider("Size", 15, 10, 35, false));
			this.textPosition = new MenuVectorSlider(menu2, O9K.Core.Helpers.Hud.Info.GlyphPosition + new Vector2(0f, 10f));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00002948 File Offset: 0x00000B48
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00002971 File Offset: 0x00000B71
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
			this.textPosition.Dispose();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00009AC8 File Offset: 0x00007CC8
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00009B18 File Offset: 0x00007D18
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				float num = (this.ownerTeam == Team.Radiant) ? Game.GlyphCooldownDire : Game.GlyphCooldownRadiant;
				if (num > 0f)
				{
					if (!this.showRemaining)
					{
						num += Game.GameTime;
					}
					O9K.Hud.Helpers.Drawer.DrawTextWithBackground(TimeSpan.FromSeconds((double)num).ToString("m\\:ss"), this.textSize, this.textPosition, renderer);
				}
				else if (!this.hide)
				{
					O9K.Hud.Helpers.Drawer.DrawTextWithBackground("Ready", this.textSize, this.textPosition, renderer);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x040000A0 RID: 160
		private readonly IContext9 context;

		// Token: 0x040000A1 RID: 161
		private readonly MenuSwitcher enabled;

		// Token: 0x040000A2 RID: 162
		private readonly MenuSwitcher hide;

		// Token: 0x040000A3 RID: 163
		private readonly MenuSwitcher showRemaining;

		// Token: 0x040000A4 RID: 164
		private readonly MenuVectorSlider textPosition;

		// Token: 0x040000A5 RID: 165
		private readonly MenuSlider textSize;

		// Token: 0x040000A6 RID: 166
		private Team ownerTeam;
	}
}
