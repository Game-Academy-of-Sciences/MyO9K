using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Talents
{
	// Token: 0x02000103 RID: 259
	[AbilityId(AbilityId.special_bonus_cast_range_50)]
	[AbilityId(AbilityId.special_bonus_cast_range_60)]
	[AbilityId(AbilityId.special_bonus_cast_range_75)]
	[AbilityId(AbilityId.special_bonus_cast_range_100)]
	[AbilityId(AbilityId.special_bonus_cast_range_125)]
	[AbilityId(AbilityId.special_bonus_cast_range_150)]
	[AbilityId(AbilityId.special_bonus_cast_range_175)]
	[AbilityId(AbilityId.special_bonus_cast_range_200)]
	[AbilityId(AbilityId.special_bonus_cast_range_250)]
	[AbilityId(AbilityId.special_bonus_cast_range_300)]
	[AbilityId(AbilityId.special_bonus_cast_range_350)]
	[AbilityId(AbilityId.special_bonus_cast_range_400)]
	public class CastRangeTalent : Talent, IHasRangeIncrease
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x000067C7 File Offset: 0x000049C7
		public CastRangeTalent(Ability baseAbility) : base(baseAbility)
		{
			this.castRange = new SpecialData(baseAbility, "value");
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x0600069F RID: 1695 RVA: 0x000067FA File Offset: 0x000049FA
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x060006A0 RID: 1696 RVA: 0x00006802 File Offset: 0x00004A02
		public float RangeIncrease
		{
			get
			{
				return this.castRange.GetValue(this.Level);
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x00006815 File Offset: 0x00004A15
		public RangeIncreaseType RangeIncreaseType { get; } = 1;

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x0000681D File Offset: 0x00004A1D
		public string RangeModifierName { get; } = "modifier_special_bonus_cast_range";

		// Token: 0x040002F9 RID: 761
		private readonly SpecialData castRange;
	}
}
