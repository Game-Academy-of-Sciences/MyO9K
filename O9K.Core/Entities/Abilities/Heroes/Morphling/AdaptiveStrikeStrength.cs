using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x02000202 RID: 514
	[AbilityId(AbilityId.morphling_adaptive_strike_str)]
	public class AdaptiveStrikeStrength : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x060009F1 RID: 2545 RVA: 0x00008F26 File Offset: 0x00007126
		public AdaptiveStrikeStrength(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x00008F49 File Offset: 0x00007149
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
