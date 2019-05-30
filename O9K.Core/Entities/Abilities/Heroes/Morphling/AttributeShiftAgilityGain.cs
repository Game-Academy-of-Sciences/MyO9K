using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x020001FC RID: 508
	[AbilityId(AbilityId.morphling_morph_agi)]
	public class AttributeShiftAgilityGain : ToggleAbility
	{
		// Token: 0x060009DD RID: 2525 RVA: 0x00008E67 File Offset: 0x00007067
		public AttributeShiftAgilityGain(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x060009DE RID: 2526 RVA: 0x00008E77 File Offset: 0x00007077
		public override bool CanBeCastedWhileChanneling { get; } = 1;
	}
}
