using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Reflection;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.Core.Entities.Mines;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using O9K.Core.Managers.Menu.Items;
using O9K.Hud.Core;

namespace O9K.Hud.Modules.Units
{
	// Token: 0x02000009 RID: 9
	internal class VisibleByEnemy : IDisposable, IHudModule
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00005124 File Offset: 0x00003324
		[ImportingConstructor]
		public VisibleByEnemy(IHudMenu hudMenu)
		{
			Menu menu = hudMenu.UnitsMenu.Add<Menu>(new Menu("Visible by enemy"));
			this.enabled = menu.Add<MenuSwitcher>(new MenuSwitcher("Enabled", true, false)).SetTooltip("Show when ally/neutral unit is visible by enemy");
			this.heroes = menu.Add<MenuSwitcher>(new MenuSwitcher("Heroes", true, false));
			this.creeps = menu.Add<MenuSwitcher>(new MenuSwitcher("Creeps", true, false));
			this.buildings = menu.Add<MenuSwitcher>(new MenuSwitcher("Buildings", true, false));
			this.other = menu.Add<MenuSwitcher>(new MenuSwitcher("Other", true, false));
			this.effectName = menu.Add<MenuSelector<string>>(new MenuSelector<string>("Effect", this.effects.Keys, false));
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000215B File Offset: 0x0000035B
		public void Activate()
		{
			this.enemyTeam = EntityManager9.Owner.EnemyTeam;
			this.enabled.ValueChange += this.EnabledOnValueChange;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000052A4 File Offset: 0x000034A4
		public void Dispose()
		{
			this.effectName.ValueChange -= this.EffectNameOnValueChange;
			this.enabled.ValueChange -= this.EnabledOnValueChange;
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.heroes.ValueChange -= this.OptionOnValueChange;
			this.creeps.ValueChange -= this.OptionOnValueChange;
			this.buildings.ValueChange -= this.OptionOnValueChange;
			this.other.ValueChange -= this.OptionOnValueChange;
			foreach (ParticleEffect particleEffect in this.particles.Values)
			{
				particleEffect.Dispose();
			}
			this.particles.Clear();
			this.units.Clear();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000053CC File Offset: 0x000035CC
		private void EffectNameOnValueChange(object sender, SelectorEventArgs<string> e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}
			foreach (ParticleEffect particleEffect in this.particles.Values)
			{
				particleEffect.Dispose();
			}
			this.particles.Clear();
			this.units.Clear();
			UpdateManager.BeginInvoke(delegate
			{
				foreach (Unit9 unit in EntityManager9.Units)
				{
					this.OnUnitAdded(unit);
				}
			}, 0);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00005460 File Offset: 0x00003660
		private void EnabledOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue)
			{
				if (e.OldValue)
				{
					if (AppDomain.CurrentDomain.GetAssemblies().Any((Assembly x) => !x.GlobalAssemblyCache && x.GetName().Name.Contains("VisibleByEnemy")))
					{
						O9K.Core.Helpers.Hud.DisplayWarning("O9K.Hud // VisibleByEnemy is already included in O9K.Hud", 10f);
					}
				}
				this.effectName.ValueChange += this.EffectNameOnValueChange;
				EntityManager9.UnitAdded += this.OnUnitAdded;
				EntityManager9.UnitRemoved += this.OnUnitRemoved;
				UpdateManager.Subscribe(new Action(this.OnUpdate), 300, true);
				this.heroes.ValueChange += this.OptionOnValueChange;
				this.creeps.ValueChange += this.OptionOnValueChange;
				this.buildings.ValueChange += this.OptionOnValueChange;
				this.other.ValueChange += this.OptionOnValueChange;
				return;
			}
			EntityManager9.UnitAdded -= this.OnUnitAdded;
			EntityManager9.UnitRemoved -= this.OnUnitRemoved;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			this.heroes.ValueChange -= this.OptionOnValueChange;
			this.creeps.ValueChange -= this.OptionOnValueChange;
			this.buildings.ValueChange -= this.OptionOnValueChange;
			this.other.ValueChange -= this.OptionOnValueChange;
			foreach (ParticleEffect particleEffect in this.particles.Values)
			{
				particleEffect.Dispose();
			}
			this.particles.Clear();
			this.units.Clear();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000565C File Offset: 0x0000385C
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (unit.Team != this.enemyTeam)
				{
					if (unit.IsHero)
					{
						if (this.heroes)
						{
							this.units.Add(unit);
							this.OnUpdate();
						}
					}
					else if (unit.IsCreep)
					{
						if (this.creeps)
						{
							this.units.Add(unit);
							this.OnUpdate();
						}
					}
					else if (unit.IsBuilding)
					{
						if (this.buildings)
						{
							this.units.Add(unit);
							this.OnUpdate();
						}
					}
					else if (this.other)
					{
						this.units.Add(unit);
						this.OnUpdate();
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00005730 File Offset: 0x00003930
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (unit.Team != this.enemyTeam)
				{
					ParticleEffect particleEffect;
					if (this.particles.TryGetValue(unit, out particleEffect))
					{
						particleEffect.Dispose();
						this.particles.Remove(unit);
					}
					this.units.Remove(unit);
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00005798 File Offset: 0x00003998
		private void OnUpdate()
		{
			try
			{
				foreach (Unit9 unit in this.units)
				{
					if (!unit.IsValid)
					{
						break;
					}
					if (!unit.IsVisibleToEnemies || !unit.IsAlive)
					{
						ParticleEffect particleEffect;
						if (this.particles.TryGetValue(unit, out particleEffect))
						{
							particleEffect.Dispose();
							this.particles.Remove(unit);
						}
					}
					else if (unit is RemoteMine)
					{
						ParticleEffect particleEffect2;
						if (this.particles.TryGetValue(unit, out particleEffect2))
						{
							particleEffect2.SetControlPoint(0u, unit.Position);
						}
						else
						{
							this.particles.Add(unit, new ParticleEffect(this.effects[this.effectName], unit.Position));
						}
					}
					else if (!this.particles.ContainsKey(unit))
					{
						this.particles.Add(unit, new ParticleEffect(this.effects[this.effectName], unit.BaseUnit, ParticleAttachment.AbsOriginFollow));
					}
				}
			}
			catch (Exception exception)
			{
				Logger.Error(exception, null);
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000058F4 File Offset: 0x00003AF4
		private void OptionOnValueChange(object sender, SwitcherEventArgs e)
		{
			if (e.NewValue == e.OldValue)
			{
				return;
			}
			foreach (ParticleEffect particleEffect in this.particles.Values)
			{
				particleEffect.Dispose();
			}
			this.particles.Clear();
			this.units.Clear();
			UpdateManager.BeginInvoke(delegate
			{
				foreach (Unit9 unit in EntityManager9.Units)
				{
					this.OnUnitAdded(unit);
				}
			}, 0);
		}

		// Token: 0x04000014 RID: 20
		private readonly MenuSwitcher buildings;

		// Token: 0x04000015 RID: 21
		private readonly MenuSwitcher creeps;

		// Token: 0x04000016 RID: 22
		private readonly MenuSelector<string> effectName;

		// Token: 0x04000017 RID: 23
		private readonly Dictionary<string, string> effects = new Dictionary<string, string>
		{
			{
				"Shiva",
				"particles/items_fx/aura_shivas.vpcf"
			},
			{
				"Aura",
				"particles/units/heroes/hero_omniknight/omniknight_heavenly_grace_core.vpcf"
			},
			{
				"Beam",
				"particles/units/heroes/hero_omniknight/omniknight_heavenly_grace_beam.vpcf"
			},
			{
				"Beam light",
				"particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_status.vpcf"
			},
			{
				"Dark",
				"particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_dark.vpcf"
			},
			{
				"Purge",
				"particles/units/heroes/hero_oracle/oracle_fortune_purge.vpcf"
			},
			{
				"Timer",
				"particles/units/heroes/hero_spirit_breaker/spirit_breaker_haste_owner_timer.vpcf"
			},
			{
				"Glow blue",
				"particles/world_tower/tower_upgrade/ti7_radiant_tower_ambient_core.vpcf"
			},
			{
				"Glow red",
				"particles/world_tower/tower_upgrade/ti7_dire_tower_ambient_core.vpcf"
			}
		};

		// Token: 0x04000018 RID: 24
		private readonly MenuSwitcher enabled;

		// Token: 0x04000019 RID: 25
		private readonly MenuSwitcher heroes;

		// Token: 0x0400001A RID: 26
		private readonly MenuSwitcher other;

		// Token: 0x0400001B RID: 27
		private readonly Dictionary<Unit9, ParticleEffect> particles = new Dictionary<Unit9, ParticleEffect>();

		// Token: 0x0400001C RID: 28
		private readonly List<Unit9> units = new List<Unit9>();

		// Token: 0x0400001D RID: 29
		private Team enemyTeam;
	}
}
