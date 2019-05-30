using System;
using Ensage;

namespace O9K.Core.Entities.Abilities.Base
{
	// Token: 0x020003E6 RID: 998
	public abstract class PassiveAbility : Ability9
	{
		// Token: 0x060010F1 RID: 4337 RVA: 0x0000ED54 File Offset: 0x0000CF54
		protected PassiveAbility(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0000ED5D File Offset: 0x0000CF5D
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return this.IsReady && (base.Owner.UnitState & UnitState.PassivesDisabled) == (UnitState)0UL;
		}
	}
}
