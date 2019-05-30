using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Tusk.Abilities
{
	// Token: 0x02000069 RID: 105
	internal class Snowball : TargetableAbility
	{
		// Token: 0x0600022D RID: 557 RVA: 0x00002FCA File Offset: 0x000011CA
		public Snowball(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000F6B4 File Offset: 0x0000D8B4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float num = base.Ability.GetCastDelay(targetManager.Target);
			if (!base.Ability.IsUsable)
			{
				num += 0.5f;
			}
			comboSleeper.Sleep(num);
			base.Sleeper.Sleep(num + 0.1f);
			base.OrbwalkSleeper.Sleep(num);
			return true;
		}
	}
}
