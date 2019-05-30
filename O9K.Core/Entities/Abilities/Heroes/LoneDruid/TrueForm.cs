using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
	// Token: 0x02000343 RID: 835
	[AbilityId(AbilityId.lone_druid_true_form)]
	public class TrueForm : ActiveAbility, IHasRangeIncrease
	{
		// Token: 0x06000E39 RID: 3641 RVA: 0x0000C89A File Offset: 0x0000AA9A
		public TrueForm(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005E2 RID: 1506
		// (get) Token: 0x06000E3A RID: 3642 RVA: 0x0000C8B5 File Offset: 0x0000AAB5
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x170005E3 RID: 1507
		// (get) Token: 0x06000E3B RID: 3643 RVA: 0x0000C8BD File Offset: 0x0000AABD
		public float RangeIncrease
		{
			get
			{
				return -400f;
			}
		}

		// Token: 0x170005E4 RID: 1508
		// (get) Token: 0x06000E3C RID: 3644 RVA: 0x0000C8C4 File Offset: 0x0000AAC4
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x06000E3D RID: 3645 RVA: 0x0000C8CC File Offset: 0x0000AACC
		public string RangeModifierName { get; } = "modifier_lone_druid_true_form";
	}
}
