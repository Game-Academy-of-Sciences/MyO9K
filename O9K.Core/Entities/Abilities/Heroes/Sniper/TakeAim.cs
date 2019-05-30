using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
	// Token: 0x020002C8 RID: 712
	[AbilityId(AbilityId.sniper_take_aim)]
	public class TakeAim : ActiveAbility, IHasRangeIncrease
	{
		// Token: 0x06000C87 RID: 3207 RVA: 0x0000B493 File Offset: 0x00009693
		public TakeAim(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000C88 RID: 3208 RVA: 0x0000B4C6 File Offset: 0x000096C6
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x0000B4CE File Offset: 0x000096CE
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0000B4E1 File Offset: 0x000096E1
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0000B4E9 File Offset: 0x000096E9
		public string RangeModifierName { get; } = "modifier_sniper_take_aim";

		// Token: 0x04000676 RID: 1654
		private readonly SpecialData attackRange;
	}
}
