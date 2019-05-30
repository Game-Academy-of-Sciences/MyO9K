using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Ensage;
using Ensage.SDK.Extensions;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Heroes.Unique;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Core.Managers.Renderer.Utils;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using SharpDX;

namespace O9K.Hud.Modules.TopPanel.Status
{
	// Token: 0x02000027 RID: 39
	internal class HeroStatus : IDisposable, IHudModule
	{
		// Token: 0x060000CA RID: 202 RVA: 0x000081A0 File Offset: 0x000063A0
		[ImportingConstructor]
		public HeroStatus(IContext9 context, ITopPanel topPanel, IHudMenu hudMenu)
		{
			this.context = context;
			this.topPanel = topPanel;
			Menu menu = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Health"));
			this.showEnemyHealth = menu.Add<MenuSwitcher>(new MenuSwitcher("Show enemy health", true, false));
			this.showAllyHealth = menu.Add<MenuSwitcher>(new MenuSwitcher("Show ally health", true, false));
			Menu menu2 = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Mana"));
			this.showEnemyMana = menu2.Add<MenuSwitcher>(new MenuSwitcher("Show enemy mana", true, false));
			this.showAllyMana = menu2.Add<MenuSwitcher>(new MenuSwitcher("Show ally mana", true, false));
			Menu menu3 = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Ultimate"));
			this.showEnemyUlt = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show enemy ultimate", true, false));
			this.showAllyUlt = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show ally ultimate", true, false));
			this.showUltCd = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show ultimate cooldown", true, false));
			this.showUltCdTime = menu3.Add<MenuSwitcher>(new MenuSwitcher("Show ultimate cooldown time", false, false));
			Menu menu4 = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Items"));
			this.showEnemyItems = menu4.Add<MenuSwitcher>(new MenuSwitcher("Show enemy items", true, false)).SetTooltip("Show important enemy items");
			this.showAllyItems = menu4.Add<MenuSwitcher>(new MenuSwitcher("Show ally items", true, false)).SetTooltip("Show important ally items");
			Menu menu5 = hudMenu.TopPanelMenu.Add<Menu>(new Menu("Visibility"));
			this.showFowTime = menu5.Add<MenuSwitcher>(new MenuSwitcher("Show time in fow", true, false)).SetTooltip("Show how many seconds enemy is in fog of war");
		}

		// Token: 0x060000CB RID: 203 RVA: 0x0000841C File Offset: 0x0000661C
		public void Activate()
		{
			this.LoadTextures();
			EntityManager9.UnitAdded += this.OnUnitAdded;
			EntityManager9.AbilityAdded += this.OnAbilityAdded;
			EntityManager9.AbilityRemoved += this.OnAbilityRemoved;
			Unit.OnModifierAdded += this.OnModifierAdded;
			Game.OnFireEvent += this.OnFireEvent;
			this.context.Renderer.Draw += this.OnDraw;
		}

