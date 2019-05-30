using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.TemplarAssassin
{
	// Token: 0x020002B0 RID: 688
	[AbilityId(AbilityId.templar_assassin_psi_blades)]
	public class PsiBlades : PassiveAbility, IHasRangeIncrease
	{
		// Token: 0x06000C21 RID: 3105 RVA: 0x00025670 File Offset: 0x00023870
		public PsiBlades(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "bonus_attack_range");
			this.splitRange = new SpecialData(baseAbility, "attack_spill_range");
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06000C22 RID: 3106 RVA: 0x0000AE99 File Offset: 0x00009099
		public float SplitRange
		{
			get
			{
				return this.splitRange.GetValue(this.Level);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0000AEAC File Offset: 0x000090AC
		public bool IsRangeIncreasePermanent { get; } = 1;

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x06000C24 RID: 3108 RVA: 0x0000AEB4 File Offset: 0x000090B4
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0000AEC7 File Offset: 0x000090C7
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000C26 RID: 3110 RVA: 0x0000AECF File Offset: 0x000090CF
		public string RangeModifierName { get; } = "modifier_templar_assassin_psi_blades";

		// Token: 0x04000637 RID: 1591
		private readonly SpecialData attackRange;

		// Token: 0x04000638 RID: 1592
		private readonly SpecialData splitRange;
	}
}
