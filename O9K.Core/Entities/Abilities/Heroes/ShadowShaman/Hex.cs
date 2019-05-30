using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
	// Token: 0x020001DF RID: 479
	[AbilityId(AbilityId.shadow_shaman_voodoo)]
	public class Hex : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000984 RID: 2436 RVA: 0x0000892A File Offset: 0x00006B2A
		public Hex(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000985 RID: 2437 RVA: 0x00008947 File Offset: 0x00006B47
		public UnitState AppliesUnitState { get; } = 74L;

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000986 RID: 2438 RVA: 0x0000894F File Offset: 0x00006B4F
		public string ImmobilityModifierName { get; } = "modifier_shadow_shaman_voodoo";
	}
}
