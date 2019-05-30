using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Disruptor.Abilities
{
	// Token: 0x02000153 RID: 339
	internal class StaticStorm : DisableAbility
	{
		// Token: 0x060006A4 RID: 1700 RVA: 0x00003482 File Offset: 0x00001682
		public StaticStorm(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060006A5 RID: 1701 RVA: 0x000201E4 File Offset: 0x0001E3E4
		public bool UseAbility(Vector3 position, TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(position, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(targetManager.Target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(position);
			targetManager.Target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
