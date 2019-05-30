using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes.Unique
{
	// Token: 0x020001A9 RID: 425
	[AbilityId(AbilityId.tusk_ice_shards)]
	internal class IceShardsNuke : OldNukeAbility
	{
		// Token: 0x06000894 RID: 2196 RVA: 0x000064AE File Offset: 0x000046AE
		public IceShardsNuke(INuke ability) : base(ability)
		{
		}

		// Token: 0x06000895 RID: 2197 RVA: 0x00026D90 File Offset: 0x00024F90
		public override bool Use(Unit9 target)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Delay += 0.5f;
			Vector3 vector = Vector3Extensions.Extend2D(base.Ability.GetPredictionOutput(predictionInput).CastPosition, base.Owner.Position, -200f);
			if (base.Owner.Distance(vector) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
