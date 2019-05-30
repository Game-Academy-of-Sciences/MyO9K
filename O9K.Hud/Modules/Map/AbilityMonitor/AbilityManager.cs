using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Ensage;
using Ensage.SDK.Geometry;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Context;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Core.Managers.Renderer;
using O9K.Hud.Core;
using O9K.Hud.Helpers;
using O9K.Hud.Helpers.Notificator;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Base;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data;
using O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.UniqueAbilities.Wards;
using SharpDX;

namespace O9K.Hud.Modules.Map.AbilityMonitor
{
	// Token: 0x02000063 RID: 99
	internal class AbilityManager : IDisposable, IHudModule
	{
		// Token: 0x0600022B RID: 555 RVA: 0x00011490 File Offset: 0x0000F690
		[ImportingConstructor]
		public AbilityManager(IContext9 context, IMinimap minimap, INotificator notificator, IHudMenu hudMenu)
		{
			this.context = context;
			this.minimap = minimap;
			this.notificator = notificator;
			Menu menu = hudMenu.MapMenu.Add<Menu>(new Menu("Abilities"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false)).SetTooltip("Show used enemy abilities in fog");
			this.showOnMinimap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on minimap", true, false)).SetTooltip("Show on minimap");
			this.showOnMap = menu.Add<MenuSwitcher>(new MenuSwitcher("Show on map", true, false)).SetTooltip("Shows on map");
			this.notificationsEnabled = hudMenu.NotificationsMenu.GetOrAdd<Menu>(new Menu("Abilities")).GetOrAdd<MenuSwitcher>(new MenuSwitcher("Enabled", true, false)).SetTooltip("Notify about used dangerous abilities");
		}

