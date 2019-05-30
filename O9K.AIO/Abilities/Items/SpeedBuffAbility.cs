using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x02000213 RID: 531
	internal class SpeedBuffAbility : BuffAbility
	{
		// Token: 0x06000A7E RID: 2686 RVA: 0x000069AE File Offset: 0x00004BAE
		public SpeedBuffAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x0002D244 File Offset: 0x0002B444
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return (target.IsMoving && target.GetAngle(base.Owner.Position, false) >= 1f) || !base.Owner.CanAttack(target, 0f);
		}
	}
}
