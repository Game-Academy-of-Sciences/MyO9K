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

namespace O9K.AIO.Heroes.CrystalMaiden.Abilities
{
	// Token: 0x020001D0 RID: 464
	internal class BlinkDaggerCM : BlinkAbility
	{
		// Token: 0x06000944 RID: 2372 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerCM(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000945 RID: 2373 RVA: 0x000290D0 File Offset: 0x000272D0
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			FreezingField freezingField = (FreezingField)usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.crystal_maiden_freezing_field);
			if (freezingField == null)
			{
				return false;
			}
			PredictionInput9 predictionInput = freezingField.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Range += base.Ability.CastRange;
			predictionInput.CastRange = base.Ability.CastRange;
			predictionInput.SkillShotType = 4;
			predictionInput.Radius *= 0.7f;
			PredictionOutput9 predictionOutput = freezingField.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < freezingField.TargetsToHit(menu))
			{
				return false;
			}
			this.blinkPosition = predictionOutput.CastPosition;
			return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
		}

		// Token: 0x06000946 RID: 2374 RVA: 0x00006A64 File Offset: 0x00004C64
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(0.5f);
			base.Sleeper.Sleep(0.5f);
			return true;
		}

		// Token: 0x040004E9 RID: 1257
		private Vector3 blinkPosition;
	}
}
