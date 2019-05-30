using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.LegionCommander.Abilities
{
	// Token: 0x02000125 RID: 293
	internal class Duel : TargetableAbility
	{
		// Token: 0x060005DD RID: 1501 RVA: 0x00002FCA File Offset: 0x000011CA
		public Duel(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x0001D3C0 File Offset: 0x0001B5C0
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsVisible && !target.IsInvulnerable && !target.IsLinkensProtected;
		}
	}
}
