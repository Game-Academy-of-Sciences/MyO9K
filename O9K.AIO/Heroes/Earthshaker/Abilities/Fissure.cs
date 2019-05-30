using System;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Earthshaker.Abilities
{
	// Token: 0x0200014B RID: 331
	internal class Fissure : DisableAbility
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x00003482 File Offset: 0x00001682
		public Fissure(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0001F880 File Offset: 0x0001DA80
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Ability9 ability = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.earthshaker_echo_slam);
			if (ability != null && ability.TimeSinceCasted < 1f)
			{
				if (!base.Ability.UseAbility(target, targetManager.EnemyHeroes, 1, 0, false, false))
				{
					return false;
				}
			}
			else
			{
				Vector3 enemyFountain = targetManager.EnemyFountain;
				PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
				PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(predictionOutput.CastPosition, enemyFountain, 200f), false, false))
				{
					return false;
				}
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}
	}
}
