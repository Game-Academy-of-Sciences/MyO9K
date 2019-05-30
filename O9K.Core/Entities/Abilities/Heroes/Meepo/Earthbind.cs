using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Meepo
{
	// Token: 0x0200032F RID: 815
	[AbilityId(AbilityId.meepo_earthbind)]
	public class Earthbind : CircleAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000DFE RID: 3582 RVA: 0x0000C646 File Offset: 0x0000A846
		public Earthbind(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x170005BE RID: 1470
		// (get) Token: 0x06000DFF RID: 3583 RVA: 0x0000C684 File Offset: 0x0000A884
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x0000C68C File Offset: 0x0000A88C
		public string ImmobilityModifierName { get; } = "modifier_meepo_earthbind";
	}
}
