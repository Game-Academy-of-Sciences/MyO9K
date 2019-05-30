using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Slark.Abilities
{
	// Token: 0x020000A4 RID: 164
	internal class Pounce : UntargetableAbility
	{
		// Token: 0x06000342 RID: 834 RVA: 0x00003F2C File Offset: 0x0000212C
		public Pounce(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001317C File Offset: 0x0001137C
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			if (target.IsMagicImmune)
			{
				return false;
			}
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			return base.Ability.GetPredictionOutput(predictionInput).HitChance > 0;
		}

		// Token: 0x06000344 RID: 836 RVA: 0x000131C0 File Offset: 0x000113C0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (base.Owner.GetAngle(predictionOutput.CastPosition, false) > 0.1f)
			{
				base.Owner.BaseUnit.Move(predictionOutput.CastPosition);
				base.OrbwalkSleeper.Sleep(0.1f);
				comboSleeper.Sleep(0.1f);
				return true;
			}
			if (!base.Ability.UseAbility(false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
