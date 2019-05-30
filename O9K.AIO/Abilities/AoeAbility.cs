using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x020001FC RID: 508
	internal class AoeAbility : UsableAbility
	{
		// Token: 0x06000A21 RID: 2593 RVA: 0x00003F23 File Offset: 0x00002123
		public AoeAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x000071B7 File Offset: 0x000053B7
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return this.UseAbility(targetManager, comboSleeper, true);
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002C088 File Offset: 0x0002A288
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (target.IsMagicImmune && !base.Ability.PiercesMagicImmunity(target))
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

		// Token: 0x06000A24 RID: 2596 RVA: 0x000244BC File Offset: 0x000226BC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
