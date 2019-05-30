using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Windows.Input;
using Ensage;
using Ensage.SDK.Renderer;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
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

namespace O9K.Hud.Modules.Screen.ItemPanel
{
	// Token: 0x02000032 RID: 50
	internal class ItemPanel : IDisposable, IHudModule
	{
		// Token: 0x0600011A RID: 282 RVA: 0x0000ACFC File Offset: 0x00008EFC
		[ImportingConstructor]
		public ItemPanel(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.ScreenMenu.Add<Menu>(new Menu("Item panel"));
			this.show = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", "enabled", false, false)).SetTooltip("Show enemy items");
			this.showCooldown = menu.Add<MenuSwitcher>(new MenuSwitcher("Show cooldowns", "cooldown", true, false));
			this.showCharges = menu.Add<MenuSwitcher>(new MenuSwitcher("Show charges", "charges", true, false));
			this.toggleKey = menu.Add<MenuToggleKey>(new MenuToggleKey("Toggle key", "toggle", Key.None, true, false)).SetTooltip("Show/hide items panel");
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.size = menu2.Add<MenuSlider>(new MenuSlider("Size", "size", 35, 20, 60, false));
			this.position = new MenuVectorSlider(menu2, new Vector2 (O9K.Core.Helpers.Hud.Info.ScreenSize.X * 0.71f, O9K.Core.Helpers.Hud.Info.ScreenSize.Y * 0.82f));
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000AE24 File Offset: 0x00009024
		public void Activate()
		{
			this.LoadTextures();
			this.ownerTeam = EntityManager9.Owner.Team;
			this.show.ValueChange += this.ShowOnValueChange;
			this.size.ValueChange += this.SizeOnValueChange;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000AE78 File Offset: 0x00009078
		public void Dispose()
		{
			this.size.ValueChange -= this.SizeOnValueChange;
			this.show.ValueChange -= this.ShowOnValueChange;
			this.toggleKey.ValueChange -= this.ToggleKeyOnValueChange;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.units.Clear();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000AF14 File Offset: 0x00009114
		private void LoadTextures()
		{
			O9K.Core.Managers.Renderer.ITextureManager textureManager = this.context.Renderer.TextureManager;
			textureManager.LoadFromDota("inventory_item_bg", "panorama\\images\\hud\\reborn\\inventory_item_well_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("inventory_tp_cd_bg", "panorama\\images\\masks\\softedge_circle_sharp_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.8f)));
			textureManager.LoadFromDota("inventory_item_cd_bg", "panorama\\images\\masks\\softedge_horizontal_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.6f)));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000AFB4 File Offset: 0x000091B4
		private void OnDraw(O9K.Core.Managers.Renderer.IRenderer renderer)
		{
			try
			{
				Vector2 vector = this.position.Value;
				foreach (Unit9 unit in this.units)
				{
					if (unit.IsValid)
					{
						renderer.DrawTexture(unit.Name, vector, this.heroSize, 0f, 1f);
						Rectangle9 rec = new Rectangle9(vector.X + this.heroSize.X + 1f, vector.Y, this.itemSize.X, this.itemSize.Y);
						for (int i = 0; i < 6; i++)
						{
							renderer.DrawTexture("inventory_item_bg", rec + new Vector2((this.itemSize.X + 1f) * (float)i, 0f), 0f, 1f);
						}
						foreach (Ability9 ability in from x in unit.Abilities
						orderby x.Id == AbilityId.item_tpscroll
						select x)
						{
							if (ability.Id == AbilityId.item_tpscroll)
							{
								Rectangle9 rec2 = new Rectangle9(vector + new Vector2(this.heroSize.X * 0.7f, this.heroSize.Y * 0.35f), this.itemSize.X * 0.55f, this.itemSize.X * 0.55f);
								renderer.DrawTexture("outline", rec2 + 1f, 0f, 1f);
								renderer.DrawTexture(ability.Name + "_rounded", rec2, 0f, 1f);
								if (this.showCooldown)
								{
									float remainingCooldown = ability.RemainingCooldown;
									if (remainingCooldown > 0f)
									{
										renderer.DrawTexture("inventory_tp_cd_bg", rec2, 0f, 1f);
										renderer.DrawText(rec2, Math.Ceiling((double)remainingCooldown).ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, this.size * 0.35f, "Calibri");
									}
								}
							}
							else if (ability.IsItem && ability.IsUsable)
							{
								Rectangle9 rec3 = rec - 4f;
								renderer.DrawTexture(ability.Name, rec3, 0f, 1f);
								if (this.showCharges && ability.IsDisplayingCharges)
								{
									string text = ability.BaseItem.CurrentCharges.ToString("N0");
									Vector2 vector2 = renderer.MeasureText(text, this.size * 0.35f, "Calibri");
									Rectangle9 rec4 = rec3.SinkToBottomRight(vector2.X * 1.1f, vector2.Y * 0.8f);
									renderer.DrawFilledRectangle(rec4, System.Drawing.Color.Black);
									renderer.DrawText(rec4, text, System.Drawing.Color.White, RendererFontFlags.Left, this.size * 0.35f, "Calibri");
								}
								if (this.showCooldown)
								{
									float remainingCooldown2 = ability.RemainingCooldown;
									if (remainingCooldown2 > 0f)
									{
										renderer.DrawTexture("inventory_item_cd_bg", rec3, 0f, 1f);
										renderer.DrawText(rec3, Math.Ceiling((double)remainingCooldown2).ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, this.size * 0.7f, "Calibri");
									}
								}
								rec += new Vector2(this.itemSize.X + 1f, 0f);
							}
						}
						vector += new Vector2(0f, (float)(this.size + 1));
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

		// Token: 0x0600011F RID: 287 RVA: 0x0000B46C File Offset: 0x0000966C
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.IsHero && !unit.IsIllusion && unit.Team != this.ownerTeam)
				{
					Meepo meepo;
					if ((meepo = (unit as Meepo)) == null || meepo.IsMainMeepo)
					{
						this.units.Add(unit);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000B4D4 File Offset: 0x000096D4
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

		// Token: 0x06000121 RID: 289 RVA: 0x0000B528 File Offset: 0x00009728
		private void ShowOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				this.toggleKey.ValueChange += this.ToggleKeyOnValueChange;
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.toggleKey.ValueChange -= this.ToggleKeyOnValueChange;
			this.context.Renderer.Draw -= this.OnDraw;
			this.units.Clear();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00002ACC File Offset: 0x00000CCC
		private void SizeOnValueChange(object sender, SliderEventArgs e)
		{
			this.heroSize = new Vector2((float)e.NewValue * 1.6f, (float)e.NewValue);
			this.itemSize = new Vector2((float)e.NewValue * 1.2f, (float)e.NewValue);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000B5D8 File Offset: 0x000097D8
		private void ToggleKeyOnValueChange(object sender, KeyEventArgs e)
		{
			if (e.NewValue)
			{
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x040000CC RID: 204
		private readonly IContext9 context;

		// Token: 0x040000CD RID: 205
		private readonly MenuVectorSlider position;

		// Token: 0x040000CE RID: 206
		private readonly MenuSwitcher show;

		// Token: 0x040000CF RID: 207
		private readonly MenuSwitcher showCharges;

		// Token: 0x040000D0 RID: 208
		private readonly MenuSwitcher showCooldown;

		// Token: 0x040000D1 RID: 209
		private readonly MenuSlider size;

		// Token: 0x040000D2 RID: 210
		private readonly MenuToggleKey toggleKey;

		// Token: 0x040000D3 RID: 211
		private readonly List<Unit9> units = new List<Unit9>();

		// Token: 0x040000D4 RID: 212
		private Vector2 heroSize;

		// Token: 0x040000D5 RID: 213
		private Vector2 itemSize;

		// Token: 0x040000D6 RID: 214
		private Team ownerTeam;
	}
}
