using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
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

namespace O9K.Hud.Modules.Units.Modifiers
{
	// Token: 0x02000016 RID: 22
	internal class Modifiers : IDisposable, IHudModule
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00006394 File Offset: 0x00004594
		[ImportingConstructor]
		public Modifiers(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.UnitsMenu.Add<Menu>(new Menu("Modifiers"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false)).SetTooltip("Show modifiers (buffs/debuffs)");
			this.showTime = menu.Add<MenuSwitcher>(new MenuSwitcher("Show remaining time", false, false));
			this.showAuras = menu.Add<MenuSwitcher>(new MenuSwitcher("Show auras", false, false));
			this.showAlly = menu.Add<MenuSwitcher>(new MenuSwitcher("Show ally modifiers", true, false));
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.position = new MenuVectorSlider(menu2, new Vector3(0f, -100f, 100f), new Vector3(0f, -100f, 100f));
			this.size = menu2.Add<MenuSlider>(new MenuSlider("Size", 30, 20, 50, false));
			this.textSize = menu2.Add<MenuSlider>(new MenuSlider("Time size", 16, 10, 35, false));
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000024CD File Offset: 0x000006CD
		public void Activate()
		{
			this.LoadTextures();
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00006590 File Offset: 0x00004790
		public void Dispose()
		{
			this.position.Dispose();
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00006620 File Offset: 0x00004820
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				Unit.OnModifierAdded += this.OnModifierAdded;
				Unit.OnModifierRemoved += this.OnModifierRemoved;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				UpdateManager.Subscribe(new Action(this.OnUpdate), 500, true);
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Unit.OnModifierRemoved -= this.OnModifierRemoved;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.context.Renderer.Draw -= this.OnDraw;
			this.units.Clear();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00006708 File Offset: 0x00004908
		private string ParseTextureName(string textureName)
		{
			string name;
			if (!this.textureNames.TryGetValue(textureName, out name))
			{
				name = textureName.Split(new char[]
				{
					'/'
				}).Last<string>();
				string value = this.trimModifierEnd.FirstOrDefault((string x) => name.EndsWith(x));
				if (!string.IsNullOrEmpty(value))
				{
					name = name.Substring(0, name.LastIndexOf(value, StringComparison.Ordinal));
				}
				this.textureNames[textureName] = name;
			}
			return name;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x000067A4 File Offset: 0x000049A4
		private void LoadTextures()
		{
			O9K.Core.Managers.Renderer.ITextureManager textureManager = this.context.Renderer.TextureManager;
			textureManager.LoadFromDota("modifier_bg", "panorama\\images\\masks\\softedge_circle_sharp_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.45f)));
			textureManager.LoadFromDota("outline_green", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0.9f, 0f, 1f)));
			textureManager.LoadFromDota("outline_red", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0.9f, 0f, 0f, 1f)));
			textureManager.LoadOutlineFromDota("outline_black", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 0, new Vector4?(new Vector4(0f, 0f, 0f, 1f)));
			textureManager.LoadFromDota("rune_arcane_rounded", "panorama\\images\\spellicons\\rune_arcane_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("rune_doubledamage_rounded", "panorama\\images\\spellicons\\rune_doubledamage_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("rune_haste_rounded", "panorama\\images\\spellicons\\rune_haste_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("rune_invis_rounded", "panorama\\images\\spellicons\\rune_invis_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("rune_regen_rounded", "panorama\\images\\spellicons\\rune_regen_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("tower_armor_aura_rounded", "panorama\\images\\spellicons\\tower_armor_aura_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("modifier_magicimmune_rounded", "panorama\\images\\spellicons\\modifier_magicimmune_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("modifier_invulnerable_rounded", "panorama\\images\\spellicons\\modifier_invulnerable_png.vtex_c", 0, 0, true, 0, null);
			textureManager.LoadFromDota("phantom_assassin_armor_corruption_debuff_rounded", "panorama\\images\\spellicons\\invoker_empty1_png.vtex_c", 0, 0, true, 0, null);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000698C File Offset: 0x00004B8C
		private void OnDraw(O9K.Core.Managers.Renderer.IRenderer renderer)
		{
			try
			{
				foreach (ModifierUnit modifierUnit in this.units)
				{
					if (modifierUnit.IsValid(this.showAlly))
					{
						Vector2 healthBarPosition = modifierUnit.HealthBarPosition;
						if (!healthBarPosition.IsZero)
						{
							Rectangle9 rec = new Rectangle9(healthBarPosition.X, healthBarPosition.Y + 55f, this.size, this.size) + this.position;
							foreach (DrawableModifier drawableModifier in modifierUnit.Modifiers)
							{
								renderer.DrawTexture(drawableModifier.TextureName, rec, 0f, 1f);
								if (!drawableModifier.IsAura)
								{
									float remainingTime = drawableModifier.RemainingTime;
									if (this.showTime)
									{
										Rectangle9 rec2 = rec * 1.5f;
										renderer.DrawTexture("modifier_bg", rec2, 0f, 1f);
										renderer.DrawText(rec2, (remainingTime < 10f) ? remainingTime.ToString("N1") : remainingTime.ToString("N0"), System.Drawing.Color.White, RendererFontFlags.Center | RendererFontFlags.VerticalCenter, this.textSize, "Calibri");
									}
									int num = (int)(100f - remainingTime / drawableModifier.Duration * 100f);
									Rectangle9 rec3 = rec * 1.17f;
									renderer.DrawTexture(drawableModifier.IsDebuff ? "outline_red" : "outline_green", rec3, 0f, 1f);
									renderer.DrawTexture("outline_black" + num, rec3, 0f, 1f);
								}
								else
								{
									renderer.DrawTexture(drawableModifier.IsDebuff ? "outline_red" : "outline_green", rec * 1.17f, 0f, 1f);
								}
								rec += new Vector2(0f, (float)(this.size + 5));
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

		// Token: 0x06000084 RID: 132 RVA: 0x00006C5C File Offset: 0x00004E5C
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				Modifier modifier = args.Modifier;
				if (modifier.IsValid && !modifier.IsHidden)
				{
					if (!this.ignoredModifiers.Contains(modifier.Name))
					{
						bool flag = modifier.Duration <= 0.5f;
						if (this.showAuras || !flag)
						{
							Unit9 modifierOwner = EntityManager9.GetUnit(sender.Handle);
							if (!(modifierOwner == null) && modifierOwner.IsHero && !modifierOwner.IsMyHero && !modifierOwner.IsIllusion)
							{
								ModifierUnit modifierUnit = this.units.Find((ModifierUnit x) => x.Unit.Handle == modifierOwner.Handle);
								if (modifierUnit == null)
								{
									modifierUnit = new ModifierUnit(modifierOwner);
									this.units.Add(modifierUnit);
								}
								string textureName = modifier.TextureName;
								if (!string.IsNullOrEmpty(textureName))
								{
									modifierUnit.AddModifier(new DrawableModifier(modifier, flag, this.ParseTextureName(textureName)));
								}
							}
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00006D9C File Offset: 0x00004F9C
		private void OnModifierRemoved(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				Unit9 modifierOwner = EntityManager9.GetUnit(sender.Handle);
				if (!(modifierOwner == null) && modifierOwner.IsHero && !modifierOwner.IsMyHero && !modifierOwner.IsIllusion)
				{
					ModifierUnit modifierUnit = this.units.Find((ModifierUnit x) => x.Unit.Handle == modifierOwner.Handle);
					if (modifierUnit != null)
					{
						modifierUnit.RemoveModifier(args.Modifier);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00006E40 File Offset: 0x00005040
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				ModifierUnit modifierUnit = this.units.Find((ModifierUnit x) => x.Unit.Handle == entity.Handle);
				if (modifierUnit != null)
				{
					this.units.Remove(modifierUnit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00006EA0 File Offset: 0x000050A0
		private void OnUpdate()
		{
			try
			{
				if (!Game.IsPaused)
				{
					foreach (ModifierUnit modifierUnit in this.units.ToList<ModifierUnit>())
					{
						if (!modifierUnit.Unit.IsValid)
						{
							this.units.Remove(modifierUnit);
						}
						else
						{
							modifierUnit.CheckModifiers();
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x04000048 RID: 72
		private readonly IContext9 context;

		// Token: 0x04000049 RID: 73
		private readonly MenuSwitcher enabled;

		// Token: 0x0400004A RID: 74
		private readonly HashSet<string> ignoredModifiers = new HashSet<string>
		{
			"modifier_fountain_aura_buff",
			"modifier_item_mekansm_noheal",
			"modifier_dragon_knight_frost_breath",
			"modifier_dragon_knight_splash_attack",
			"modifier_lina_fiery_soul",
			"modifier_templar_assassin_refraction_damage",
			"modifier_rubick_telekinesis"
		};

		// Token: 0x0400004B RID: 75
		private readonly MenuVectorSlider position;

		// Token: 0x0400004C RID: 76
		private readonly MenuSwitcher showAlly;

		// Token: 0x0400004D RID: 77
		private readonly MenuSwitcher showAuras;

		// Token: 0x0400004E RID: 78
		private readonly MenuSwitcher showTime;

		// Token: 0x0400004F RID: 79
		private readonly MenuSlider size;

		// Token: 0x04000050 RID: 80
		private readonly MenuSlider textSize;

		// Token: 0x04000051 RID: 81
		private readonly Dictionary<string, string> textureNames = new Dictionary<string, string>();

		// Token: 0x04000052 RID: 82
		private readonly HashSet<string> trimModifierEnd = new HashSet<string>
		{
			"_arcana",
			"_immortal",
			"_alt",
			"_alt1",
			"_alt2",
			"_axe_pw",
			"_tinker",
			"_ti8_crimson"
		};

		// Token: 0x04000053 RID: 83
		private readonly List<ModifierUnit> units = new List<ModifierUnit>();
	}
}
