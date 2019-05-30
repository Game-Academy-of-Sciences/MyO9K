using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Rubick
{
	// Token: 0x020002E3 RID: 739
	[AbilityId(AbilityId.rubick_telekinesis)]
	public class Telekinesis : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000CDE RID: 3294 RVA: 0x0000B8CE File Offset: 0x00009ACE
		public Telekinesis(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.landDistanceData = new SpecialData(baseAbility, "max_land_distance");
		}

		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0000B902 File Offset: 0x00009B02
		public override float Radius
		{
			get
			{
				return base.Radius + this.landDistanceData.GetValue(this.Level);
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x0000B91C File Offset: 0x00009B1C
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x040006A8 RID: 1704
		private readonly SpecialData landDistanceData;
	}
}
