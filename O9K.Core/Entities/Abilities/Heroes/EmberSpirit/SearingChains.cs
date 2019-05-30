using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
	// Token: 0x02000384 RID: 900
	[AbilityId(AbilityId.ember_spirit_searing_chains)]
	public class SearingChains : RangedAreaOfEffectAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000F72 RID: 3954 RVA: 0x0000DA0D File Offset: 0x0000BC0D
		public SearingChains(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06000F73 RID: 3955 RVA: 0x0000DA3A File Offset: 0x0000BC3A
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06000F74 RID: 3956 RVA: 0x0000DA42 File Offset: 0x0000BC42
		public string ImmobilityModifierName { get; } = "modifier_ember_spirit_searing_chains";
	}
}
