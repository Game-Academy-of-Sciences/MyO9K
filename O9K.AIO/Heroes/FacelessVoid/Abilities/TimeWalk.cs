using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.FacelessVoid.Abilities
{
	// Token: 0x02000177 RID: 375
	internal class TimeWalk : BlinkAbility
	{
		// Token: 0x060007B4 RID: 1972 RVA: 0x00002F6B File Offset: 0x0000116B
		public TimeWalk(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x00023558 File Offset: 0x00021758
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Chronosphere chronosphere = (Chronosphere)usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.faceless_void_chronosphere);
			if (chronosphere == null)
			{
				if (base.Owner.Distance(targetManager.Target) < 300f)
				{
					return false;
				}
				this.blinkPosition = Vector3.Zero;
				return true;
			}
			else
			{
				PredictionInput9 predictionInput = chronosphere.Ability.GetPredictionInput(targetManager.Target, EntityManager9.EnemyHeroes);
				predictionInput.CastRange += base.Ability.CastRange;
				predictionInput.Range += base.Ability.CastRange;
				PredictionOutput9 predictionOutput = chronosphere.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < chronosphere.TargetsToHit(menu))
				{
					return false;
				}
				float num = Math.Min(base.Ability.CastRange, base.Owner.Distance(predictionOutput.CastPosition) - 200f);
				this.blinkPosition = Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.CastPosition, num);
				return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
			}
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x00023694 File Offset: 0x00021894
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.blinkPosition.IsZero)
			{
				PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
				predictionInput.Delay += 0.5f;
				PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				this.blinkPosition = predictionOutput.CastPosition;
			}
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(this.blinkPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0400043A RID: 1082
		private Vector3 blinkPosition;
	}
}
