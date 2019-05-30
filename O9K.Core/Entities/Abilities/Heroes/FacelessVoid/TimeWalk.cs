using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
	// Token: 0x02000379 RID: 889
	[AbilityId(AbilityId.faceless_void_time_walk)]
	public class TimeWalk : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000F43 RID: 3907 RVA: 0x0000D774 File Offset: 0x0000B974
		public TimeWalk(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.castRangeData = new SpecialData(baseAbility, "range");
		}

		// Token: 0x1700066B RID: 1643
		// (get) Token: 0x06000F44 RID: 3908 RVA: 0x0000D79F File Offset: 0x0000B99F
		public BlinkType BlinkType { get; }

		// Token: 0x1700066C RID: 1644
		// (get) Token: 0x06000F45 RID: 3909 RVA: 0x0000D7A7 File Offset: 0x0000B9A7
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x040007E4 RID: 2020
		private readonly SpecialData castRangeData;
	}
}
