using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020F RID: 527
	internal class HurricanePike : ForceStaff
	{
		// Token: 0x06000A75 RID: 2677 RVA: 0x000073E3 File Offset: 0x000055E3
		public HurricanePike(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x0002D0F4 File Offset: 0x0002B2F4
		public override bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			if (target.IsRanged)
			{
				return false;
			}
			if (target.IsLinkensProtected || target.IsInvulnerable || target.IsUntargetable)
			{
				return false;
			}
			if (target.IsStunned || target.IsHexed || target.IsRooted || target.IsDisarmed)
			{
				return false;
			}
			if (!base.Ability.CanHit(target))
			{
				return false;
			}
			if (base.Owner.Distance(target) > 350f)
			{
				return false;
			}
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			return true;
		}
	}
}
