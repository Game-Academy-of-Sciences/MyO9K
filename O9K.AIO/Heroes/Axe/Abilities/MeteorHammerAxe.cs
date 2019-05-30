using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Axe.Abilities
{
	// Token: 0x020001E8 RID: 488
	internal class MeteorHammerAxe : DisableAbility
	{
		// Token: 0x060009B3 RID: 2483 RVA: 0x00003482 File Offset: 0x00001682
		public MeteorHammerAxe(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060009B4 RID: 2484 RVA: 0x0002A464 File Offset: 0x00028664
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, targetManager.EnemyHeroes);
			predictionInput.Delay -= ((IChanneled)base.Ability).ChannelTime;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (predictionOutput.HitChance < 1)
			{
				return false;
			}
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(predictionOutput.CastPosition) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(predictionOutput.CastPosition);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}

		// Token: 0x060009B5 RID: 2485 RVA: 0x00002E73 File Offset: 0x00001073
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			return true;
		}
	}
}
