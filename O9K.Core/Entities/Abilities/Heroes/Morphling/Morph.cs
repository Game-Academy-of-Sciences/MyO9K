using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x020001FF RID: 511
	[AbilityId(AbilityId.morphling_replicate)]
	public class Morph : RangedAbility
	{
		// Token: 0x060009EC RID: 2540 RVA: 0x00006527 File Offset: 0x00004727
		public Morph(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700038F RID: 911
		// (get) Token: 0x060009ED RID: 2541 RVA: 0x00008F06 File Offset: 0x00007106
		public bool CanTargetAlly
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_morphling_5);
				return abilityById != null && abilityById.Level > 0u;
			}
		}
	}
}
