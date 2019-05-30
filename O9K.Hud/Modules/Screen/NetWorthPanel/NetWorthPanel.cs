using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Entities.Units.Unique;
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

namespace O9K.Hud.Modules.Screen.NetWorthPanel
{
	// Token: 0x02000030 RID: 48
	internal class NetWorthPanel : IDisposable, IHudModule
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000A5A0 File Offset: 0x000087A0
		[ImportingConstructor]
		public NetWorthPanel(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.ScreenMenu.Add<Menu>(new Menu("Net worth panel"));
			this.show = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", "enabled", false, false)).SetTooltip("Show net worth of the heroes");
			this.allies = menu.Add<MenuSwitcher>(new MenuSwitcher("Allies", "allies", true, false));
			this.enemies = menu.Add<MenuSwitcher>(new MenuSwitcher("Enemies", "enemies", true, false));
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.size = menu2.Add<MenuSlider>(new MenuSlider("Size", "size", 25, 20, 60, false));
			this.position = new MenuVectorSlider(menu2, new Vector2 (O9K.Core.Helpers.Hud.Info.ScreenSize.X * 0.19f, O9K.Core.Helpers.Hud.Info.ScreenSize.Y * 0.75f));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x0000A6A0 File Offset: 0x000088A0
		public void Activate()
		{
			this.context.Renderer.TextureManager.LoadFromDota("net_worth_bg_ally", "panorama\\images\\masks\\gradient_leftright_png.vtex_c", 0, 0, false, -180, new Vector4?(new Vector4(0f, 1f, 0f, 0.9f)));
			this.context.Renderer.TextureManager.LoadFromDota("net_worth_bg_enemy", "panorama\\images\\masks\\gradient_leftright_png.vtex_c", 0, 0, false, -180, new Vector4?(new Vector4(1f, 0f, 0f, 0.9f)));
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
			this.size.ValueChange += this.SizeOnValueChange;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x0000A778 File Offset: 0x00008978
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.show.ValueChange -= this.ShowOnValueChange;
			this.size.ValueChange -= this.SizeOnValueChange;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000A814 File Offset: 0x00008A14
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (ability.IsItem)
				{
					Unit9 unit = ability.Owner;
					if (unit is SpiritBear)
					{
						unit = EntityManager9.GetUnit(unit.Owner.Handle);
						if (unit == null)
						{
							return;
						}
					}
					if (this.units.ContainsKey(unit))
					{
						Dictionary<Unit9, int> dictionary = this.units;
						Unit9 key = unit;
						dictionary[key] += (int)ability.BaseItem.Cost;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000111 RID: 273 RVA: 0x0000A8A4 File Offset: 0x00008AA4
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (ability.IsItem)
				{
					Unit9 unit = ability.Owner;
					if (unit is SpiritBear)
					{
						unit = EntityManager9.GetUnit(unit.Owner.Handle);
						if (unit == null)
						{
							return;
						}
					}
					if (this.units.ContainsKey(unit))
					{
						Dictionary<Unit9, int> dictionary = this.units;
						Unit9 key = unit;
						dictionary[key] -= (int)ability.BaseItem.Cost;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000112 RID: 274 RVA: 0x0000A934 File Offset: 0x00008B34
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				Vector2 vector = this.position.Value;
				foreach (KeyValuePair<Unit9, int> keyValuePair in from x in this.units
				orderby x.Value descending
				select x)
				{
					Unit9 key = keyValuePair.Key;
					if (key.IsValid)
					{
						if (key.Team == this.ownerTeam)
						{
							if (!this.allies)
							{
								continue;
							}
							renderer.DrawTexture("net_worth_bg_ally", vector, this.lineSize, 0f, 1f);
						}
						else
						{
							if (!this.enemies)
							{
								continue;
							}
							renderer.DrawTexture("net_worth_bg_enemy", vector, this.lineSize, 0f, 1f);
						}
						renderer.DrawTexture(key.Name, vector, this.heroSize, 0f, 1f);
						renderer.DrawText(vector + new Vector2(this.heroSize.X + 5f, (this.lineSize.Y - this.textSize) / 5f), keyValuePair.Value.ToString("N0"), System.Drawing.Color.White, this.textSize, "Calibri");
						vector += new Vector2(0f, this.heroSize.Y + 1f);
					}
				}
			}
			catch (InvalidOperationException)
			{
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x0000AB14 File Offset: 0x00008D14
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion)
				{
					Meepo meepo;
					if ((meepo = (unit as Meepo)) == null || meepo.IsMainMeepo)
					{
						this.units[unit] = 0;
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x0000AB70 File Offset: 0x00008D70
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion)
				{
					this.units.Remove(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x0000ABB8 File Offset: 0x00008DB8
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				EntityManager9.AbilityAdded += this.OnAbilityAdded;
				EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.units.Clear();
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000AC9C File Offset: 0x00008E9C
		private void SizeOnValueChange(object sender, SliderEventArgs e)
		{
			this.heroSize = new Vector2((float)e.NewValue * 1.5f, (float)e.NewValue);
			this.lineSize = new Vector2((float)e.NewValue * 5.5f, (float)e.NewValue);
			this.textSize = (float)e.NewValue * 0.7f;
		}

		// Token: 0x040000BF RID: 191
		private readonly MenuSwitcher allies;

		// Token: 0x040000C0 RID: 192
		private readonly IContext9 context;

		// Token: 0x040000C1 RID: 193
		private readonly MenuSwitcher enemies;

		// Token: 0x040000C2 RID: 194
		private readonly MenuVectorSlider position;

		// Token: 0x040000C3 RID: 195
		private readonly MenuSwitcher show;

		// Token: 0x040000C4 RID: 196
		private readonly MenuSlider size;

		// Token: 0x040000C5 RID: 197
		private readonly Dictionary<Unit9, int> units = new Dictionary<Unit9, int>();

		// Token: 0x040000C6 RID: 198
		private Vector2 heroSize;

		// Token: 0x040000C7 RID: 199
		private Vector2 lineSize;

		// Token: 0x040000C8 RID: 200
		private Team ownerTeam;

		// Token: 0x040000C9 RID: 201
		private float textSize;
	}
}
