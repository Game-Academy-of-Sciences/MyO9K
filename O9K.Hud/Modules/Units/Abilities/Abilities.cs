using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using O9K.Hud.Modules.Units.Abilities.HudEntities.Abilities;
using O9K.Hud.Modules.Units.Abilities.HudEntities.Units;
using SharpDX;

namespace O9K.Hud.Modules.Units.Abilities
{
	// Token: 0x0200001E RID: 30
	internal class Abilities : IDisposable, IHudModule
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00007038 File Offset: 0x00005238
		[ImportingConstructor]
		public Abilities(IContext9 context, IHudMenu hudMenu)
		{
			this.context = context;
			Menu menu = hudMenu.UnitsMenu.Add<Menu>(new Menu("Abilities"));
			this.abilitiesEnabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.abilitiesShowAlly = menu.Add<MenuSwitcher>(new MenuSwitcher("Show ally abilities", false, false));
			Menu menu2 = menu.Add<Menu>(new Menu("Settings"));
			this.abilitiesPosition = new MenuVectorSlider(menu2, new Vector3(0f, -250f, 250f), new Vector3(0f, -250f, 250f));
			this.abilitiesSize = menu2.Add<MenuSlider>(new MenuSlider("Size", 25, 20, 50, false));
			this.abilitiesTextSize = menu2.Add<MenuSlider>(new MenuSlider("Cooldown size", 16, 10, 35, false));
			Menu menu3 = hudMenu.UnitsMenu.Add<Menu>(new Menu("Items"));
			this.itemsEnabled = menu3.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false));
			this.itemsShowAlly = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show ally items", false, false));
			Menu menu4 = menu3.Add<Menu>(new Menu("Settings"));
			this.itemsPosition = new MenuVectorSlider(menu4, new Vector3(0f, -250f, 250f), new Vector3(0f, -250f, 250f));
			this.itemsSize = menu4.Add<MenuSlider>(new MenuSlider("Size", 25, 20, 50, false));
			this.itemsTextSize = menu4.Add<MenuSlider>(new MenuSlider("Cooldown size", 16, 10, 35, false));
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000071E8 File Offset: 0x000053E8
		public void Activate()
		{
			this.LoadTextures();
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
			EntityManager9.UnitRemoved += this.OnUnitRemoved;
			this.context.Renderer.Draw += this.OnDraw;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000724C File Offset: 0x0000544C
		public void Dispose()
		{
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			this.context.Renderer.Draw -= this.OnDraw;
			this.abilitiesPosition.Dispose();
			this.itemsPosition.Dispose();
			this.units.Clear();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000072CC File Offset: 0x000054CC
		private void LoadTextures()
		{
			ITextureManager textureManager = this.context.Renderer.TextureManager;
			textureManager.LoadFromDota("tusk_launch_snowball", "panorama\\images\\spellicons\\tusk_snowball_png.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("ability_cd_bg", "panorama\\images\\masks\\softedge_horizontal_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.6f)));
			textureManager.LoadFromDota("ability_0lvl_bg", "panorama\\images\\masks\\softedge_horizontal_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0.3f, 0.3f, 0.3f, 0.4f)));
			textureManager.LoadFromDota("ability_lvl_bg", "panorama\\images\\masks\\softedge_horizontal_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.9f)));
			textureManager.LoadFromDota("ability_mana_bg", "panorama\\images\\masks\\softedge_horizontal_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0.9f, 0.8f)));
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000073D0 File Offset: 0x000055D0
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (!ability.IsTalent)
				{
					Unit9 abilityOwner = ability.Owner;
					if (abilityOwner.CanUseAbilities && abilityOwner.IsHero && !abilityOwner.IsMyHero)
					{
						if ((abilityOwner.UnitState & UnitState.CommandRestricted) == (UnitState)0UL)
						{
							HudUnit hudUnit = this.units.Find((HudUnit x) => x.Unit.Handle == abilityOwner.Handle);
							if (hudUnit == null)
							{
								hudUnit = new HudUnit(abilityOwner);
								this.units.Add(hudUnit);
							}
							hudUnit.AddAbility(ability);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00007490 File Offset: 0x00005690
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				Unit9 abilityOwner = ability.Owner;
				if (abilityOwner.CanUseAbilities && abilityOwner.IsHero && !abilityOwner.IsMyHero)
				{
					HudUnit hudUnit = this.units.Find((HudUnit x) => x.Unit.Handle == abilityOwner.Handle);
					if (hudUnit != null)
					{
						hudUnit.RemoveAbility(ability);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00007518 File Offset: 0x00005718
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (HudUnit hudUnit in this.units)
				{
					if (hudUnit.IsValid)
					{
						Vector2 healthBarPosition = hudUnit.HealthBarPosition;
						if (!healthBarPosition.IsZero)
						{
							Vector2 healthBarSize = hudUnit.HealthBarSize;
							if (this.abilitiesEnabled && (!hudUnit.IsAlly || this.abilitiesShowAlly))
							{
								HudAbility[] array = hudUnit.Abilities.ToArray<HudAbility>();
								Vector2 vector = new Vector2(healthBarPosition.X + healthBarSize.X * 0.5f, healthBarPosition.Y - this.abilitiesSize) + this.abilitiesPosition - new Vector2((float)(this.abilitiesSize * array.Length) / 2f, 0f);
								for (int i = 0; i < array.Length; i++)
								{
									array[i].Draw(renderer, new Rectangle9(vector + new Vector2((float)(i * this.abilitiesSize), 0f), this.abilitiesSize, this.abilitiesSize), this.abilitiesTextSize);
								}
							}
							if (this.itemsEnabled && (!hudUnit.IsAlly || this.itemsShowAlly))
							{
								HudAbility[] array2 = hudUnit.Items.ToArray<HudAbility>();
								Vector2 vector2 = new Vector2(healthBarPosition.X + healthBarSize.X * 0.5f, healthBarPosition.Y + 18f) + this.itemsPosition - new Vector2((float)(this.itemsSize * array2.Length) / 2f, 0f);
								for (int j = 0; j < array2.Length; j++)
								{
									array2[j].Draw(renderer, new Rectangle9(vector2 + new Vector2((float)(j * this.itemsSize), 0f), this.itemsSize, this.itemsSize), this.itemsTextSize);
								}
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

		// Token: 0x060000A5 RID: 165 RVA: 0x000077C8 File Offset: 0x000059C8
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				HudUnit hudUnit = this.units.Find((HudUnit x) => x.Unit.Handle == entity.Handle);
				if (hudUnit != null)
				{
					this.units.Remove(hudUnit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400005E RID: 94
		private readonly MenuSwitcher abilitiesEnabled;

		// Token: 0x0400005F RID: 95
		private readonly MenuVectorSlider abilitiesPosition;

		// Token: 0x04000060 RID: 96
		private readonly MenuSwitcher abilitiesShowAlly;

		// Token: 0x04000061 RID: 97
		private readonly MenuSlider abilitiesSize;

		// Token: 0x04000062 RID: 98
		private readonly MenuSlider abilitiesTextSize;

		// Token: 0x04000063 RID: 99
		private readonly IContext9 context;

		// Token: 0x04000064 RID: 100
		private readonly MenuSwitcher itemsEnabled;

		// Token: 0x04000065 RID: 101
		private readonly MenuVectorSlider itemsPosition;

		// Token: 0x04000066 RID: 102
		private readonly MenuSwitcher itemsShowAlly;

		// Token: 0x04000067 RID: 103
		private readonly MenuSlider itemsSize;

		// Token: 0x04000068 RID: 104
		private readonly MenuSlider itemsTextSize;

		// Token: 0x04000069 RID: 105
		private readonly List<HudUnit> units = new List<HudUnit>();
	}
}
