using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Talents
{
	// Token: 0x02000102 RID: 258
	[AbilityId(AbilityId.special_bonus_attack_range_50)]
	[AbilityId(AbilityId.special_bonus_attack_range_75)]
	[AbilityId(AbilityId.special_bonus_attack_range_100)]
	[AbilityId(AbilityId.special_bonus_attack_range_125)]
	[AbilityId(AbilityId.special_bonus_attack_range_150)]
	[AbilityId(AbilityId.special_bonus_attack_range_175)]
	[AbilityId(AbilityId.special_bonus_attack_range_200)]
	[AbilityId(AbilityId.special_bonus_attack_range_250)]
	[AbilityId(AbilityId.special_bonus_attack_range_300)]
	[AbilityId(AbilityId.special_bonus_attack_range_400)]
	public class AttackRangeTalent : Talent, IHasRangeIncrease
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00006756 File Offset: 0x00004956
		public AttackRangeTalent(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "value");
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00006789 File Offset: 0x00004989
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x00006791 File Offset: 0x00004991
		public float RangeIncrease
		{
			get
			{
				if (!base.Owner.IsRanged)
				{
					return 0f;
				}
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x000067B7 File Offset: 0x000049B7
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x000067BF File Offset: 0x000049BF
		public string RangeModifierName { get; } = "modifier_special_bonus_attack_range";

		// Token: 0x040002F5 RID: 757
		private readonly SpecialData attackRange;
	}
}
