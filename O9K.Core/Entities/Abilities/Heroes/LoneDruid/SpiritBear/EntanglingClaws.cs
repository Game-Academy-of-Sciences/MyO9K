using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid.SpiritBear
{
	// Token: 0x02000348 RID: 840
	[AbilityId(AbilityId.lone_druid_spirit_bear_entangle)]
	public class EntanglingClaws : PassiveAbility, IAppliesImmobility
	{
		// Token: 0x06000E46 RID: 3654 RVA: 0x0000C932 File Offset: 0x0000AB32
		public EntanglingClaws(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06000E47 RID: 3655 RVA: 0x0000C946 File Offset: 0x0000AB46
		public string ImmobilityModifierName { get; } = "modifier_lone_druid_spirit_bear_entangle_effect";
	}
}
