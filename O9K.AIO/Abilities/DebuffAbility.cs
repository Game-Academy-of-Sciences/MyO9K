using System;
using Ensage;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x020001FF RID: 511
	internal class DebuffAbility : UsableAbility
	{
		// Token: 0x06000A30 RID: 2608 RVA: 0x0000721D File Offset: 0x0000541D
		public DebuffAbility(ActiveAbility ability) : base(ability)
		{
			this.Debuff = (IDebuff)ability;
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000A31 RID: 2609 RVA: 0x00007248 File Offset: 0x00005448
		protected IDebuff Debuff { get; }

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, base.Ability.GetHitTime(targetManager.Target));
			}
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002C550 File Offset: 0x0002A750
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			bool isVisible = target.IsVisible;
			if (base.Ability.UnitTargetCast && !isVisible)
			{
				return false;
			}
			if (isVisible)
			{
				if (this.visibleSleeper.IsSleeping(target.Handle))
				{
					return false;
				}
				Modifier modifier = target.GetModifier(this.Debuff.DebuffModifierName);
				if (modifier != null)
				{
					float num = modifier.RemainingTime - base.Ability.GetHitTime(target);
					if (num > 0f)
					{
						this.debuffSleeper.Sleep(target.Handle, num);
						return false;
					}
				}
			}
			else
			{
				this.visibleSleeper.Sleep(target.Handle, 0.1f);
				if (this.debuffSleeper.IsSleeping(target.Handle))
				{
					return false;
				}
			}
			if (base.Ability.BreaksLinkens && target.IsBlockingAbilities)
			{
				return false;
			}
			if (target.IsDarkPactProtected)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (this.Debuff.UnitTargetCast)
				{
					return false;
				}
				if (!this.ChainStun(target, true))
				{
					return false;
				}
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x0002C674 File Offset: 0x0002A874
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, base.Ability.GetHitTime(targetManager.Target));
			}
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000554 RID: 1364
		private readonly MultiSleeper debuffSleeper = new MultiSleeper();

		// Token: 0x04000555 RID: 1365
		private readonly MultiSleeper visibleSleeper = new MultiSleeper();
	}
}
