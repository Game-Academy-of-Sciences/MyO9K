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

namespace O9K.AIO.Heroes.Earthshaker.Abilities
{
	// Token: 0x02000148 RID: 328
	internal class BlinkDaggerShaker : BlinkAbility
	{
		// Token: 0x06000674 RID: 1652 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerShaker(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			UsableAbility usableAbility = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.earthshaker_enchant_totem);
			if (usableAbility != null)
			{
				PredictionInput9 predictionInput = usableAbility.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
				predictionInput.Range += base.Ability.CastRange;
				predictionInput.CastRange = base.Ability.CastRange;
				predictionInput.SkillShotType = 4;
				if (base.Owner.HasModifier("modifier_earthshaker_enchant_totem"))
				{
					predictionInput.AreaOfEffect = false;
					predictionInput.Delay -= 0.1f;
				}
				PredictionOutput9 predictionOutput = usableAbility.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				this.blinkPosition = predictionOutput.CastPosition;
				return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
			}
			else
			{
				EchoSlam echoSlam;
				if ((echoSlam = (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.earthshaker_echo_slam) as EchoSlam)) == null)
				{
					return false;
				}
				PredictionInput9 predictionInput2 = echoSlam.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
				predictionInput2.Range += base.Ability.CastRange;
				predictionInput2.CastRange = base.Ability.CastRange;
				predictionInput2.SkillShotType = 4;
				PredictionOutput9 predictionOutput2 = echoSlam.Ability.GetPredictionOutput(predictionInput2);
				if (predictionOutput2.HitChance < 1 || predictionOutput2.AoeTargetsHit.Count < echoSlam.TargetsToHit(menu))
				{
					return false;
				}
				this.blinkPosition = predictionOutput2.CastPosition;
				return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001F7B4 File Offset: 0x0001D9B4
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

		// Token: 0x0400038B RID: 907
		private Vector3 blinkPosition;
	}
}
