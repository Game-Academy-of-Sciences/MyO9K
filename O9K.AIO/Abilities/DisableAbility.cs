using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x020001FE RID: 510
	internal class DisableAbility : UsableAbility
	{
		// Token: 0x06000A2B RID: 2603 RVA: 0x00007200 File Offset: 0x00005400
		public DisableAbility(ActiveAbility ability) : base(ability)
		{
			this.Disable = (IDisable)ability;
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x00007215 File Offset: 0x00005415
		protected IDisable Disable { get; }

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002C284 File Offset: 0x0002A484
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002C30C File Offset: 0x0002A50C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
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
				if (this.Disable.UnitTargetCast)
				{
					return false;
				}
				if (!this.ChainStun(target, true))
				{
					return false;
				}
			}
			if ((float)base.Ability.GetDamage(target) < target.Health)
			{
				if (target.IsStunned)
				{
					return this.ChainStun(target, false);
				}
				if (target.IsHexed)
				{
					return this.ChainStun(target, false);
				}
				if (target.IsSilenced)
				{
					return !AbilityExtensions.IsSilence(this.Disable, false) || this.ChainStun(target, false);
				}
				if (target.IsRooted)
				{
					return !AbilityExtensions.IsRoot(this.Disable) || this.ChainStun(target, false);
				}
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002C418 File Offset: 0x0002A618
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (aoe)
			{
				if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
				{
					return false;
				}
			}
			else if (!base.Ability.UseAbility(targetManager.Target, 1, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			targetManager.Target.SetExpectedUnitState(this.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}
	}
}
