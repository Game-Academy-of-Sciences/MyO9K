using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
	// Token: 0x02000225 RID: 549
	[AbilityId(AbilityId.jakiro_ice_path)]
	public class IcePath : LineAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A5F RID: 2655 RVA: 0x00009584 File Offset: 0x00007784
		public IcePath(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "path_radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "path_delay");
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x000095B8 File Offset: 0x000077B8
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
