using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
	// Token: 0x020001DF RID: 479
	internal class PrimalSplit : UntargetableAbility
	{
		// Token: 0x0600098B RID: 2443 RVA: 0x00003F2C File Offset: 0x0000212C
		public PrimalSplit(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600098C RID: 2444 RVA: 0x00006C98 File Offset: 0x00004E98
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.Owner.Distance(targetManager.Target) < 600f;
		}
	}
}
