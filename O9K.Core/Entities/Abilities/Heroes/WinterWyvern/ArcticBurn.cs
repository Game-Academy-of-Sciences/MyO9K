using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.WinterWyvern
{
	// Token: 0x020001C1 RID: 449
	[AbilityId(AbilityId.winter_wyvern_arctic_burn)]
	public class ArcticBurn : ActiveAbility, IHasRangeIncrease
	{
		// Token: 0x06000906 RID: 2310 RVA: 0x000082D0 File Offset: 0x000064D0
		public ArcticBurn(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "attack_range_bonus");
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000907 RID: 2311 RVA: 0x000082FC File Offset: 0x000064FC
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00008304 File Offset: 0x00006504
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00008317 File Offset: 0x00006517
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x0000831F File Offset: 0x0000651F
		public string RangeModifierName { get; } = "modifier_winter_wyvern_arctic_burn_flight";

		// Token: 0x0400047D RID: 1149
		private readonly SpecialData attackRange;
	}
}
