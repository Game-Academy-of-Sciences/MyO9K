using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000171 RID: 369
	[AbilityId(AbilityId.item_hand_of_midas)]
	public class HandOfMidas : RangedAbility
	{
		// Token: 0x06000764 RID: 1892 RVA: 0x00006527 File Offset: 0x00004727
		public HandOfMidas(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x00006ED9 File Offset: 0x000050D9
		public override bool TargetsEnemy { get; }
	}
}
