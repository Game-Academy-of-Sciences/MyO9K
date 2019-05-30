using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
	// Token: 0x0200028F RID: 655
	[AbilityId(AbilityId.abyssal_underlord_pit_of_malice)]
	public class PitOfMalice : CircleAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000BAD RID: 2989 RVA: 0x0000A81E File Offset: 0x00008A1E
		public PitOfMalice(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06000BAE RID: 2990 RVA: 0x0000A84B File Offset: 0x00008A4B
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000BAF RID: 2991 RVA: 0x0000A853 File Offset: 0x00008A53
		public string ImmobilityModifierName { get; } = "modifier_abyssal_underlord_pit_of_malice_ensare";
	}
}
