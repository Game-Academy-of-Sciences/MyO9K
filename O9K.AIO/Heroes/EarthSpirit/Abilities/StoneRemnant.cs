using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x02000194 RID: 404
	internal class StoneRemnant : TargetableAbility
	{
		// Token: 0x0600082F RID: 2095 RVA: 0x00002FCA File Offset: 0x000011CA
		public StoneRemnant(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00025878 File Offset: 0x00023A78
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(base.Owner.Position, targetManager.Target.Position, 100f), false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x000258F4 File Offset: 0x00023AF4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Modifier modifier = target.GetModifier("modifier_earth_spirit_magnetize");
			if (modifier == null || modifier.RemainingTime > 0.75f)
			{
				return false;
			}
			List<Unit9> list = (from x in targetManager.EnemyHeroes
			where x.HasModifier("modifier_earth_spirit_magnetize")
			select x).ToList<Unit9>();
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, list);
			predictionInput.Radius = 400f;
			predictionInput.AreaOfEffect = true;
			predictionInput.SkillShotType = 4;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x000259F4 File Offset: 0x00023BF4
		public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, BoulderSmash smash)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = smash.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = smash.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.CastPosition, 100f), false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00025A98 File Offset: 0x00023C98
		public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, GeomagneticGrip grip)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = grip.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = grip.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.1f);
			base.Sleeper.Sleep(1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
