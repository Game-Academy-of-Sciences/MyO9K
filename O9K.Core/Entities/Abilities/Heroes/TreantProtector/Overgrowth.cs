using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
	// Token: 0x0200029F RID: 671
	[AbilityId(AbilityId.treant_overgrowth)]
	public class Overgrowth : AreaOfEffectAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000BE2 RID: 3042 RVA: 0x0000AB0E File Offset: 0x00008D0E
		public Overgrowth(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06000BE3 RID: 3043 RVA: 0x0000AB3B File Offset: 0x00008D3B
		public string ImmobilityModifierName { get; } = "modifier_treant_overgrowth";

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x0000AB43 File Offset: 0x00008D43
		public UnitState AppliesUnitState { get; } = 1L;
	}
}
