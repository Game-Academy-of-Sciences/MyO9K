using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000204 RID: 516
	internal class MoveBuffAbility : BuffAbility
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x000069AE File Offset: 0x00004BAE
		public MoveBuffAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x0002CA9C File Offset: 0x0002AC9C
		public override bool ShouldCast(TargetManager targetManager)
		{
			ToggleAbility toggleAbility;
			return !targetManager.Owner.Hero.HasModifier(base.Buff.BuffModifierName) && ((toggleAbility = (base.Buff as ToggleAbility)) == null || !toggleAbility.Enabled);
		}
	}
}
