using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
	// Token: 0x02000345 RID: 837
	[AbilityId(AbilityId.lone_druid_savage_roar)]
	[AbilityId(AbilityId.lone_druid_savage_roar_bear)]
	public class SavageRoar : AreaOfEffectAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x0000C907 File Offset: 0x0000AB07
		public SavageRoar(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x06000E43 RID: 3651 RVA: 0x0000C92A File Offset: 0x0000AB2A
		public UnitState AppliesUnitState { get; } = 26L;
	}
}
