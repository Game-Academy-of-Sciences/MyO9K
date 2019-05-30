using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
	// Token: 0x02000373 RID: 883
	[AbilityId(AbilityId.huskar_inner_fire)]
	public class InnerFire : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000F34 RID: 3892 RVA: 0x0000D687 File Offset: 0x0000B887
		public InnerFire(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x0000D6BA File Offset: 0x0000B8BA
		public UnitState AppliesUnitState { get; } = 2L;
	}
}
