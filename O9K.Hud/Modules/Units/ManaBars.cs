using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using Ensage;
using Ensage.SDK.Renderer;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.Units
{
	// Token: 0x02000008 RID: 8
	internal class ManaBars : IDisposable, IHudModule
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00004C80 File Offset: 0x00002E80
		[ImportingConstructor]
		public ManaBars(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.UnitsMenu.Add<Menu>(new Menu("Mana bars"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.showAmount = menu.Add<MenuSwitcher>(new MenuSwitcher("Show amount", false, false));
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.position = new MenuVectorSlider(menu2, new Vector3(0f, -50f, 50f), new Vector3(0f, -50f, 50f));
			this.size = new MenuVectorSlider(menu2, new MenuSlider("X size", "xSize", 0, -50, 50, false), new MenuSlider("Y size", "ySize", 0, -5, 15, false));
			this.manaTextSize = menu2.Add<MenuSlider>(new MenuSlider("Mana amount size", "manaSize", 16, 10, 35, false));
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002132 File Offset: 0x00000332
		public void Activate()
		{
			this.ownerTeam = EntityManager9.Owner.Team;
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00004D88 File Offset: 0x00002F88
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			this.position.Dispose();
			this.size.Dispose();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00004E00 File Offset: 0x00003000
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00004E94 File Offset: 0x00003094
		private void OnDraw(O9K.Core.Managers.Renderer.IRenderer renderer)
		{
			try
			{
				foreach (Unit9 unit in this.units)
				{
					if (unit.IsValid && unit.IsVisible && unit.IsAlive)
					{
						Vector2 healthBarPosition = unit.HealthBarPosition;
						if (!healthBarPosition.IsZero)
						{
							Vector2 healthBarSize = unit.HealthBarSize;
							Vector2 location = healthBarPosition + new Vector2(0f, healthBarSize.Y + 1f) + this.position;
							Vector2 vector = new Vector2(healthBarSize.X, healthBarSize.Y * 0.53f) + this.size;
							Rectangle9 rec = new Rectangle9(location, vector.X, vector.Y) + 1.5f;
							Rectangle9 rec2 = new Rectangle9(location, vector.X * unit.ManaPercentageBase, vector.Y);
							renderer.DrawFilledRectangle(rec, System.Drawing.Color.Black);
							renderer.DrawFilledRectangle(rec2, System.Drawing.Color.FromArgb(25, 54, 255));
							if (this.showAmount)
							{
								renderer.DrawText(rec + 5f, unit.Mana.ToString("####"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, this.manaTextSize, "Calibri");
							}
						}
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

		// Token: 0x06000026 RID: 38 RVA: 0x0000507C File Offset: 0x0000327C
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion && unit.Team != this.ownerTeam)
				{
					this.units.Add(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000050D0 File Offset: 0x000032D0
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion && unit.Team != this.ownerTeam)
				{
					this.units.Remove(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400000C RID: 12
		private readonly IContext9 context;

		// Token: 0x0400000D RID: 13
		private readonly MenuSwitcher enabled;

		// Token: 0x0400000E RID: 14
		private readonly MenuSlider manaTextSize;

		// Token: 0x0400000F RID: 15
		private readonly MenuVectorSlider position;

		// Token: 0x04000010 RID: 16
		private readonly MenuSwitcher showAmount;

		// Token: 0x04000011 RID: 17
		private readonly MenuVectorSlider size;

		// Token: 0x04000012 RID: 18
		private readonly List<Unit9> units = new List<Unit9>();

		// Token: 0x04000013 RID: 19
		private Team ownerTeam;
	}
}
