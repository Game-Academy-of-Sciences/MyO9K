using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
	// Token: 0x0200037A RID: 890
	[AbilityId(AbilityId.faceless_void_chronosphere)]
	public class Chronosphere : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F46 RID: 3910 RVA: 0x0000D7BA File Offset: 0x0000B9BA
		public Chronosphere(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700066D RID: 1645
		// (get) Token: 0x06000F47 RID: 3911 RVA: 0x0000D7DD File Offset: 0x0000B9DD
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