		// Token: 0x0600022C RID: 556 RVA: 0x00003647 File Offset: 0x00001847
		public void Activate()
		{
			this.LoadTextures();
			this.allyTeam = EntityManager9.Owner.Team;
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00011590 File Offset: 0x0000F790
		public void Dispose()
		{
			this.context.Renderer.Draw -= this.OnDraw;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdateRemove));
			UpdateManager.Unsubscribe(new Action(this.OnUpdateWard));
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;
			this.enabled.ValueChange -= this.EnabledOnValueChange;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00011648 File Offset: 0x0000F848
		private void AddWard(EnemyHero enemy, string unitName)
		{
			AbilityFullData abilityFullData;
			if (!this.abilityData.Units.TryGetValue(unitName, out abilityFullData))
			{
				return;
			}
			Vector3 wardPosition = enemy.Unit.InFront(400f, 0f, true);
			if (this.drawableAbilities.OfType<DrawableWardAbility>().Any((DrawableWardAbility x) => x.Unit != null && x.AbilityUnitName == unitName && x.Position.Distance2D(wardPosition, false) < 400f))
			{
				return;
			}
			if (this.drawableAbilities.OfType<DrawableWardAbility>().Any((DrawableWardAbility x) => x.Position.Distance2D(wardPosition, false) <= 50f))
			{
				wardPosition += new Vector3(60f, 0f, 0f);
			}
			((WardAbilityData)abilityFullData).AddDrawableAbility(this.drawableAbilities, wardPosition);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00011714 File Offset: 0x0000F914
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				if (e.OldValue)
				{
					if (AppDomain.CurrentDomain.GetAssemblies().Any((Assembly x) => !x.GlobalAssemblyCache && x.GetName().Name.Contains("VisionControl")))
					{
						O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // VisionControl is already included in O9K.Hud", 10f);
					}
					if (AppDomain.CurrentDomain.GetAssemblies().Any((Assembly x) => !x.GlobalAssemblyCache && x.GetName().Name.Contains("VisionControl")))
					{
						O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // BeAware is already included in O9K.Hud", 10f);
					}
				}
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				UpdateManager.Subscribe(new Action(this.OnUpdateRemove), 500, true);
				UpdateManager.Subscribe(new Action(this.OnUpdateWard), 200, true);
				Entity.OnParticleEffectAdded += this.OnParticleEffectAdded;
				ObjectManager.OnAddEntity += this.OnAddEntity;
				ObjectManager.OnRemoveEntity += this.OnRemoveEntity;
				this.context.Renderer.Draw += this.OnDraw;
				return;
			}
			this.context.Renderer.Draw -= this.OnDraw;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			Entity.OnParticleEffectAdded -= this.OnParticleEffectAdded;
			ObjectManager.OnAddEntity -= this.OnAddEntity;
			ObjectManager.OnRemoveEntity -= this.OnRemoveEntity;
			UpdateManager.Unsubscribe(new Action(this.OnUpdateRemove));
			UpdateManager.Unsubscribe(new Action(this.OnUpdateWard));
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000118E8 File Offset: 0x0000FAE8
		private bool GaveWard(EnemyHero enemy)
		{
			return this.enemyHeroes.Any((EnemyHero x) => x.Unit.IsValid && !x.Equals(enemy) && x.Unit.IsVisible && x.Unit.IsAlive && x.Unit.Distance(enemy.Unit) <= 600f && x.ObserversCount + x.SentryCount < x.CountWards(AbilityId.item_ward_observer) + x.CountWards(AbilityId.item_ward_sentry));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0001191C File Offset: 0x0000FB1C
		private void LoadTextures()
		{
			this.context.Renderer.TextureManager.LoadFromDota("minimap_item_ward_observer_rounded", "panorama\\images\\hero_selection\\minimap_ward_obs_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(1f, 0.6f, 0f, 1f)));
			this.context.Renderer.TextureManager.LoadFromDota("minimap_item_ward_sentry_rounded", "panorama\\images\\hero_selection\\minimap_ward_obs_png.vtex_c", 0, 0, false, 0, new Vector4?(new Vector4(0.1f, 0.4f, 1f, 1f)));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000119AC File Offset: 0x0000FBAC
		private void OnAddEntity(EntityEventArgs args)
		{
			try
			{
				Unit unit = args.Entity as Unit;
				if (!(unit == null) && unit.Team != this.allyTeam)
				{
					AbilityFullData abilityFullData;
					if (this.abilityData.Units.TryGetValue(unit.Name, out abilityFullData))
					{
						abilityFullData.AddDrawableAbility(this.drawableAbilities, unit, this.notificationsEnabled ? this.notificator : null);
					}
					else
					{
						if (!(unit.NetworkName != "CDOTA_BaseNPC"))
						{
							if (unit.DayVision > 0u)
							{
								Dictionary<AbilityId, AbilityFullData> ids = (from x in this.abilityData.AbilityUnitVision
								where (long)x.Value.Vision == (long)((ulong)unit.DayVision)
								select x).ToDictionary((KeyValuePair<AbilityId, AbilityFullData> x) => x.Key, (KeyValuePair<AbilityId, AbilityFullData> x) => x.Value);
								List<Ability9> list = (from x in EntityManager9.Abilities
								where x.Owner.Team != this.allyTeam && x.Owner.CanUseAbilities && ids.ContainsKey(x.Id) && (!x.Owner.IsVisible || x.TimeSinceCasted < 0.5f + x.ActivationDelay)
								select x).ToList<Ability9>();
								if (list.Count == 1)
								{
									if (ids.TryGetValue(list[0].Id, out abilityFullData))
									{
										abilityFullData.AddDrawableAbility(this.drawableAbilities, list[0], unit, this.notificationsEnabled ? this.notificator : null);
									}
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

		// Token: 0x06000233 RID: 563 RVA: 0x00011B98 File Offset: 0x0000FD98
		private void OnDraw(IRenderer renderer)
		{
			try
			{
				foreach (IDrawableAbility drawableAbility in this.drawableAbilities)
				{
					if (drawableAbility.Draw)
					{
						if (this.showOnMap)
						{
							drawableAbility.DrawOnMap(renderer, this.minimap);
						}
						if (this.showOnMinimap)
						{
							drawableAbility.DrawOnMinimap(renderer, this.minimap);
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

		// Token: 0x06000234 RID: 564 RVA: 0x00011C48 File Offset: 0x0000FE48
		private void OnParticleEffectAdded(Entity sender, ParticleEffectAddedEventArgs args)
		{
			try
			{
				AbilityFullData data;
				if (this.abilityData.Particles.TryGetValue(args.Name, out data))
				{
					UpdateManager.BeginInvoke(delegate
					{
						try
						{
							ParticleEffect particleEffect = args.ParticleEffect;
							if (particleEffect.IsValid)
							{
								data.AddDrawableAbility(this.drawableAbilities, particleEffect, this.allyTeam, this.notificationsEnabled ? this.notificator : null);
							}
						}
						catch (Exception exception2)
						{
							Logger.Error(exception2, null);
						}
					}, 0);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00011CCC File Offset: 0x0000FECC
		private void OnRemoveEntity(EntityEventArgs args)
		{
			try
			{
				Entity entity = args.Entity;
				if (entity.Team != this.allyTeam)
				{
					if (this.abilityData.Units.ContainsKey(entity.Name))
					{
						DrawableUnitAbility drawableUnitAbility = this.drawableAbilities.OfType<DrawableUnitAbility>().FirstOrDefault((DrawableUnitAbility x) => x.Unit == entity);
						if (drawableUnitAbility != null)
						{
							this.drawableAbilities.Remove(drawableUnitAbility);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00011D68 File Offset: 0x0000FF68
		private void OnUnitAdded(Unit9 entity)
		{
			try
			{
				if (entity.IsHero && entity.CanUseAbilities && entity.Team != this.allyTeam)
				{
					this.enemyHeroes.Add(new EnemyHero(entity));
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				if (entity.IsHero && entity.CanUseAbilities && entity.Team != this.allyTeam)
				{
					EnemyHero enemyHero = this.enemyHeroes.Find((EnemyHero x) => x.Unit == entity);
					if (enemyHero != null)
					{
						this.enemyHeroes.Remove(enemyHero);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00011E4C File Offset: 0x0001004C
		private void OnUpdateRemove()
		{
			try
			{
				for (int i = this.drawableAbilities.Count - 1; i > -1; i--)
				{
					IDrawableAbility drawableAbility = this.drawableAbilities[i];
					if (!drawableAbility.IsValid)
					{
						if (drawableAbility.IsShowingRange)
						{
							drawableAbility.RemoveRange();
						}
						this.drawableAbilities.RemoveAt(i);
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00011EBC File Offset: 0x000100BC
		private void OnUpdateWard()
		{
			try
			{
				foreach (EnemyHero enemyHero in this.enemyHeroes)
				{
					if (enemyHero.Unit.IsValid)
					{
						if (!enemyHero.Unit.IsVisible)
						{
							enemyHero.ObserversCount = 0u;
							enemyHero.SentryCount = 0u;
						}
						else if (this.PlacedWard(enemyHero, AbilityId.item_ward_observer))
						{
							this.AddWard(enemyHero, "npc_dota_observer_wards");
						}
						else if (this.PlacedWard(enemyHero, AbilityId.item_ward_sentry))
						{
							this.AddWard(enemyHero, "npc_dota_sentry_wards");
						}
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00011F78 File Offset: 0x00010178
		private bool PlacedWard(EnemyHero enemy, AbilityId id)
		{
			uint num = enemy.CountWards(id);
			if (num < enemy.GetWardsCount(id))
			{
				enemy.SetWardsCount(id, num);
				if (!this.GaveWard(enemy) && !enemy.DroppedWard(id))
				{
					return true;
				}
			}
			else if (num > enemy.GetWardsCount(id) && !this.TookWard(enemy))
			{
				enemy.SetWardsCount(id, num);
			}
			return false;
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00011FD0 File Offset: 0x000101D0
		private bool TookWard(EnemyHero enemy)
		{
			return this.enemyHeroes.Any((EnemyHero x) => x.Unit.IsValid && !x.Equals(enemy) && x.Unit.IsAlive && x.Unit.Distance(enemy.Unit) <= 600f && x.ObserversCount + x.SentryCount > x.CountWards(AbilityId.item_ward_observer) + x.CountWards(AbilityId.item_ward_sentry));
		}

		// Token: 0x04000194 RID: 404
		private readonly O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.AbilityData abilityData = new O9K.Hud.Modules.Map.AbilityMonitor.Abilities.Data.AbilityData();

		// Token: 0x04000195 RID: 405
		private readonly IContext9 context;

		// Token: 0x04000196 RID: 406
		private readonly List<IDrawableAbility> drawableAbilities = new List<IDrawableAbility>();

		// Token: 0x04000197 RID: 407
		private readonly MenuSwitcher enabled;

		// Token: 0x04000198 RID: 408
		private readonly List<EnemyHero> enemyHeroes = new List<EnemyHero>();

		// Token: 0x04000199 RID: 409
		private readonly IMinimap minimap;

		// Token: 0x0400019A RID: 410
		private readonly MenuSwitcher notificationsEnabled;

		// Token: 0x0400019B RID: 411
		private readonly INotificator notificator;

		// Token: 0x0400019C RID: 412
		private readonly MenuSwitcher showOnMap;

		// Token: 0x0400019D RID: 413
		private readonly MenuSwitcher showOnMinimap;

		// Token: 0x0400019E RID: 414
		private Team allyTeam;
	}
}
