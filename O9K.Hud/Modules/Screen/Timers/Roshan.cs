using System;
using System.ComponentModel.Composition;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
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
	// Token: 0x0200002D RID: 45
	internal class Roshan : IDisposable, IHudModule
	{
		// Token: 0x060000F7 RID: 247 RVA: 0x00009BD4 File Offset: 0x00007DD4
		[ImportingConstructor]
		public Roshan(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.ScreenMenu.GetOrAdd<Menu>(new Menu("Timers")).Add<Menu>(new Menu("Roshan"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.showRemaining = menu.Add<MenuSwitcher>(new MenuSwitcher("Remaining time", true, false)).SetTooltip("Show remaining time or respawn time");
			this.showMinTime = menu.Add<MenuSwitcher>(new MenuSwitcher("Minimum time", true, false)).SetTooltip("Show minimum respawn time");
			this.hide = menu.Add<MenuSwitcher>(new MenuSwitcher("Auto hide", true, false)).SetTooltip("Hide timer if roshan is spawned");
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.textSize = menu2.Add<MenuSlider>(new MenuSlider("Size", 15, 10, 35, false));
			this.textPosition = new MenuVectorSlider(menu2, O9K.Core.Helpers.Hud.Info.ScanPosition + new Vector2(0f, -50f));
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000029B1 File Offset: 0x00000BB1
		public void Activate()
		{
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009CFC File Offset: 0x00007EFC
		public void Dispose()
		{
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
			Game.OnFireEvent -= this.OnFireEvent;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.textPosition.Dispose();
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009D6C File Offset: 0x00007F6C
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDraw;
				Game.OnFireEvent += this.OnFireEvent;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
			Game.OnFireEvent -= this.OnFireEvent;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009DF0 File Offset: 0x00007FF0
		private void OnAbilityRemoved(Ability9 ability)
		{
			if (ability.Id != AbilityId.item_aegis || !ability.Owner.IsHero || ability.Owner.IsIllusion)
			{
				return;
			}
			this.aegisPickUpTime = -999999f;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00009E40 File Offset: 0x00008040
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				float rawGameTime = Game.RawGameTime;
				float num = rawGameTime - this.roshanKillTime;
				float num2 = rawGameTime - this.aegisPickUpTime;
				string text;
				if (num > 660f)
				{
					if (this.hide)
					{
						return;
					}
					text = "Alive";
				}
				else if (num > 480f)
				{
					float num3 = 660f - num;
					if (!this.showRemaining)
					{
						num3 += Game.GameTime;
					}
					text = TimeSpan.FromSeconds((double)num3).ToString("m\\:ss") + "*";
				}
				else if (num2 <= 300f)
				{
					float num4 = 300f - num2;
					if (!this.showRemaining)
					{
						num4 += Game.GameTime;
					}
					text = TimeSpan.FromSeconds((double)num4).ToString("m\\:ss") + "!";
				}
				else
				{
					float num5 = 660f - num;
					if (!this.showRemaining)
					{
						num5 += Game.GameTime;
					}
					text = TimeSpan.FromSeconds((double)num5).ToString("m\\:ss");
					if (this.showMinTime)
					{
						text = TimeSpan.FromSeconds((double)(num5 - 180f)).ToString("m\\:ss") + Environment.NewLine + text;
					}
				}
				O9K.Hud.Helpers.Drawer.DrawTextWithBackground(text, this.textSize, this.textPosition, renderer);
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00009FCC File Offset: 0x000081CC
		private void OnFireEvent(FireEventEventArgs args)
		{
			string name = args.GameEvent.Name;
			if (name == "dota_roshan_kill")
			{
				this.roshanKillTime = Game.RawGameTime;
				return;
			}
			if (!(name == "aegis_event"))
			{
				return;
			}
			this.aegisPickUpTime = Game.RawGameTime;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
		}

		// Token: 0x040000A7 RID: 167
		private readonly IContext9 context;

		// Token: 0x040000A8 RID: 168
		private readonly MenuSwitcher enabled;

		// Token: 0x040000A9 RID: 169
		private readonly MenuSwitcher hide;

		// Token: 0x040000AA RID: 170
		private readonly MenuVectorSlider textPosition;

		// Token: 0x040000AB RID: 171
		private readonly MenuSlider textSize;

		// Token: 0x040000AC RID: 172
		private readonly MenuSwitcher showRemaining;

		// Token: 0x040000AD RID: 173
		private readonly MenuSwitcher showMinTime;

		// Token: 0x040000AE RID: 174
		private float aegisPickUpTime = -999999f;

		// Token: 0x040000AF RID: 175
		private float roshanKillTime = -99999f;
	}
}
