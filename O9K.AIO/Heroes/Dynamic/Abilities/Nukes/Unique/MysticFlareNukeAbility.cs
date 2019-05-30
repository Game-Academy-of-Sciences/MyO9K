using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001AB RID: 427
	[AbilityId(AbilityId.skywrath_mage_mystic_flare)]
	internal class MysticFlareNukeAbility : OldNukeAbility
	{
		// Token: 0x06000898 RID: 2200 RVA: 0x000064AE File Offset: 0x000046AE
		public MysticFlareNukeAbility(INuke ability) : base(ability)
		{
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x00026E64 File Offset: 0x00025064
		public override bool ShouldCast(Unit9 target)
		{
			if (!base.ShouldCast(target))
			{
				return false;
			}
			if (target.IsStunned || target.IsRooted || target.IsHexed)
			{
				return target.GetImmobilityDuration() > 0f;
			}
			return target.HasModifier("modifier_skywrath_mage_concussive_shot_slow") || target.Speed < 250f;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00026EC4 File Offset: 0x000250C4
		public override bool Use(Unit9 target)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Delay += 0.5f;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
