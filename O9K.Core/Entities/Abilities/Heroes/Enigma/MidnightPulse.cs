using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
	// Token: 0x02000230 RID: 560
	[AbilityId(AbilityId.enigma_midnight_pulse)]
	public class MidnightPulse : CircleAbility
	{
		// Token: 0x06000A76 RID: 2678 RVA: 0x00007DDD File Offset: 0x00005FDD
		public MidnightPulse(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
