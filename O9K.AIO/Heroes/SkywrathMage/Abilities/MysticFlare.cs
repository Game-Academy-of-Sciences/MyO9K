using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.SkywrathMage.Abilities
{
	// Token: 0x020000AC RID: 172
	internal class MysticFlare : NukeAbility
	{
		// Token: 0x06000376 RID: 886 RVA: 0x000032F0 File Offset: 0x000014F0
		public MysticFlare(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00013C48 File Offset: 0x00011E48
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if (target.Speed <= 200f)
			{
				return true;
			}
			if (target.GetImmobilityDuration() < 0.3f)
			{
				float num = base.Ability.Radius / target.Speed + 0.3f;
				if ((float)base.Ability.GetDamage(target) * num < target.Health)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00013CB8 File Offset: 0x00011EB8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			if (base.Owner.HasAghanimsScepter && (target.GetImmobilityDuration() > 0f || !target.IsMoving) && !targetManager.EnemyHeroes.Any((Unit9 x) => !x.Equals(target) && x.Distance(target) < 900f))
			{
				Vector3 vector = target.InFront(base.Ability.Radius + (float)(target.IsMoving ? 30 : 10), 0f, true);
				if (base.Owner.Distance(vector) > base.Ability.CastRange)
				{
					return false;
				}
				if (!base.Ability.UseAbility(vector, false, false))
				{
					return false;
				}
			}
			else
			{
				PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
				predictionInput.Delay += 0.3f;
				PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
				if (predictionOutput.HitChance < 1)
				{
					return false;
				}
				if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
				{
					return false;
				}
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
