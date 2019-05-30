using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x02000210 RID: 528
	internal class MantaStyle : BuffAbility
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x000069AE File Offset: 0x00004BAE
		public MantaStyle(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0002D1AC File Offset: 0x0002B3AC
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return !target.IsAttackImmune && base.Owner.Distance(target) <= base.Owner.GetAttackRange(target, 200f);
		}
	}
}
