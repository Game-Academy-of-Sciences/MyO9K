using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tiny
{
	// Token: 0x020002A5 RID: 677
	[AbilityId(AbilityId.tiny_avalanche)]
	public class Avalanche : CircleAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000C04 RID: 3076 RVA: 0x0000ACB2 File Offset: 0x00008EB2
		public Avalanche(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "avalanche_damage");
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000C05 RID: 3077 RVA: 0x0000ACE6 File Offset: 0x00008EE6
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
