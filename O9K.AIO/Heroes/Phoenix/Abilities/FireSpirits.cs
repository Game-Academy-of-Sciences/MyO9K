using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Phoenix.Abilities
{
	// Token: 0x020000D6 RID: 214
	internal class FireSpirits : DebuffAbility
	{
		// Token: 0x06000464 RID: 1124 RVA: 0x000034DD File Offset: 0x000016DD
		public FireSpirits(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x00017BF8 File Offset: 0x00015DF8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.IsUsable && base.Ability.UseAbility(false, false))
			{
				comboSleeper.Sleep(0.1f);
				return true;
			}
			if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(base.Ability.GetHitTime(targetManager.Target) + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
