using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Mars.Abilities
{
	// Token: 0x02000105 RID: 261
	internal class ArenaOfBlood : AoeAbility
	{
		// Token: 0x06000536 RID: 1334 RVA: 0x0000356A File Offset: 0x0000176A
		public ArenaOfBlood(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000537 RID: 1335 RVA: 0x0001ACE4 File Offset: 0x00018EE4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			float castRange = base.Ability.CastRange;
			int num = 50;
			while ((float)num <= castRange)
			{
				PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
				predictionInput.CastRange = (float)num;
				predictionInput.Radius -= 75f;
				PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance >= 1 && base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
				{
					float castDelay = base.Ability.GetCastDelay(targetManager.Target);
					comboSleeper.Sleep(castDelay);
					base.Sleeper.Sleep(castDelay + 0.5f);
					base.OrbwalkSleeper.Sleep(castDelay);
					return true;
				}
				num += 50;
			}
			return false;
		}
	}
}
