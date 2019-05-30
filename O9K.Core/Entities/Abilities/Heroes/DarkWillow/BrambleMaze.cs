using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
	// Token: 0x02000398 RID: 920
	[AbilityId(AbilityId.dark_willow_bramble_maze)]
	public class BrambleMaze : CircleAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x000284B0 File Offset: 0x000266B0
		public BrambleMaze(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "placement_range");
			this.ActivationDelayData = new SpecialData(baseAbility, "initial_creation_delay");
			this.additionalActivationDelayData = new SpecialData(baseAbility, "latch_creation_delay");
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0000DCB1 File Offset: 0x0000BEB1
		public override float ActivationDelay
		{
			get
			{
				return this.ActivationDelayData.GetValue(this.Level) + this.additionalActivationDelayData.GetValue(this.Level);
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0000DCD6 File Offset: 0x0000BED6
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0000DCDE File Offset: 0x0000BEDE
		public string ImmobilityModifierName { get; } = "modifier_dark_willow_bramble_maze";

		// Token: 0x04000819 RID: 2073
		private readonly SpecialData additionalActivationDelayData;
	}
}
