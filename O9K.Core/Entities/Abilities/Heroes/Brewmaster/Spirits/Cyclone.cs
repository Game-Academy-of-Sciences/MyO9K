using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
	// Token: 0x020003B4 RID: 948
	[AbilityId(AbilityId.brewmaster_storm_cyclone)]
	public class Cyclone : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000FF3 RID: 4083 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		public Cyclone(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x0000E100 File Offset: 0x0000C300
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06000FF5 RID: 4085 RVA: 0x0000E108 File Offset: 0x0000C308
		public string ImmobilityModifierName { get; } = "modifier_brewmaster_storm_cyclone";
	}
}
