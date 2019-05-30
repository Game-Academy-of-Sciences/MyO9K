using System;
using System.Collections.Generic;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Windranger;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x0200003F RID: 63
	internal class Powershot : NukeAbility, IDisposable
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00002FF3 File Offset: 0x000011F3
		public Powershot(ActiveAbility ability) : base(ability)
		{
			this.powershot = (Powershot)ability;
			Player.OnExecuteOrder += this.OnExecuteOrder;
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00003019 File Offset: 0x00001219
		// (set) Token: 0x06000168 RID: 360 RVA: 0x00003021 File Offset: 0x00001221
		public Shackleshot Shackleshot { get; set; }

		// Token: 0x06000169 RID: 361 RVA: 0x0000C8E0 File Offset: 0x0000AAE0
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
			Polygon polygon = new Polygon.Rectangle(base.Owner.Position, Vector3Extensions.Extend2D(base.Owner.Position, this.castPosition, base.Ability.Range), base.Ability.Radius - 75f);
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			predictionInput.Delay = this.powershot.ChannelTime - base.Ability.BaseAbility.ChannelTime;
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			return (!polygon.IsInside(predictionOutput.TargetPosition) || (float)this.powershot.GetCurrentDamage(target) > target.Health) && base.Owner.BaseUnit.Stop();
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000302A File Offset: 0x0000122A
		public void Dispose()
		{
			Player.OnExecuteOrder -= this.OnExecuteOrder;
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C9DC File Offset: 0x0000ABDC
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Unit9 target = targetManager.Target;
			return (float)base.Ability.GetDamage(target) > target.Health || usableAbilities.Count <= 0 || target.GetImmobilityDuration() > 0.4f;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000CA20 File Offset: 0x0000AC20
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			Shackleshot shackleshot = this.Shackleshot;
			if (shackleshot != null && shackleshot.Ability.TimeSinceCasted < 0.5f)
			{
				predictionInput.Delay -= base.Ability.ActivationDelay;
			}
			PredictionOutput9 predictionOutput = base.Ability.GetPredictionOutput(predictionInput);
			if (!base.Ability.UseAbility(predictionOutput.CastPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000303D File Offset: 0x0000123D
		private void OnExecuteOrder(Player sender, ExecuteOrderEventArgs args)
		{
			if (args.IsQueued || args.OrderId != OrderId.AbilityLocation)
			{
				return;
			}
			if (args.Ability.Handle == base.Ability.Handle)
			{
				this.castPosition = args.TargetPosition;
			}
		}

		// Token: 0x040000CF RID: 207
		private readonly Powershot powershot;

		// Token: 0x040000D0 RID: 208
		private Vector3 castPosition;
	}
}
