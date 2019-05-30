using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Timbersaw;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Timbersaw.Abilities
{
	// Token: 0x02000080 RID: 128
	internal class Chakram : NukeAbility
	{
		// Token: 0x06000291 RID: 657 RVA: 0x00003A02 File Offset: 0x00001C02
		public Chakram(ActiveAbility ability) : base(ability)
		{
			this.chakram = (Chakram)ability;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00003A17 File Offset: 0x00001C17
		public ReturnChakram ReturnChakram
		{
			get
			{
				return this.chakram.ReturnChakram;
			}
		}

		// Token: 0x06000293 RID: 659 RVA: 0x00003A24 File Offset: 0x00001C24
		public bool IsDamaging(TargetManager targetManager)
		{
			return !base.Sleeper.IsSleeping && targetManager.Target.Distance(this.castPosition) <= base.Ability.Radius;
		}

		// Token: 0x06000294 RID: 660 RVA: 0x00003A56 File Offset: 0x00001C56
		public bool Return()
		{
			return this.chakram.UseAbility(false, false);
		}

		// Token: 0x06000295 RID: 661 RVA: 0x000109A0 File Offset: 0x0000EBA0
		public bool ShouldReturnChakram(TargetManager targetManager, int damagingChakrams)
		{
			if (base.Sleeper.IsSleeping || !this.chakram.ReturnChakram.CanBeCasted(true))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if (this.IsDamaging(targetManager))
			{
				if (target.Health < (float)(base.Ability.GetDamage(target) / 2 * damagingChakrams))
				{
					return true;
				}
				if (target.GetAngle(this.castPosition, false) < 0.75f)
				{
					return false;
				}
				if (target.IsMoving && target.Distance(this.castPosition) > base.Ability.Radius - 50f)
				{
					return true;
				}
			}
			else if (target.GetAngle(this.castPosition, false) > 0.5f)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x00010A50 File Offset: 0x0000EC50
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
			if (target.GetAngle(base.Owner.Position, false) > 2f)
			{
				predictionInput.Delay += 0.5f;
			}
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			this.castPosition = predictionOutput.CastPosition;
			if (!base.Ability.UseAbility(this.castPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(this.castPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(base.Ability.GetHitTime(this.castPosition));
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000163 RID: 355
		private readonly Chakram chakram;

		// Token: 0x04000164 RID: 356
		private Vector3 castPosition;
	}
}
