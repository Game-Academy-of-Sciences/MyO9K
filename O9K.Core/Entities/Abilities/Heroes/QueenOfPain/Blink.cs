using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
	// Token: 0x020001E7 RID: 487
	[AbilityId(AbilityId.queenofpain_blink)]
	public class Blink : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x0600099D RID: 2461 RVA: 0x00008A99 File Offset: 0x00006C99
		public Blink(Ability baseAbility) : base(baseAbility)
		{
			this.castRangeData = new SpecialData(baseAbility, "blink_range");
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600099E RID: 2462 RVA: 0x00008AB3 File Offset: 0x00006CB3
		public BlinkType BlinkType { get; }

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x0600099F RID: 2463 RVA: 0x00008ABB File Offset: 0x00006CBB
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x040004D6 RID: 1238
		private readonly SpecialData castRangeData;
	}
}
