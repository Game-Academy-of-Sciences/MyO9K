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

namespace O9K.AIO.Heroes.Enigma.Abilities
{
	// Token: 0x0200013A RID: 314
	internal class BlinkDaggerEnigma : BlinkAbility
	{
		// Token: 0x06000642 RID: 1602 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerEnigma(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x0001E8A8 File Offset: 0x0001CAA8
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			BlackHole blackHole = (BlackHole)usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.enigma_black_hole);
			if (blackHole == null)
			{
				return false;
			}
			PredictionInput9 predictionInput = blackHole.Ability.GetPredictionInput(targetManager.Target, EntityManager9.EnemyHeroes);
			predictionInput.CastRange += base.Ability.CastRange;
			predictionInput.Range += base.Ability.CastRange;
			PredictionOutput9 predictionOutput = blackHole.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1 || predictionOutput.AoeTargetsHit.Count < blackHole.TargetsToHit(menu))
			{
				return false;
			}
			float num = Math.Min(base.Ability.CastRange, base.Owner.Distance(predictionOutput.CastPosition) - 200f);
			this.blinkPosition = Vector3Extensions.Extend2D(base.Owner.Position, predictionOutput.CastPosition, num);
			return base.Owner.Distance(this.blinkPosition) <= base.Ability.CastRange;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x000053A9 File Offset: 0x000035A9
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

		// Token: 0x0400036F RID: 879
		private Vector3 blinkPosition;
	}
}
