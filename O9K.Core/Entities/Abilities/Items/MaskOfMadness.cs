using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000132 RID: 306
	[AbilityId(AbilityId.item_mask_of_madness)]
	public class MaskOfMadness : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x060006E9 RID: 1769 RVA: 0x000069FA File Offset: 0x00004BFA
		public MaskOfMadness(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006EA RID: 1770 RVA: 0x00006A15 File Offset: 0x00004C15
		public string BuffModifierName { get; } = "modifier_item_mask_of_madness_berserk";

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006EB RID: 1771 RVA: 0x00006A1D File Offset: 0x00004C1D
		public bool BuffsAlly { get; }

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x00006A25 File Offset: 0x00004C25
		public bool BuffsOwner { get; } = 1;
	}
}
