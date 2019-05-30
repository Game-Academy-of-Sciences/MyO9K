using System;
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003F6 RID: 1014
	public interface IHasRangeIncrease
	{
		// Token: 0x17000775 RID: 1909
		// (get) Token: 0x0600111C RID: 4380
		bool IsRangeIncreasePermanent { get; }

		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x0600111D RID: 4381
		bool IsValid { get; }

		// Token: 0x17000777 RID: 1911
		// (get) Token: 0x0600111E RID: 4382
		float RangeIncrease { get; }

		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x0600111F RID: 4383
		RangeIncreaseType RangeIncreaseType { get; }

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x06001120 RID: 4384
		string RangeModifierName { get; }
	}
}
