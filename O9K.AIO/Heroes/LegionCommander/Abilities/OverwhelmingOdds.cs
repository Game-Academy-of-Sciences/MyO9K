using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
	// Token: 0x02000127 RID: 295
	internal class OverwhelmingOdds : NukeAbility
	{
		// Token: 0x060005E2 RID: 1506 RVA: 0x000032F0 File Offset: 0x000014F0
		public OverwhelmingOdds(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x0001D490 File Offset: 0x0001B690
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (aoe)
			{
				PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, (from x in EntityManager9.Units
				where x.IsUnit && !x.IsAlly(base.Owner) && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable
				select x).ToList<Unit9>());
				PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
				{
					return false;
				}
			}
			else if (!base.Ability.UseAbility(targetManager.Target, 1, false, false))
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
