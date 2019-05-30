using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.FailSafe;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Leshrac.Abilities
{
	// Token: 0x02000122 RID: 290
	internal class SplitEarth : DisableAbility
	{
		// Token: 0x060005C8 RID: 1480 RVA: 0x00003482 File Offset: 0x00001682
		public SplitEarth(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x00005095 File Offset: 0x00003295
		// (set) Token: 0x060005CA RID: 1482 RVA: 0x0000509D File Offset: 0x0000329D
		public NukeAbility Storm { get; set; }

		// Token: 0x060005CB RID: 1483 RVA: 0x0001CCD4 File Offset: 0x0001AED4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			Modifier modifier = targetManager.Target.GetModifier("modifier_leshrac_lightning_storm_slow");
			if (modifier != null)
			{
				if (modifier.RemainingTime < predictionInput.Delay)
				{
					predictionInput.Delay += (predictionInput.Delay - modifier.RemainingTime) * 3f;
					this.FailSafe.Sleeper.Sleep(0.3f);
				}
			}
			else if (this.Storm.Sleeper.IsSleeping)
			{
				predictionInput.Delay *= 0.5f;
				this.FailSafe.Sleeper.Sleep(0.3f);
			}
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			Vector3 castPosition = predictionOutput.CastPosition;
			if (predictionOutput.HitChance < 1 || base.Owner.Distance(castPosition) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(castPosition, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(castPosition) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(castPosition);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}

		// Token: 0x04000332 RID: 818
		public FailSafe FailSafe;
	}
}
