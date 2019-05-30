using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200016A RID: 362
	[AbilityId(AbilityId.item_aether_lens)]
	public class AetherLens : PassiveAbility, IHasRangeIncrease
	{
		// Token: 0x0600074A RID: 1866 RVA: 0x00006D1F File Offset: 0x00004F1F
		public AetherLens(Ability baseAbility) : base(baseAbility)
		{
			this.castRange = new SpecialData(baseAbility, "cast_range_bonus");
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600074B RID: 1867 RVA: 0x00006D52 File Offset: 0x00004F52
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600074C RID: 1868 RVA: 0x00006D5A File Offset: 0x00004F5A
		public float RangeIncrease
		{
			get
			{
				if (!base.IsActive)
				{
					return 0f;
				}
				return this.castRange.GetValue(this.Level);
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600074D RID: 1869 RVA: 0x00006D7B File Offset: 0x00004F7B
		public RangeIncreaseType RangeIncreaseType { get; } = 1;

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x0600074E RID: 1870 RVA: 0x00006D83 File Offset: 0x00004F83
		public string RangeModifierName { get; } = "modifier_item_aether_lens";

		// Token: 0x0400033D RID: 829
		private readonly SpecialData castRange;
	}
}
