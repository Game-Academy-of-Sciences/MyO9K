using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Debuffs.Unique
{
	// Token: 0x020001BD RID: 445
	[AbilityId(AbilityId.sniper_shrapnel)]
	internal class ShrapnelDebuff : OldDebuffAbility
	{
		// Token: 0x060008E3 RID: 2275 RVA: 0x00006786 File Offset: 0x00004986
		public ShrapnelDebuff(IDebuff ability) : base(ability)
		{
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00027EE0 File Offset: 0x000260E0
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
			base.AbilitySleeper.Sleep(base.Ability.Handle, 2f);
			return true;
		}
	}
}
