using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020C RID: 524
	internal class EtherealBlade : DebuffAbility
	{
		// Token: 0x06000A6A RID: 2666 RVA: 0x000034DD File Offset: 0x000016DD
		public EtherealBlade(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A6B RID: 2667 RVA: 0x000073D8 File Offset: 0x000055D8
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return this.UseAbility(targetManager, comboSleeper, false);
		}

		// Token: 0x06000A6C RID: 2668 RVA: 0x0002CD64 File Offset: 0x0002AF64
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float hitTime = base.Ability.GetHitTime(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, hitTime);
			}
			comboSleeper.Sleep(hitTime);
			base.Sleeper.Sleep(hitTime);
			return true;
		}
	}
}
