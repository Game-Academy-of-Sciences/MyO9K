using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tidehunter
{
	// Token: 0x020002AF RID: 687
	[AbilityId(AbilityId.tidehunter_ravage)]
	public class Ravage : AreaOfEffectAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0000AE5D File Offset: 0x0000905D
		public Ravage(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06000C20 RID: 3104 RVA: 0x0000AE91 File Offset: 0x00009091
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
