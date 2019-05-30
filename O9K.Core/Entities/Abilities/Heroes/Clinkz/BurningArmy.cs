using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
	// Token: 0x02000252 RID: 594
	[AbilityId(AbilityId.clinkz_burning_army)]
	public class BurningArmy : RangedAbility
	{
		// Token: 0x06000AD9 RID: 2777 RVA: 0x00009D02 File Offset: 0x00007F02
		public BurningArmy(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00009D16 File Offset: 0x00007F16
		public override float Radius { get; } = 400f;
	}
}
