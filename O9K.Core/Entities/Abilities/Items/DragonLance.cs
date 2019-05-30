using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200011F RID: 287
	[AbilityId(AbilityId.item_dragon_lance)]
	public class DragonLance : PassiveAbility, IHasRangeIncrease
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x000068F0 File Offset: 0x00004AF0
		public DragonLance(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "base_attack_range");
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00006923 File Offset: 0x00004B23
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000692B File Offset: 0x00004B2B
		public float RangeIncrease
		{
			get
			{
				if (!base.IsActive || !base.Owner.IsRanged)
				{
					return 0f;
				}
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00006959 File Offset: 0x00004B59
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x00006961 File Offset: 0x00004B61
		public string RangeModifierName { get; } = "modifier_item_dragon_lance";

		// Token: 0x0400030A RID: 778
		private readonly SpecialData attackRange;
	}
}
