using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
	// Token: 0x02000248 RID: 584
	[AbilityId(AbilityId.death_prophet_carrion_swarm)]
	public class CryptSwarm : ConeAbility, INuke, IActiveAbility
	{
		// Token: 0x06000AB7 RID: 2743 RVA: 0x000247A0 File Offset: 0x000229A0
		public CryptSwarm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
			this.RangeData = new SpecialData(baseAbility, "range");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}
	}
}
