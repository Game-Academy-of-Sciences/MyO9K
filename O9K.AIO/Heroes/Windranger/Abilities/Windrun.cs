using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x02000043 RID: 67
	internal class Windrun : ShieldAbility
	{
		// Token: 0x0600017B RID: 379 RVA: 0x000030D7 File Offset: 0x000012D7
		public Windrun(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600017C RID: 380 RVA: 0x0000D584 File Offset: 0x0000B784
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			if (target.IsStunned || target.IsHexed || target.IsDisarmed)
			{
				return false;
			}
			float num = base.Owner.Distance(target);
			float attackRange = base.Owner.GetAttackRange(target, 0f);
			return (num > attackRange + 100f && num < attackRange + 500f) || num < base.Ability.Radius + 100f || target.HealthPercentage < 50f;
		}
	}
}
