using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
	// Token: 0x020003CE RID: 974
	[AbilityId(AbilityId.antimage_blink)]
	public class Blink : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x06001039 RID: 4153 RVA: 0x0000E4AE File Offset: 0x0000C6AE
		public Blink(Ability baseAbility) : base(baseAbility)
		{
			this.castRangeData = new SpecialData(baseAbility, "blink_range");
		}

		// Token: 0x170006F5 RID: 1781
		// (get) Token: 0x0600103A RID: 4154 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		public BlinkType BlinkType { get; }

		// Token: 0x170006F6 RID: 1782
		// (get) Token: 0x0600103B RID: 4155 RVA: 0x0000E4D0 File Offset: 0x0000C6D0
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x04000873 RID: 2163
		private readonly SpecialData castRangeData;
	}
}
