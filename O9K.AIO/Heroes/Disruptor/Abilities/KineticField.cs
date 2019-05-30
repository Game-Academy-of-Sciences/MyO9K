using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Disruptor.Abilities
{
	// Token: 0x02000152 RID: 338
	internal class KineticField : AoeAbility
	{
		// Token: 0x0600069F RID: 1695 RVA: 0x0000356A File Offset: 0x0000176A
		public KineticField(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00005608 File Offset: 0x00003808
		// (set) Token: 0x060006A1 RID: 1697 RVA: 0x00005610 File Offset: 0x00003810
		public Vector3 CastPosition { get; private set; }

		// Token: 0x060006A2 RID: 1698 RVA: 0x000200F0 File Offset: 0x0001E2F0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			this.CastPosition = predictionOutput.CastPosition;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x060006A3 RID: 1699 RVA: 0x00020188 File Offset: 0x0001E388
		public bool UseAbility(Vector3 position, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			this.CastPosition = position;
			float castDelay = base.Ability.GetCastDelay(position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
