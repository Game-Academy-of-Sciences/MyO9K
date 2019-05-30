using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Warlock.Abilities
{
	// Token: 0x02000056 RID: 86
	internal class FatalBonds : DebuffAbility
	{
		// Token: 0x060001D3 RID: 467 RVA: 0x000034DD File Offset: 0x000016DD
		public FatalBonds(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000E39C File Offset: 0x0000C59C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			IEnumerable<Unit9> source = from x in EntityManager9.Units
			where x.IsAlive && x.IsVisible && !x.IsAlly(base.Owner) && !x.IsInvulnerable
			select x;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, source.ToList<Unit9>());
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < 3)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.Target, false, false))
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
