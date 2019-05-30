using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Windranger
{
	// Token: 0x020001C7 RID: 455
	[AbilityId(AbilityId.windrunner_shackleshot)]
	public class Shackleshot : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00022DA4 File Offset: 0x00020FA4
		public Shackleshot(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "arrow_speed");
			this.RangeData = new SpecialData(baseAbility, "shackle_distance");
			this.angleData = new SpecialData(baseAbility, "shackle_angle");
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000928 RID: 2344 RVA: 0x00008444 File Offset: 0x00006644
		public float ShackleRange
		{
			get
			{
				return this.RangeData.GetValue(this.Level) - 100f;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000929 RID: 2345 RVA: 0x0000845D File Offset: 0x0000665D
		public float Angle
		{
			get
			{
				return this.angleData.GetValue(this.Level);
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x0600092A RID: 2346 RVA: 0x00008470 File Offset: 0x00006670
		public override float Range
		{
			get
			{
				return base.Range + this.ShackleRange;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x0600092B RID: 2347 RVA: 0x0000847F File Offset: 0x0000667F
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x04000493 RID: 1171
		private readonly SpecialData angleData;
	}
}
