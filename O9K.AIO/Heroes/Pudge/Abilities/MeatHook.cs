using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;

namespace O9K.AIO.Heroes.Pudge.Abilities
{
	// Token: 0x020000D2 RID: 210
	internal class MeatHook : NukeAbility
	{
		// Token: 0x06000444 RID: 1092 RVA: 0x000032F0 File Offset: 0x000014F0
		public MeatHook(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00017400 File Offset: 0x00015600
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(target, null);
			if (base.Ability.GetPredictionOutput(predictionInput).HitChance <= 0)
			{
				return false;
			}
			if (target.Distance(base.Owner) < 300f || target.GetImmobilityDuration() > 0f)
			{
				return true;
			}
			if (!target.Equals(this.lastTarget))
			{
				this.lastTarget = target;
				this.rotationTime = Game.RawGameTime;
				this.rotation = target.BaseUnit.NetworkRotationRad;
				return false;
			}
			if ((double)Math.Abs(this.rotation - target.BaseUnit.NetworkRotationRad) > 0.1)
			{
				this.rotationTime = Game.RawGameTime;
				this.rotation = target.BaseUnit.NetworkRotationRad;
				return false;
			}
			MeatHookMenu abilitySettingsMenu = comboMenu.GetAbilitySettingsMenu<MeatHookMenu>(this);
			return this.rotationTime + abilitySettingsMenu.Delay / 1000f <= Game.RawGameTime;
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000043DF File Offset: 0x000025DF
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new MeatHookMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000174F8 File Offset: 0x000156F8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.UseAbility(targetManager, comboSleeper, aoe))
			{
				return false;
			}
			if (this.lastTarget != null)
			{
				this.lastTarget.RefreshUnitState();
				this.lastTarget = null;
			}
			float hitTime = base.Ability.GetHitTime(targetManager.Target);
			base.OrbwalkSleeper.Sleep(hitTime + base.Owner.Distance(targetManager.Target) / base.Ability.Speed);
			comboSleeper.Sleep(hitTime + 0.1f);
			return true;
		}

		// Token: 0x04000260 RID: 608
		private Unit9 lastTarget;

		// Token: 0x04000261 RID: 609
		private float rotation;

		// Token: 0x04000262 RID: 610
		private float rotationTime;
	}
}
