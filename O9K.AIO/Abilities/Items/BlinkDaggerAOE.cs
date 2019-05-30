using System;
using System.Collections.Generic;
using System.Linq;
using Ensage.SDK.Geometry;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020A RID: 522
	internal class BlinkDaggerAOE : BlinkAbility
	{
		// Token: 0x06000A65 RID: 2661 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerAOE(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A66 RID: 2662 RVA: 0x0002CC20 File Offset: 0x0002AE20
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			UsableAbility usableAbility = usableAbilities.FirstOrDefault<UsableAbility>();
			if (usableAbility == null)
			{
				return false;
			}
			PredictionInput9 predictionInput = usableAbility.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Range += base.Ability.CastRange;
			predictionInput.CastRange = base.Ability.CastRange;
			predictionInput.SkillShotType = 4;
			PredictionOutput9 predictionOutput = usableAbility.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.TargetPosition.Distance2D(predictionOutput.CastPosition, false) > usableAbility.Ability.Radius * 0.9f)
			{
				return false;
			}
			this.blinkPosition = predictionOutput.CastPosition;
			return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
		}

		// Token: 0x06000A67 RID: 2663 RVA: 0x0002CCEC File Offset: 0x0002AEEC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			float hitTime = base.Ability.GetHitTime(this.blinkPosition);
			comboSleeper.Sleep(hitTime + 0.1f);
			base.OrbwalkSleeper.Sleep(hitTime + 0.2f);
			base.Sleeper.Sleep(base.Ability.GetCastDelay(targetManager.Target) + 0.5f);
			return true;
		}

		// Token: 0x0400055F RID: 1375
		private Vector3 blinkPosition;
	}
}
