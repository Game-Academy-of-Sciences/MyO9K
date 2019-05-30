using System;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.MonkeyKing;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.MonkeyKing.Abilities
{
	// Token: 0x020000FB RID: 251
	internal class PrimalSpring : NukeAbility
	{
		// Token: 0x06000502 RID: 1282 RVA: 0x00004973 File Offset: 0x00002B73
		public PrimalSpring(ActiveAbility ability) : base(ability)
		{
			this.primalSpring = (PrimalSpring)ability;
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x0001A11C File Offset: 0x0001831C
		public bool CancelChanneling(TargetManager targetManager)
		{
			if (!base.Ability.IsChanneling || !base.Ability.BaseAbility.IsChanneling)
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if (target.IsStunned || target.IsRooted)
			{
				return false;
			}
			float num = base.Owner.Distance(this.castPosition) / base.Ability.Speed;
			Polygon polygon = new Polygon.Circle(this.castPosition, base.Ability.Radius, 20);
			Polygon.Circle circle = new Polygon.Circle(this.castPosition, base.Ability.Radius - num * target.Speed - 75f, 20);
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Delay = this.primalSpring.ChannelTime - base.Ability.BaseAbility.ChannelTime + num;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			return ((!polygon.IsInside(predictionOutput.TargetPosition) && !circle.IsInside(target.Position) && target.GetAngle(this.castPosition, false) > 1.5f) || (float)this.primalSpring.GetCurrentDamage(target) > target.Health) && base.Owner.BaseUnit.Stop();
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x0001A258 File Offset: 0x00018458
		public bool CanHit(Unit9 target, IComboModeMenu comboMenu)
		{
			return comboMenu != null && !comboMenu.IsAbilityEnabled(base.Ability) && base.Ability.CanBeCasted(true) && base.Ability.CanHit(target) && !target.IsReflectingDamage && base.Ability.GetDamage(target) > 0 && (!target.IsInvulnerable || this.ChainStun(target, true)) && (!target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f);
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x0001A2F0 File Offset: 0x000184F0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, targetManager.EnemyHeroes);
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			this.castPosition = predictionOutput.CastPosition;
			if (!base.Ability.UseAbility(this.castPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x040002C5 RID: 709
		private readonly PrimalSpring primalSpring;

		// Token: 0x040002C6 RID: 710
		private Vector3 castPosition;
	}
}
