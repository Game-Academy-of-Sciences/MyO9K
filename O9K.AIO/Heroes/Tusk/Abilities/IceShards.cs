using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Tusk.Abilities
{
	// Token: 0x02000066 RID: 102
	internal class IceShards : NukeAbility
	{
		// Token: 0x06000223 RID: 547 RVA: 0x000032F0 File Offset: 0x000014F0
		public IceShards(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00003612 File Offset: 0x00001812
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			return base.CanBeCasted(targetManager, channelingCheck, comboMenu) && (!base.Owner.IsInvulnerable || base.Owner.Distance(targetManager.Target) <= 800f);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
			predictionInput.Delay += 0.5f;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(predictionOutput.CastPosition, base.Ability.Owner.Position, -200f), false, false))
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
