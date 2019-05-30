using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000201 RID: 513
	internal class KillStealNukeAbility : NukeAbility
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x000032F0 File Offset: 0x000014F0
		public KillStealNukeAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x0000728C File Offset: 0x0000548C
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && (float)base.Ability.GetDamage(targetManager.Target) > targetManager.Target.Health;
		}
	}
}
