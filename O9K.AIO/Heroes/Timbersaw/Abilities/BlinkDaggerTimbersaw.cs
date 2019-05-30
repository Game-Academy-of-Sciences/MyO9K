using System;
using System.Collections.Generic;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
	// Token: 0x02000085 RID: 133
	internal class BlinkDaggerTimbersaw : BlinkAbility
	{
		// Token: 0x060002A3 RID: 675 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerTimbersaw(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x00003B13 File Offset: 0x00001D13
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && !base.Owner.HasModifier("modifier_shredder_timber_chain");
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00010E94 File Offset: 0x0000F094
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Unit9 target = targetManager.Target;
			if (base.Owner.Distance(target) < 600f)
			{
				return false;
			}
			if (usableAbilities.FirstOrDefault((UsableAbility x) => x.Ability.Id == AbilityId.shredder_timber_chain) == null)
			{
				return true;
			}
			this.blinkPosition = target.Position;
			UsableAbility usableAbility = usableAbilities.FirstOrDefault((UsableAbility x) => x.Ability.Id == AbilityId.shredder_whirling_death);
			if (usableAbility != null)
			{
				PredictionInput9 predictionInput = usableAbility.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
				predictionInput.Range += base.Ability.CastRange;
				predictionInput.CastRange = base.Ability.CastRange;
				predictionInput.SkillShotType = 4;
				PredictionOutput9 predictionOutput = usableAbility.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				this.blinkPosition = predictionOutput.CastPosition;
			}
			return base.Owner.Distance(this.blinkPosition) < base.Ability.CastRange;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00003B35 File Offset: 0x00001D35
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(this.blinkPosition, false, false))
			{
				return false;
			}
			base.Sleeper.Sleep(base.Ability.GetCastDelay(targetManager.Target));
			return true;
		}

		// Token: 0x04000170 RID: 368
		private Vector3 blinkPosition;
	}
}
