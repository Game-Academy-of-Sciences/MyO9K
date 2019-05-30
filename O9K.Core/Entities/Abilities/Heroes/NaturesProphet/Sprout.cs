using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.NaturesProphet
{
	// Token: 0x020001FA RID: 506
	[AbilityId(AbilityId.furion_sprout)]
	public class Sprout : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x060009D8 RID: 2520 RVA: 0x00008E10 File Offset: 0x00007010
		public Sprout(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x060009D9 RID: 2521 RVA: 0x00008E21 File Offset: 0x00007021
		public UnitState AppliesUnitState { get; } = 1L;
	}
}
