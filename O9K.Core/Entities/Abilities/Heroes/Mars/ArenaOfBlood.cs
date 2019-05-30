using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Mars
{
	// Token: 0x02000334 RID: 820
	[AbilityId(AbilityId.mars_arena_of_blood)]
	public class ArenaOfBlood : CircleAbility
	{
		// Token: 0x06000E10 RID: 3600 RVA: 0x000099FD File Offset: 0x00007BFD
		public ArenaOfBlood(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "formation_time");
		}
	}
}
