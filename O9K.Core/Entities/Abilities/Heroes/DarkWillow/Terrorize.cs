using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
	// Token: 0x0200039B RID: 923
	[AbilityId(AbilityId.dark_willow_terrorize)]
	public class Terrorize : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000FB5 RID: 4021 RVA: 0x0000DDA4 File Offset: 0x0000BFA4
		public Terrorize(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "destination_radius");
			this.SpeedData = new SpecialData(baseAbility, "destination_travel_speed");
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0000DDD8 File Offset: 0x0000BFD8
		public UnitState AppliesUnitState { get; } = 26L;
	}
}
