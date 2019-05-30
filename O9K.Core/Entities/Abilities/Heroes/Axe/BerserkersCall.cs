using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Axe
{
	// Token: 0x020003CB RID: 971
	[AbilityId(AbilityId.axe_berserkers_call)]
	public class BerserkersCall : AreaOfEffectAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x0600102D RID: 4141 RVA: 0x0000E3D1 File Offset: 0x0000C5D1
		public BerserkersCall(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170006EF RID: 1775
		// (get) Token: 0x0600102E RID: 4142 RVA: 0x0000E3FF File Offset: 0x0000C5FF
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170006F0 RID: 1776
		// (get) Token: 0x0600102F RID: 4143 RVA: 0x0000E407 File Offset: 0x0000C607
		public string ImmobilityModifierName { get; } = "modifier_axe_berserkers_call";
	}
}
