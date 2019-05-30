using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;
using O9K.Core.Prediction.Collision;

namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
	// Token: 0x02000237 RID: 567
	[AbilityId(AbilityId.earth_spirit_rolling_boulder)]
	public class RollingBoulder : LineAbility, IBlink, IDisable, IActiveAbility
	{
		// Token: 0x06000A8A RID: 2698 RVA: 0x000241C4 File Offset: 0x000223C4
		public RollingBoulder(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.castRangeData = new SpecialData(baseAbility, "distance");
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x00009884 File Offset: 0x00007A84
		public override CollisionTypes CollisionTypes { get; } = 16;

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x0000988C File Offset: 0x00007A8C
		public BlinkType BlinkType { get; }

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00009894 File Offset: 0x00007A94
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x0000989C File Offset: 0x00007A9C
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x04000554 RID: 1364
		private readonly SpecialData castRangeData;
	}
}
