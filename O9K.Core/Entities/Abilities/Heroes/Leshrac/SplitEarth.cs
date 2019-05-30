using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
	// Token: 0x02000217 RID: 535
	[AbilityId(AbilityId.leshrac_split_earth)]
	public class SplitEarth : CircleAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000A31 RID: 2609 RVA: 0x0000935E File Offset: 0x0000755E
		public SplitEarth(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000A32 RID: 2610 RVA: 0x00009392 File Offset: 0x00007592
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
