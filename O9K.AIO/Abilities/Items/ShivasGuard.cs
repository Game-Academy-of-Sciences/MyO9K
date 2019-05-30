using System;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x02000214 RID: 532
	internal class ShivasGuard : DebuffAbility
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x000034DD File Offset: 0x000016DD
		public ShivasGuard(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A81 RID: 2689 RVA: 0x0002D29C File Offset: 0x0002B49C
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			Unit9 target = targetManager.Target;
			return base.Owner.Distance(target) <= 600f && (!target.IsMagicImmune || base.Ability.PiercesMagicImmunity(target));
		}
	}
}
