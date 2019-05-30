using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables.Unique
{
	// Token: 0x020001C3 RID: 451
	[AbilityId(AbilityId.dark_willow_bramble_maze)]
	internal class BrambleMazeDisable : OldDisableAbility
	{
		// Token: 0x06000909 RID: 2313 RVA: 0x000068AD File Offset: 0x00004AAD
		public BrambleMazeDisable(IDisable ability) : base(ability)
		{
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00028704 File Offset: 0x00026904
		public override bool Use(Unit9 target)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (!target.IsMoving && base.Owner.Distance(target) < base.Ability.CastRange + 140f)
			{
				if (!base.Ability.UseAbility(Vector3Extensions.Extend2D(predictionOutput.CastPosition, base.Owner.Position, 150f), false, false))
				{
					return false;
				}
				base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
				base.AbilitySleeper.Sleep(base.Ability.Handle, 2f);
				return true;
			}
			else
			{
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
}
