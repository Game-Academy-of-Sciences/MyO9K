using System;
using System.Linq;
using Ensage;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.StormSpirit;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Data;
using SharpDX;

namespace O9K.AIO.Heroes.StormSpirit.Abilities
{
	// Token: 0x02000099 RID: 153
	internal class BallLightning : BlinkAbility
	{
		// Token: 0x06000300 RID: 768 RVA: 0x00003E34 File Offset: 0x00002034
		public BallLightning(ActiveAbility ability) : base(ability)
		{
			this.ball = (BallLightning)ability;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00012430 File Offset: 0x00010630
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			BallLightningMenu abilitySettingsMenu = comboMenu.GetAbilitySettingsMenu<BallLightningMenu>(this);
			if (abilitySettingsMenu != null)
			{
				this.menuMaxCastRange = abilitySettingsMenu.MaxCastRange;
				this.maxDamage = abilitySettingsMenu.MaxDamageCombo;
			}
			return true;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00003E49 File Offset: 0x00002049
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return this.UseAbility(targetManager, comboSleeper, base.Owner.InFront(50f, 0f, true));
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00003E69 File Offset: 0x00002069
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new BallLightningMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00003E77 File Offset: 0x00002077
		public override bool ShouldCast(TargetManager targetManager)
		{
			return !base.Owner.IsInvulnerable && (targetManager.Target == null || !targetManager.Target.HasModifier("modifier_pudge_meat_hook"));
		}

		// Token: 0x06000305 RID: 773 RVA: 0x0001246C File Offset: 0x0001066C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Vector3 position = target.Position;
			Vector3 position2 = base.Owner.Position;
			float num = base.Owner.Distance(target);
			if (num > this.menuMaxCastRange)
			{
				return false;
			}
			Ability9 ability = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.storm_spirit_static_remnant);
			Ability9 ability2 = base.Owner.Abilities.FirstOrDefault((Ability9 x) => x.Id == AbilityId.storm_spirit_electric_vortex);
			float attackRange = base.Owner.GetAttackRange(target, 0f);
			bool flag = num < attackRange;
			float maxCastRange = this.ball.MaxCastRange;
			PredictionInput9 predictionInput = base.Ability.GetPredictionInput(targetManager.Target, null);
			predictionInput.CastRange = maxCastRange;
			predictionInput.Range = maxCastRange;
			predictionInput.Radius = 1f;
			predictionInput.Delay += 0.3f;
			Vector3 castPosition = base.Ability.GetPredictionOutput(predictionInput).CastPosition;
			if (ability != null && ability.CanBeCasted(true) && num > 200f)
			{
				if (((target.GetImmobilityDuration() > 0.5f && !target.HasModifier("modifier_storm_spirit_electric_vortex_pull")) || !base.Owner.IsVisibleToEnemies) && (float)this.ball.GetRemainingMana(castPosition) > ability.ManaCost)
				{
					return this.UseAbility(targetManager, comboSleeper, castPosition);
				}
				if (ability2 != null && ability2.CanBeCasted(true) && (float)this.ball.GetRemainingMana(castPosition) > ability.ManaCost + ability2.ManaCost && target.GetAngle(position2, false) > 1.5f)
				{
					return this.UseAbility(targetManager, comboSleeper, castPosition);
				}
			}
			if (!flag)
			{
				Vector3 toPosition = Vector3Extensions.Extend2D(castPosition, position2, attackRange - 100f);
				return this.UseAbility(targetManager, comboSleeper, toPosition);
			}
			if (!this.maxDamage || !base.Owner.CanAttack(target, 0f))
			{
				return false;
			}
			if (target.IsRanged || target.GetAngle(base.Owner, false) > 1f)
			{
				Vector3 vector = Vector3Extensions.Extend2D(castPosition, position2, attackRange - 50f);
				Vector3 vector2 = Vector3Extensions.Extend2D(position2, castPosition, 100f);
				Vector3 toPosition2 = (castPosition.Distance2D(vector, false) < castPosition.Distance2D(vector2, false)) ? vector : vector2;
				return this.UseAbility(targetManager, comboSleeper, toPosition2);
			}
			if (!target.IsStunned && !target.IsDisarmed)
			{
				Vector3 vector3 = Vector3Extensions.Extend2D(position, position2, attackRange - 100f);
				Vector3 vector4 = Vector3Extensions.Extend2D(position, position2, 300f);
				Vector3 toPosition3 = (position2.Distance2D(vector3, false) < position2.Distance2D(vector4, false)) ? vector3 : vector4;
				return this.UseAbility(targetManager, comboSleeper, toPosition3);
			}
			return false;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x0001273C File Offset: 0x0001093C
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (!base.Ability.UseAbility(toPosition, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(toPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x040001A8 RID: 424
		private readonly BallLightning ball;

		// Token: 0x040001A9 RID: 425
		private bool maxDamage;

		// Token: 0x040001AA RID: 426
		private float menuMaxCastRange;
	}
}
