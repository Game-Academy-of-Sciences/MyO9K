using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200018E RID: 398
	[AbilityId(AbilityId.item_heavens_halberd)]
	public class HeavensHalberd : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0000776B File Offset: 0x0000596B
		public HeavensHalberd(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000808 RID: 2056 RVA: 0x0000777C File Offset: 0x0000597C
		public UnitState AppliesUnitState { get; } = 2L;
	}
}
