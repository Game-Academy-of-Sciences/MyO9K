using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Slark.Abilities
{
	// Token: 0x020000A5 RID: 165
	internal class ShadowDance : ShieldAbility
	{
		// Token: 0x06000345 RID: 837 RVA: 0x000030D7 File Offset: 0x000012D7
		public ShadowDance(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00003FD3 File Offset: 0x000021D3
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && base.Owner.HealthPercentage <= 30f;
		}
	}
}
