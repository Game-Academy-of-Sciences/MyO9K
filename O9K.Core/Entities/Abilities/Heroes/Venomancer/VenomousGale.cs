using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
	// Token: 0x02000282 RID: 642
	[AbilityId(AbilityId.venomancer_venomous_gale)]
	public class VenomousGale : LineAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B86 RID: 2950 RVA: 0x00025064 File Offset: 0x00023264
		public VenomousGale(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "strike_damage");
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0000A623 File Offset: 0x00008823
		public string DebuffModifierName { get; } = "modifier_venomancer_venomous_gale";
	}
}
