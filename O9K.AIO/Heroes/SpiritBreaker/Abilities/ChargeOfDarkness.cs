using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.SpiritBreaker.Abilities
{
	// Token: 0x02000048 RID: 72
	internal class ChargeOfDarkness : TargetableAbility
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00002FCA File Offset: 0x000011CA
		public ChargeOfDarkness(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000DA58 File Offset: 0x0000BC58
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float num = base.Ability.GetCastDelay(targetManager.Target) + 0.5f;
			comboSleeper.Sleep(num);
			base.Sleeper.Sleep(num);
			base.OrbwalkSleeper.Sleep(num);
			return true;
		}
	}
}
