using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Brewmaster.Abilities
{
	// Token: 0x020001E0 RID: 480
	internal class WindWalk : BuffAbility
	{
		// Token: 0x0600098D RID: 2445 RVA: 0x000069AE File Offset: 0x00004BAE
		public WindWalk(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600098E RID: 2446 RVA: 0x00006CB2 File Offset: 0x00004EB2
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && targetManager.Target.Distance(base.Owner) >= 400f;
		}
	}
}
