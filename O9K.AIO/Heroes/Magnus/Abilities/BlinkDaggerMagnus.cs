using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Magnus.Abilities
{
	// Token: 0x02000110 RID: 272
	internal class BlinkDaggerMagnus : BlinkAbility
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerMagnus(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001B884 File Offset: 0x00019A84
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			ReversePolarity reversePolarity;
			if ((reversePolarity = (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.magnataur_reverse_polarity) as ReversePolarity)) == null)
			{
				return false;
			}
			PredictionInput9 predictionInput = reversePolarity.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Range += base.Ability.CastRange;
			predictionInput.CastRange = base.Ability.CastRange;
			predictionInput.SkillShotType = 4;
			PredictionOutput9 predictionOutput = reversePolarity.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < reversePolarity.TargetsToHit(menu))
			{
				return false;
			}
			this.blinkPosition = predictionOutput.CastPosition;
			return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
		}

		// Token: 0x06000562 RID: 1378 RVA: 0x0001B964 File Offset: 0x00019B64
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			comboSleeper.Sleep(0.3f);
			base.OrbwalkSleeper.Sleep(0.5f);
			base.Sleeper.Sleep(base.Ability.GetCastDelay(targetManager.Target) + 0.5f);
			return true;
		}

		// Token: 0x04000301 RID: 769
		private Vector3 blinkPosition;
	}
}
