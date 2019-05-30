using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Lina
{
	// Token: 0x0200034D RID: 845
	[AbilityId(AbilityId.lina_light_strike_array)]
	public class LightStrikeArray : CircleAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x00027414 File Offset: 0x00025614
		public LightStrikeArray(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "light_strike_array_aoe");
			this.ActivationDelayData = new SpecialData(baseAbility, "light_strike_array_delay_time");
			this.DamageData = new SpecialData(baseAbility, "light_strike_array_damage");
		}

		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06000E52 RID: 3666 RVA: 0x0000C9FF File Offset: 0x0000ABFF
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
