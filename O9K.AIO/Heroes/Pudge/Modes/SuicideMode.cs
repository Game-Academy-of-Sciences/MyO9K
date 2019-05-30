using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Heroes.Pudge.Units;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Units;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Managers.Entity.Monitors;

namespace O9K.AIO.Heroes.Pudge.Modes
{
	// Token: 0x020000CD RID: 205
	internal class SuicideMode : PermanentMode
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x00004357 File Offset: 0x00002557
		public SuicideMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00016F64 File Offset: 0x00015164
		private Pudge Hero
		{
			get
			{
				Pudge pudge = this.hero;
				if (pudge == null || !pudge.IsValid)
				{
					this.hero = (base.UnitManager.ControllableUnits.FirstOrDefault((ControllableUnit x) => base.Owner.Hero.Handle == x.Handle) as Pudge);
				}
				return this.hero;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00016FB8 File Offset: 0x000151B8
		public override void Disable()
		{
			base.Disable();
			ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
			ObjectManager.OnRemoveTrackingProjectile -= this.OnRemoveTrackingProjectile;
			EntityManager9.UnitMonitor.UnitDied -= new UnitMonitor.EventHandler(this.OnUnitDied);
			EntityManager9.UnitMonitor.AttackStart -= new UnitMonitor.EventHandler(this.OnAttackStart);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0001701C File Offset: 0x0001521C
		public override void Dispose()
		{
			base.Dispose();
			ObjectManager.OnAddTrackingProjectile -= this.OnAddTrackingProjectile;
			ObjectManager.OnRemoveTrackingProjectile -= this.OnRemoveTrackingProjectile;
			EntityManager9.UnitMonitor.UnitDied -= new UnitMonitor.EventHandler(this.OnUnitDied);
			EntityManager9.UnitMonitor.AttackStart -= new UnitMonitor.EventHandler(this.OnAttackStart);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00017080 File Offset: 0x00015280
		public override void Enable()
		{
			base.Enable();
			ObjectManager.OnAddTrackingProjectile += this.OnAddTrackingProjectile;
			ObjectManager.OnRemoveTrackingProjectile += this.OnRemoveTrackingProjectile;
			EntityManager9.UnitMonitor.UnitDied += new UnitMonitor.EventHandler(this.OnUnitDied);
			EntityManager9.UnitMonitor.AttackStart += new UnitMonitor.EventHandler(this.OnAttackStart);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000170E4 File Offset: 0x000152E4
		protected override void Execute()
		{
			if (this.Hero == null)
			{
				return;
			}
			if (!this.hero.Owner.IsAlive || this.hero.Owner.IsInvulnerable)
			{
				return;
			}
			this.hero.SoulRingSuicide(this.attacks, this.projectiles);
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00017138 File Offset: 0x00015338
		private void OnAddTrackingProjectile(TrackingProjectileEventArgs args)
		{
			try
			{
				TrackingProjectile projectile = args.Projectile;
				Entity target = projectile.Target;
				EntityHandle? entityHandle = (target != null) ? new EntityHandle?(target.Handle) : null;
				if (!(((entityHandle != null) ? new uint?(entityHandle.GetValueOrDefault()) : null) != base.Owner.HeroHandle))
				{
					Unit unit = projectile.Source as Unit;
					if (!(unit == null))
					{
						Unit9 unit2 = EntityManager9.GetUnit(unit.Handle);
						if (!(unit2 == null))
						{
							this.projectiles[projectile] = unit2.GetAttackDamage(base.Owner, 2, 0f);
						}
					}
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00017230 File Offset: 0x00015430
		private void OnAttackStart(Unit9 unit)
		{
			try
			{
				if (unit.Team != base.Owner.Team && unit.Distance(base.Owner) <= 2000f)
				{
					this.attacks[unit] = Game.RawGameTime - Game.Ping / 2000f;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000172A4 File Offset: 0x000154A4
		private void OnRemoveTrackingProjectile(TrackingProjectileEventArgs args)
		{
			try
			{
				this.projectiles.Remove(args.Projectile);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000172E0 File Offset: 0x000154E0
		private void OnUnitDied(Unit9 unit)
		{
			try
			{
				this.attacks.Remove(unit);
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x0400025A RID: 602
		private readonly Dictionary<Unit9, float> attacks = new Dictionary<Unit9, float>();

		// Token: 0x0400025B RID: 603
		private readonly Dictionary<TrackingProjectile, int> projectiles = new Dictionary<TrackingProjectile, int>();

		// Token: 0x0400025C RID: 604
		private Pudge hero;
	}
}
