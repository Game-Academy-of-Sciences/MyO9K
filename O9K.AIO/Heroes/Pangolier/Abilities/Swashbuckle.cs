using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Pangolier;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Pangolier.Abilities
{
	// Token: 0x020000E8 RID: 232
	internal class Swashbuckle : NukeAbility
	{
		// Token: 0x060004B2 RID: 1202 RVA: 0x00004758 File Offset: 0x00002958
		public Swashbuckle(ActiveAbility ability) : base(ability)
		{
			this.swashbuckle = (Swashbuckle)ability;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0000476D File Offset: 0x0000296D
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && !base.Owner.HasModifier("modifier_pangolier_gyroshell");
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x00018BC8 File Offset: 0x00016DC8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.CastRange = base.Ability.CastRange;
			predictionInput.Range = base.Ability.Range;
			predictionInput.UseBlink = true;
			predictionInput.AreaOfEffect = true;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || base.Owner.Distance(predictionOutput.TargetPosition) > base.Ability.CastRange + 100f)
			{
				return false;
			}
			if (!this.swashbuckle.UseAbility(predictionOutput.BlinkLinePosition, predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float hitTime = base.Ability.GetHitTime(targetManager.Target);
			comboSleeper.Sleep(hitTime);
			base.Sleeper.Sleep(hitTime + 0.5f);
			base.OrbwalkSleeper.Sleep(hitTime);
			return true;
		}

		// Token: 0x04000292 RID: 658
		private readonly Swashbuckle swashbuckle;
	}
}
