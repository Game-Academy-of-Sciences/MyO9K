using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000206 RID: 518
	internal class UntargetableAbility : UsableAbility
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00003F23 File Offset: 0x00002123
		public UntargetableAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x000071B7 File Offset: 0x000053B7
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return this.UseAbility(targetManager, comboSleeper, true);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x0002CB28 File Offset: 0x0002AD28
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
