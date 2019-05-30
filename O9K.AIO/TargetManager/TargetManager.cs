using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using Ensage.SDK.Helpers;
using O9K.AIO.Menu;
using O9K.Core.Entities.Heroes;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Menu.EventArgs;
using SharpDX;

namespace O9K.AIO.TargetManager
{
	// Token: 0x02000014 RID: 20
	internal class TargetManager : IDisposable
	{
		// Token: 0x0600005A RID: 90 RVA: 0x00008E18 File Offset: 0x00007018
		public TargetManager(MenuManager menu)
		{
			this.Owner = EntityManager9.Owner;
			this.targetManagerMenu = new TargetManagerMenu(menu.GeneralSettingsMenu);
			this.targetManagerMenu.FocusTarget.ValueChange += this.FocusTargetOnValueChanged;
			this.targetManagerMenu.DrawTargetParticle.ValueChange += this.DrawTargetParticleOnValueChanged;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000022DB File Offset: 0x000004DB
		public Sleeper TargetSleeper { get; } = new Sleeper();

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000022E3 File Offset: 0x000004E3
		public Owner Owner { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000022EB File Offset: 0x000004EB
		public Vector3 EnemyFountain
		{
			get
			{
				if (this.fountain == Vector3.Zero)
				{
					this.fountain = EntityManager9.Units.First((Unit9 x) => x.IsFountain && x.IsEnemy(this.Owner)).Position;
				}
				return this.fountain;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002326 File Offset: 0x00000526
		public Vector3 Fountain
		{
			get
			{
				if (this.fountain == Vector3.Zero)
				{
					this.fountain = EntityManager9.Units.First((Unit9 x) => x.IsFountain && x.IsAlly(this.Owner)).Position;
				}
				return this.fountain;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002361 File Offset: 0x00000561
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00002369 File Offset: 0x00000569
		public bool TargetLocked { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002372 File Offset: 0x00000572
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000237A File Offset: 0x0000057A
		public float TargetDistance { get; set; } = 2500f;

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002383 File Offset: 0x00000583
		// (set) Token: 0x06000064 RID: 100 RVA: 0x0000238B File Offset: 0x0000058B
		public Unit9 Target { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002394 File Offset: 0x00000594
		public bool HasValidTarget
		{
			get
			{
				Unit9 target = this.Target;
				return target != null && target.IsValid && this.Target.IsAlive;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000066 RID: 102 RVA: 0x000023B7 File Offset: 0x000005B7
		public List<Unit9> EnemyHeroes
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000067 RID: 103 RVA: 0x000023D4 File Offset: 0x000005D4
		public List<Unit9> AllEnemyHeroes
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsHero && x.IsAlive && x.IsVisible && !x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000023F1 File Offset: 0x000005F1
		public List<Unit9> AllEnemyUnits
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsUnit && x.IsAlive && !x.IsInvulnerable && x.IsVisible && !x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000069 RID: 105 RVA: 0x0000240E File Offset: 0x0000060E
		public List<Unit9> EnemyUnits
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsUnit && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600006A RID: 106 RVA: 0x0000242B File Offset: 0x0000062B
		public List<Unit9> AllyUnits
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsUnit && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002448 File Offset: 0x00000648
		public List<Unit9> AllyHeroes
		{
			get
			{
				return (from x in EntityManager9.Units
				where x.IsHero && x.IsAlive && !x.IsIllusion && x.IsVisible && !x.IsInvulnerable && x.IsAlly(this.Owner)
				select x).ToList<Unit9>();
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00009018 File Offset: 0x00007218
		public Unit9 ClosestAllyHeroToMouse(Unit9 unit, bool ignoreSelf = true)
		{
			Vector3 mouse = Game.MousePosition;
			return (from x in this.AllyHeroes
			where (!ignoreSelf || !x.Equals(unit)) && x.Distance(mouse) < 500f
			orderby x.Distance(mouse)
			select x).FirstOrDefault<Unit9>();
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00009074 File Offset: 0x00007274
		public void Disable()
		{
			ParticleEffect particleEffect = this.targetParticleEffect;
			if (particleEffect != null)
			{
				particleEffect.Dispose();
			}
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x000090C8 File Offset: 0x000072C8
		public void Dispose()
		{
			this.targetManagerMenu.FocusTarget.ValueChange -= this.FocusTargetOnValueChanged;
			this.targetManagerMenu.DrawTargetParticle.ValueChange -= this.DrawTargetParticleOnValueChanged;
			UpdateManager.Unsubscribe(new Action(this.OnUpdate));
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00009140 File Offset: 0x00007340
		public void Enable()
		{
			UpdateManager.Subscribe(new Action(this.OnUpdate), 0, true);
			if (ObjectManager.GetEntities<Player>().Any((Player x) => x.SelectedHeroId == HeroId.npc_dota_hero_phoenix))
			{
				EntityManager9.UnitAdded += new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
				EntityManager9.UnitRemoved += new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002465 File Offset: 0x00000665
		public void ForceSetTarget(Unit9 target)
		{
			this.Target = target;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000091B0 File Offset: 0x000073B0
		private IEnumerable<Unit9> ClosestToMouse(Unit9 unit, float range)
		{
			Vector3 mouse = Game.MousePosition;
			return (from x in EntityManager9.Units
			where (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name))) && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(mouse) < 750f
			orderby x.Distance(mouse)
			select x).ToList<Unit9>();
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00009208 File Offset: 0x00007408
		private IEnumerable<Unit9> ClosestToUnit(Unit9 unit, float range)
		{
			return (from x in EntityManager9.Heroes
			where (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name))) && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(unit) <= range
			orderby x.Distance(unit)
			select x).ToList<Hero9>();
		}

		// Token: 0x06000073 RID: 115 RVA: 0x0000925C File Offset: 0x0000745C
		private void DrawTargetParticle()
		{
			ParticleEffect particleEffect = this.targetParticleEffect;
			if (particleEffect == null || !particleEffect.IsValid)
			{
				this.targetParticleEffect = new ParticleEffect("materials\\ensage_ui\\particles\\target.vpcf", this.Target.Position);
				this.targetParticleEffect.SetControlPoint(6u, new Vector3(255f));
			}
			this.targetParticleEffect.SetControlPoint(2u, this.Owner.Hero.Position);
			this.targetParticleEffect.SetControlPoint(5u, this.TargetLocked ? this.orangeRedColor : this.orangeColor);
			this.targetParticleEffect.SetControlPoint(7u, this.Target.IsVisible ? this.Target.Position : this.Target.GetPredictedPosition(0f));
		}

		// Token: 0x06000074 RID: 116 RVA: 0x0000246E File Offset: 0x0000066E
		private void DrawTargetParticleOnValueChanged(object sender, SwitcherEventArgs e)
		{
			this.drawTargetParticle = e.NewValue;
			if (!this.drawTargetParticle)
			{
				this.RemoveTargetParticle();
			}
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00009328 File Offset: 0x00007528
		private void FocusTargetOnValueChanged(object sender, SelectorEventArgs<string> e)
		{
			string newValue = e.NewValue;
			if (newValue == "Near mouse")
			{
				this.getTargetsFunc = new Func<Unit9, float, IEnumerable<Unit9>>(this.ClosestToMouse);
				return;
			}
			if (newValue == "Near hero")
			{
				this.getTargetsFunc = new Func<Unit9, float, IEnumerable<Unit9>>(this.ClosestToUnit);
				return;
			}
			if (!(newValue == "Lowest health"))
			{
				return;
			}
			this.getTargetsFunc = new Func<Unit9, float, IEnumerable<Unit9>>(this.LowestHealth);
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000939C File Offset: 0x0000759C
		private void GetTarget()
		{
			this.Target = this.getTargetsFunc(this.Owner.Hero, this.TargetDistance).FirstOrDefault((Unit9 x) => (x.UnitState & (UnitState.Unselectable | UnitState.CommandRestricted)) != (UnitState.Unselectable | UnitState.CommandRestricted));
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000093F0 File Offset: 0x000075F0
		private IEnumerable<Unit9> LowestHealth(Unit9 unit, float range)
		{
			return (from x in EntityManager9.Heroes
			where (x.IsHero || (this.targetManagerMenu.AdditionalTargets && !x.IsBuilding && this.targetableUnits.Contains(x.Name))) && x.IsAlive && !x.IsIllusion && x.IsVisible && x.IsEnemy(unit) && x.Distance(unit) <= range
			orderby x.Health
			select x).ToList<Hero9>();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00009458 File Offset: 0x00007658
		private void OnUnitAdded(Unit9 unit)
		{
			try
			{
				if (this.TargetLocked && !unit.IsAlly(this.Owner) && !(unit.Name != "npc_dota_phoenix_sun"))
				{
					Unit9 target = this.Target;
					if (((target != null) ? target.Name : null) == "npc_dota_hero_phoenix")
					{
						this.Target = unit;
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000094D4 File Offset: 0x000076D4
		private void OnUnitRemoved(Unit9 unit)
		{
			try
			{
				if (this.TargetLocked && !unit.IsAlly(this.Owner) && !(unit.Name != "npc_dota_phoenix_sun"))
				{
					Unit9 target = this.Target;
					if (target != null && target.Equals(unit))
					{
						this.Target = EntityManager9.Heroes.FirstOrDefault((Hero9 x) => x.Id == HeroId.npc_dota_hero_phoenix);
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00009570 File Offset: 0x00007770
		private void OnUpdate()
		{
			try
			{
				if (this.TargetLocked && this.targetManagerMenu.LockTarget)
				{
					if (this.targetManagerMenu.DeathSwitch && !this.HasValidTarget)
					{
						this.GetTarget();
					}
				}
				else
				{
					this.GetTarget();
				}
				if (this.drawTargetParticle)
				{
					if (!this.HasValidTarget)
					{
						this.RemoveTargetParticle();
					}
					else
					{
						this.DrawTargetParticle();
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000248A File Offset: 0x0000068A
		private void RemoveTargetParticle()
		{
			if (this.targetParticleEffect == null)
			{
				return;
			}
			this.targetParticleEffect.Dispose();
			this.targetParticleEffect = null;
		}

		// Token: 0x04000035 RID: 53
		private readonly Vector3 orangeColor = new Vector3((float)Color.Orange.R, (float)Color.Orange.G, (float)Color.Orange.B);

		// Token: 0x04000036 RID: 54
		private readonly Vector3 orangeRedColor = new Vector3((float)Color.OrangeRed.R, (float)Color.OrangeRed.G, (float)Color.OrangeRed.B);

		// Token: 0x04000037 RID: 55
		private readonly TargetManagerMenu targetManagerMenu;

		// Token: 0x04000038 RID: 56
		private bool drawTargetParticle;

		// Token: 0x04000039 RID: 57
		private Vector3 fountain;

		// Token: 0x0400003A RID: 58
		private Func<Unit9, float, IEnumerable<Unit9>> getTargetsFunc;

		// Token: 0x0400003B RID: 59
		private readonly HashSet<string> targetableUnits = new HashSet<string>
		{
			"npc_dota_hero_target_dummy",
			"npc_dota_courier",
			"npc_dota_phoenix_sun",
			"npc_dota_juggernaut_healing_ward",
			"npc_dota_templar_assassin_psionic_trap",
			"npc_dota_techies_land_mine",
			"npc_dota_techies_stasis_trap",
			"npc_dota_techies_remote_mine",
			"npc_dota_weaver_swarm",
			"npc_dota_grimstroke_ink_creature",
			"npc_dota_sentry_wards",
			"npc_dota_observer_wards",
			"npc_dota_lone_druid_bear1",
			"npc_dota_lone_druid_bear2",
			"npc_dota_lone_druid_bear3",
			"npc_dota_lone_druid_bear4",
			"npc_dota_unit_tombstone1",
			"npc_dota_unit_tombstone2",
			"npc_dota_unit_tombstone3",
			"npc_dota_unit_tombstone4",
			"npc_dota_pugna_nether_ward_1",
			"npc_dota_pugna_nether_ward_2",
			"npc_dota_pugna_nether_ward_3",
			"npc_dota_pugna_nether_ward_4"
		};

		// Token: 0x0400003C RID: 60
		private ParticleEffect targetParticleEffect;
	}
}
