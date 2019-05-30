using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Venomancer.Abilities
{
	// Token: 0x0200005A RID: 90
	internal class PlagueWardAbility : AoeAbility
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x0000356A File Offset: 0x0000176A
		public PlagueWardAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x0000E7D4 File Offset: 0x0000C9D4
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsVisible && !target.IsInvulnerable;
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000E800 File Offset: 0x0000CA00
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
			predictionInput.Delay += 0.5f;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(predictionOutput.CastPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
