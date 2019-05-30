using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Io
{
	// Token: 0x0200022A RID: 554
	[AbilityId(AbilityId.wisp_tether)]
	public class Tether : RangedAbility
	{
		// Token: 0x06000A6D RID: 2669 RVA: 0x00009669 File Offset: 0x00007869
		public Tether(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "latch_speed");
		}
	}
}
