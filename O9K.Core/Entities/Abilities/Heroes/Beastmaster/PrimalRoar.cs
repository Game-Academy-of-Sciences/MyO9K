using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
	// Token: 0x020003C6 RID: 966
	[AbilityId(AbilityId.beastmaster_primal_roar)]
	public class PrimalRoar : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x0000E2D5 File Offset: 0x0000C4D5
		public PrimalRoar(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "damage_radius");
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x0000E309 File Offset: 0x0000C509
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
