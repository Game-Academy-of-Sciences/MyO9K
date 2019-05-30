using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Luna
{
	// Token: 0x0200033F RID: 831
	[AbilityId(AbilityId.luna_lucent_beam)]
	public class LucentBeam : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000E31 RID: 3633 RVA: 0x0000C83C File Offset: 0x0000AA3C
		public LucentBeam(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "beam_damage");
		}

		// Token: 0x170005DE RID: 1502
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0000C85F File Offset: 0x0000AA5F
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
