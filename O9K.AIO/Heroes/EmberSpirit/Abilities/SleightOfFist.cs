using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
	// Token: 0x02000187 RID: 391
	internal class SleightOfFist : NukeAbility
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x000032F0 File Offset: 0x000014F0
		public SleightOfFist(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000244BC File Offset: 0x000226BC
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