		// Token: 0x060000CC RID: 204 RVA: 0x000084A0 File Offset: 0x000066A0
		public void Dispose()
		{
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.AbilityAdded -= this.OnAbilityAdded;
			EntityManager9.AbilityRemoved -= this.OnAbilityRemoved;
			Unit.OnModifierAdded -= this.OnModifierAdded;
			Game.OnFireEvent -= this.OnFireEvent;
			this.context.Renderer.Draw -= this.OnDraw;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00008520 File Offset: 0x00006720
		private void LoadTextures()
		{
			ITextureManager textureManager = this.context.Renderer.TextureManager;
			textureManager.LoadFromDota("health_ally", "panorama\\images\\hud\\reborn\\topbar_health_psd.vtex_c", 256, 12, false, -60, null);
			textureManager.LoadFromDota("health_ally_visible", "panorama\\images\\hud\\reborn\\topbar_health_psd.vtex_c", 256, 12, false, 0, null);
			textureManager.LoadFromDota("health_ally_bg", "panorama\\images\\hud\\reborn\\topbar_health_psd.vtex_c", 256, 12, false, -170, null);
			textureManager.LoadFromDota("health_enemy", "panorama\\images\\hud\\reborn\\topbar_health_dire_psd.vtex_c", 256, 12, false, 0, null);
			textureManager.LoadFromDota("health_enemy_invis", "panorama\\images\\hud\\reborn\\topbar_health_dire_psd.vtex_c", 256, 12, false, -60, null);
			textureManager.LoadFromDota("health_enemy_bg", "panorama\\images\\hud\\reborn\\topbar_health_dire_psd.vtex_c", 256, 12, false, -170, null);
			textureManager.LoadFromDota("mana", "panorama\\images\\hud\\reborn\\topbar_mana_psd.vtex_c", 256, 12, false, 0, null);
			textureManager.LoadFromDota("mana_invis", "panorama\\images\\hud\\reborn\\topbar_mana_psd.vtex_c", 256, 12, false, -60, null);
			textureManager.LoadFromDota("mana_bg", "panorama\\images\\hud\\reborn\\topbar_mana_psd.vtex_c", 256, 12, false, -170, null);
			textureManager.LoadFromDota("ult_rdy", "panorama\\images\\hud\\reborn\\ult_ready_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("ult_cd", "panorama\\images\\hud\\reborn\\ult_cooldown_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("ult_mp", "panorama\\images\\hud\\reborn\\ult_no_mana_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadFromDota("buyback", "panorama\\images\\hud\\reborn\\buyback_header_psd.vtex_c", 0, 0, false, -60, null);
			textureManager.LoadFromDota("top_ult_cd_bg", "panorama\\images\\masks\\softedge_circle_sharp_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0f, 0f, 0f, 0.5f)));
			textureManager.LoadFromDota("outline", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 0, 0, false, 0, null);
			textureManager.LoadOutlineFromDota("outline_green_pct", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 0, new Vector4?(new Vector4(0f, 1f, 0f, 1f)));
			textureManager.LoadOutlineFromDota("outline_blue_pct", "panorama\\images\\hud\\reborn\\buff_outline_psd.vtex_c", 40, new Vector4?(new Vector4(0f, 0.4f, 0.9f, 1f)));
			foreach (AbilityId abilityId in this.drawItems)
			{
				textureManager.LoadFromDota(abilityId, true);
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x000087EC File Offset: 0x000069EC
		private void OnAbilityAdded(Ability9 ability)
		{
			try
			{
				if (!ability.Owner.IsIllusion)
				{
					if (ability.IsItem)
					{
						if (this.drawItems.Contains(ability.Id))
						{
							int purchaserId = ability.BaseItem.PurchaserId;
							if (purchaserId != -1)
							{
								TopPanelUnit topPanelUnit = this.units[purchaserId];
								if (topPanelUnit != null)
								{
									topPanelUnit.AddItem(ability);
								}
							}
						}
					}
					else if (ability.IsUltimate && !ability.IsStolen)
					{
						foreach (TopPanelUnit topPanelUnit2 in this.units)
						{
							if (topPanelUnit2 != null)
							{
								topPanelUnit2.SetUltimate(ability);
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

		// Token: 0x060000CF RID: 207 RVA: 0x000088A0 File Offset: 0x00006AA0
		private void OnAbilityRemoved(Ability9 ability)
		{
			try
			{
				if (ability.IsItem && !ability.Owner.IsIllusion && this.drawItems.Contains(ability.Id))
				{
					int purchaserId = ability.BaseItem.PurchaserId;
					if (purchaserId != -1)
					{
						TopPanelUnit topPanelUnit = this.units[purchaserId];
						if (topPanelUnit != null)
						{
							topPanelUnit.RemoveItem(ability);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00008918 File Offset: 0x00006B18
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				for (int i = 0; i < this.units.Length; i++)
				{
					TopPanelUnit topPanelUnit = this.units[i];
					if (topPanelUnit != null && topPanelUnit.IsValid)
					{
						float screenRatio = O9K.Core.Helpers.Hud.Info.ScreenRatio;
						topPanelUnit.DrawRunes(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7f * screenRatio, 0f));
						if (topPanelUnit.IsAlly)
						{
							float num = 0f;
							if (this.showAllyHealth)
							{
								topPanelUnit.DrawAllyHealth(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7f * screenRatio, 0f));
								num += 7f * screenRatio;
							}
							if (this.showAllyMana)
							{
								topPanelUnit.DrawAllyMana(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7f * screenRatio, num));
								num += 7f * screenRatio;
							}
							if (this.showAllyUlt && topPanelUnit.DrawUltimate(renderer, this.topPanel.GetPlayersUltimatePosition(i, 16f * screenRatio, 0f), this.showUltCd ? this.topPanel.GetPlayersUltimatePosition(i, 42f * screenRatio, num + 28f * screenRatio) : Rectangle9.Zero, this.showUltCdTime))
							{
								num += 50f * screenRatio;
							}
							if (this.showAllyItems)
							{
								topPanelUnit.DrawItems(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 15f * screenRatio, num + 5f * screenRatio));
							}
						}
						else
						{
							topPanelUnit.DrawBuyback(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 28f * screenRatio, 0f));
							if (this.showFowTime)
							{
								topPanelUnit.DrawFowTime(renderer, this.topPanel.GetPlayerPosition(i));
							}
							float num2 = 0f;
							if (this.showEnemyHealth)
							{
								topPanelUnit.DrawEnemyHealth(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7f * screenRatio, 0f));
								num2 += 7f * screenRatio;
							}
							if (this.showEnemyMana)
							{
								topPanelUnit.DrawEnemyMana(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 7f * screenRatio, num2));
								num2 += 7f * screenRatio;
							}
							if (this.showEnemyUlt && topPanelUnit.DrawUltimate(renderer, this.topPanel.GetPlayersUltimatePosition(i, 16f * screenRatio, 0f), this.showUltCd ? this.topPanel.GetPlayersUltimatePosition(i, 42f * screenRatio, num2 + 28f * screenRatio) : Rectangle9.Zero, this.showUltCdTime))
							{
								num2 += 50f * screenRatio;
							}
							if (this.showEnemyItems)
							{
								topPanelUnit.DrawItems(renderer, this.topPanel.GetPlayersHealthBarPosition(i, 15f * screenRatio, num2 + 5f * screenRatio));
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

		// Token: 0x060000D1 RID: 209 RVA: 0x00008C78 File Offset: 0x00006E78
		private void OnFireEvent(FireEventEventArgs args)
		{
			if (args.GameEvent.Name != "dota_buyback")
			{
				return;
			}
			try
			{
				int @int = args.GameEvent.GetInt("player_id");
				TopPanelUnit topPanelUnit = this.units[@int];
				if (topPanelUnit != null)
				{
					topPanelUnit.BuybackSleeper.Sleep(480f);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00008CE8 File Offset: 0x00006EE8
		private void OnModifierAdded(Unit sender, ModifierChangedEventArgs args)
		{
			try
			{
				Modifier modifier = args.Modifier;
				if (this.runeModifiers.Contains(modifier.Name))
				{
					TopPanelUnit topPanelUnit = this.units.Find((TopPanelUnit x) => ((x != null) ? new uint?(x.Handle) : null) == sender.Handle);
					if (topPanelUnit != null)
					{
						topPanelUnit.AddModifier(modifier);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00008D5C File Offset: 0x00006F5C
		private void OnUnitAdded(Unit9 hero)
		{
			try
			{
				if (hero.IsHero && !hero.IsIllusion)
				{
					Meepo meepo;
					if ((meepo = (hero as Meepo)) == null || meepo.IsMainMeepo)
					{
						Player player;
						if ((player = (hero.BaseOwner as Player)) != null)
						{
							this.units[player.Id] = new TopPanelUnit(hero);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0400007E RID: 126
		private readonly IContext9 context;

		// Token: 0x0400007F RID: 127
		private readonly HashSet<AbilityId> drawItems = new HashSet<AbilityId>
		{
			AbilityId.item_gem,
			AbilityId.item_dust,
			AbilityId.item_rapier,
			AbilityId.item_aegis,
			AbilityId.item_cheese,
			AbilityId.item_refresher_shard,
			AbilityId.item_smoke_of_deceit,
			AbilityId.item_ward_sentry,
			AbilityId.item_ward_observer,
			AbilityId.item_ward_dispenser
		};

		// Token: 0x04000080 RID: 128
		private readonly HashSet<string> runeModifiers = new HashSet<string>
		{
			"modifier_rune_arcane",
			"modifier_rune_doubledamage",
			"modifier_rune_haste",
			"modifier_rune_invis",
			"modifier_rune_regen"
		};

		// Token: 0x04000081 RID: 129
		private readonly MenuSwitcher showAllyHealth;

		// Token: 0x04000082 RID: 130
		private readonly MenuSwitcher showAllyItems;

		// Token: 0x04000083 RID: 131
		private readonly MenuSwitcher showAllyMana;

		// Token: 0x04000084 RID: 132
		private readonly MenuSwitcher showAllyUlt;

		// Token: 0x04000085 RID: 133
		private readonly MenuSwitcher showEnemyHealth;

		// Token: 0x04000086 RID: 134
		private readonly MenuSwitcher showEnemyItems;

		// Token: 0x04000087 RID: 135
		private readonly MenuSwitcher showEnemyMana;

		// Token: 0x04000088 RID: 136
		private readonly MenuSwitcher showEnemyUlt;

		// Token: 0x04000089 RID: 137
		private readonly MenuSwitcher showFowTime;

		// Token: 0x0400008A RID: 138
		private readonly MenuSwitcher showUltCd;

		// Token: 0x0400008B RID: 139
		private readonly MenuSwitcher showUltCdTime;

		// Token: 0x0400008C RID: 140
		private readonly ITopPanel topPanel;

		// Token: 0x0400008D RID: 141
		private readonly TopPanelUnit[] units = new TopPanelUnit[10];
	}
}
