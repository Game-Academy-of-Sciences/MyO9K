using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Axe.Abilities
{
	// Token: 0x020001E7 RID: 487
	internal class CullingBlade : NukeAbility
	{
		// Token: 0x060009B1 RID: 2481 RVA: 0x000032F0 File Offset: 0x000014F0
		public CullingBlade(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060009B2 RID: 2482 RVA: 0x0002A3E8 File Offset: 0x000285E8
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (targetManager.Target.IsCourier && targetManager.Target.IsVisible && !targetManager.Target.IsInvulnerable)
			{
				return true;
			}
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			float num = target.Health + target.HealthRegeneration * base.Ability.CastPoint + 15f;
			return (float)base.Ability.GetDamage(target) >= num;
		}
	}
}
