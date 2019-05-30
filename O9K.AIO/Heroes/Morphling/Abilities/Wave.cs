using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Morphling.Abilities
{
	// Token: 0x020000F8 RID: 248
	internal class Wave : NukeAbility
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x000032F0 File Offset: 0x000014F0
		public Wave(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00004914 File Offset: 0x00002B14
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && base.Owner.Distance(targetManager.Target) >= base.Owner.GetAttackRange(targetManager.Target, 0f);
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00019D84 File Offset: 0x00017F84
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, 1, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(base.Ability.GetHitTime(targetManager.Target) + 1f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
