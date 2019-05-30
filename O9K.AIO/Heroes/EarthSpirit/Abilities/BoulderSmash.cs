using System;
using System.Linq;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x0200018D RID: 397
	internal class BoulderSmash : NukeAbility
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x000032F0 File Offset: 0x000014F0
		public BoulderSmash(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x00006094 File Offset: 0x00004294
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return !base.Owner.HasModifier("modifier_earth_spirit_rolling_boulder_caster");
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x00024F60 File Offset: 0x00023160
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = this.GetPredictionInput(target);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			foreach (Unit9 unit in from x in EntityManager9.Units
			where x.IsUnit && (!x.IsHero || x.IsIllusion) && x.Distance(base.Owner) < base.Ability.CastRange && x.IsAlive
			select x)
			{
				if (new Polygon.Rectangle(Vector3Extensions.Extend2D(base.Owner.Position, unit.Position, -100f), Vector3Extensions.Extend2D(base.Owner.Position, unit.Position, base.Ability.Range), base.Ability.Radius).IsInside(predictionOutput.TargetPosition) && base.Ability.UseAbility(unit, false, false))
				{
					float castDelay = base.Ability.GetCastDelay(targetManager.Target);
					comboSleeper.Sleep(castDelay);
					base.Sleeper.Sleep(castDelay + 0.5f);
					base.OrbwalkSleeper.Sleep(castDelay);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00025094 File Offset: 0x00023294
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = this.GetPredictionInput(target);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			foreach (Unit9 unit in from x in EntityManager9.Units
			where x.Name == "npc_dota_earth_spirit_stone" && x.Distance(base.Owner) < base.Ability.CastRange && x.IsAlive
			select x)
			{
				if (new Polygon.Rectangle(Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.TargetPosition, -100f), Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.TargetPosition, base.Ability.Range), base.Ability.Radius).IsInside(unit.Position) && base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
				{
					float castDelay = base.Ability.GetCastDelay(targetManager.Target);
					comboSleeper.Sleep(castDelay);
					base.Sleeper.Sleep(castDelay + 0.5f);
					base.OrbwalkSleeper.Sleep(castDelay);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x000060AB File Offset: 0x000042AB
		private PredictionInput9 GetPredictionInput(Unit9 target)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.SkillShotType = 3;
			return predictionInput;
		}
	}
}
