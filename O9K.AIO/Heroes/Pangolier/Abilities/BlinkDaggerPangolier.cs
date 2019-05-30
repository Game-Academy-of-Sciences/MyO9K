using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Pangolier.Abilities
{
	// Token: 0x020000E4 RID: 228
	internal class BlinkDaggerPangolier : BlinkAbility
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00002F6B File Offset: 0x0000116B
		public BlinkDaggerPangolier(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000188EC File Offset: 0x00016AEC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Owner.HasModifier("modifier_pangolier_gyroshell"))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			float num = base.Owner.Distance(target);
			if (num < 300f)
			{
				return false;
			}
			if (target.HasModifier("modifier_pangolier_gyroshell_timeout") || (base.Owner.GetAngle(target.Position, false) < 1.25f && num < 800f))
			{
				return false;
			}
			Vector3 predictedPosition = target.GetPredictedPosition(0.1f);
			if (base.Ability.CastRange < base.Owner.Distance(predictedPosition))
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictedPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(predictedPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
