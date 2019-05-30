using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
	// Token: 0x02000337 RID: 823
	[AbilityId(AbilityId.mars_spear)]
	public class SpearOfMars : LineAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000E1E RID: 3614 RVA: 0x000273A8 File Offset: 0x000255A8
		public SpearOfMars(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.SpeedData = new SpecialData(baseAbility, "spear_speed");
			this.RadiusData = new SpecialData(baseAbility, "spear_width");
			this.RangeData = new SpecialData(baseAbility, "spear_range");
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06000E1F RID: 3615 RVA: 0x0000C7A4 File Offset: 0x0000A9A4
		public override CollisionTypes CollisionTypes { get; } = 16;

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		public override bool HasAreaOfEffect { get; }

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x06000E21 RID: 3617 RVA: 0x0000C7B4 File Offset: 0x0000A9B4
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00006E98 File Offset: 0x00005098
		protected override float BaseCastRange
		{
			get
			{
				return this.RangeData.GetValue(this.Level);
			}
		}
	}
}
