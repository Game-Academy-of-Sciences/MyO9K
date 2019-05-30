using System;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.QueenOfPain.Abilities
{
	// Token: 0x020000C5 RID: 197
	internal class BlinkQueen : BlinkAbility
	{
		// Token: 0x060003F8 RID: 1016 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkQueen(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00015F94 File Offset: 0x00014194
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			Vector3 vector = Vector3Extensions.Extend2D(base.Ability.GetPredictionOutput(predictionInput).CastPosition, base.Owner.Position, 300f);
			if (base.Owner.Distance(target.Position) < vector.Distance2D(target.Position, false))
			{
				return false;
			}
			if (base.Owner.Distance(vector) > base.Ability.CastRange || !base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(vector);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
