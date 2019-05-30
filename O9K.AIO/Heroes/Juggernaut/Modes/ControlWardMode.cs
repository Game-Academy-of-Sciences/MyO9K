using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Heroes.Base;
using O9K.AIO.Modes.Permanent;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Juggernaut;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Logger;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Juggernaut.Modes
{
	// Token: 0x02000161 RID: 353
	internal class ControlWardMode : PermanentMode
	{
		// Token: 0x06000751 RID: 1873 RVA: 0x00005B53 File Offset: 0x00003D53
		public ControlWardMode(BaseHero baseHero, PermanentModeMenu menu) : base(baseHero, menu)
		{
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x00005B68 File Offset: 0x00003D68
		public override void Disable()
		{
			base.Disable();
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x00005B92 File Offset: 0x00003D92
		public override void Dispose()
		{
			base.Dispose();
			EntityManager9.UnitAdded -= new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved -= new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x00005BBC File Offset: 0x00003DBC
		public override void Enable()
		{
			base.Enable();
			EntityManager9.UnitAdded += new EntityManager9.EventHandler<Unit9>(this.OnUnitAdded);
			EntityManager9.UnitRemoved += new EntityManager9.EventHandler<Unit9>(this.OnUnitRemoved);
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0002243C File Offset: 0x0002063C
		protected override void Execute()
		{
			if (this.sleeper.IsSleeping)
			{
				return;
			}
			Unit9 unit = this.ward;
			if (unit == null || !unit.IsValid || !this.ward.IsAlive)
			{
				return;
			}
			Vector3 fountain = base.TargetManager.Fountain;
			Unit9 owner = this.ward.Owner;
			HealingWard healingWard = (HealingWard)owner.Abilities.First((Ability9 x) => x.Id == AbilityId.juggernaut_healing_ward);
			List<Unit9> list = (from x in base.TargetManager.AllyHeroes
			where x.HealthPercentage < 90f || x.IsMyHero
			orderby x.HealthPercentage
			select x).ToList<Unit9>();
			Unit9 unit2 = (owner.HealthPercentage < 80f && owner.IsAlive) ? owner : list.FirstOrDefault<Unit9>();
			if (unit2 == null || !unit2.IsAlive)
			{
				return;
			}
			PredictionInput9 predictionInput = healingWard.GetPredictionInput(unit2, list);
			predictionInput.CastRange = 2000f;
			PredictionOutput9 predictionOutput = healingWard.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return;
			}
			Vector3 vector = predictionOutput.CastPosition;
			if (predictionOutput.AoeTargetsHit.Count == 1)
			{
				vector = predictionOutput.TargetPosition;
			}
			if (!unit2.Equals(owner) || owner.HealthPercentage > 50f)
			{
				foreach (Unit9 unit3 in from x in base.TargetManager.EnemyHeroes
				orderby x.GetAttackRange(null, 0f) descending
				select x)
				{
					float num = unit3.GetAttackRange(null, 0f) * (unit3.IsRanged ? 2f : 3f);
					if (unit3.Distance(vector) <= num)
					{
						vector = unit2.InFront(5f, 0f, false);
						break;
					}
				}
			}
			if (this.ward.Position == vector)
			{
				return;
			}
			this.ward.BaseUnit.Move(vector);
			this.sleeper.Sleep(0.15f);
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0002269C File Offset: 0x0002089C
		private void OnUnitAdded(Unit9 entity)
		{
			try
			{
				if (entity.IsControllable && entity.IsAlly(base.Owner) && !(entity.Name != "npc_dota_juggernaut_healing_ward"))
				{
					this.ward = entity;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x000226FC File Offset: 0x000208FC
		private void OnUnitRemoved(Unit9 entity)
		{
			try
			{
				if (!(entity != this.ward))
				{
					this.ward = null;
				}
			}
			catch (Exception ex)
			{
				Logger.Error(ex, null);
			}
		}

		// Token: 0x04000402 RID: 1026
		private readonly Sleeper sleeper = new Sleeper();

		// Token: 0x04000403 RID: 1027
		private Unit9 ward;
	}
}
