using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000202 RID: 514
	internal class NukeAbility : UsableAbility
	{
		// Token: 0x06000A3E RID: 2622 RVA: 0x000072BB File Offset: 0x000054BB
		public NukeAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A3F RID: 2623 RVA: 0x0002C4C0 File Offset: 0x0002A6C0
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

		// Token: 0x06000A40 RID: 2624 RVA: 0x0002C89C File Offset: 0x0002AA9C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (target.IsReflectingDamage)
			{
				return false;
			}
			if (base.Ability.BreaksLinkens && target.IsBlockingAbilities)
			{
				return false;
			}
			if (base.Ability.GetDamage(target) <= 0 && !target.HasModifier(this.breakShields))
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (base.Ability.UnitTargetCast)
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

		// Token: 0x06000A41 RID: 2625 RVA: 0x0002C950 File Offset: 0x0002AB50
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

		// Token: 0x04000557 RID: 1367
		private string[] breakShields = new string[]
		{
			"modifier_ember_spirit_flame_guard",
			"modifier_item_pipe_barrier",
			"modifier_abaddon_aphotic_shield",
			"modifier_oracle_false_promise_timer"
		};
	}
}
