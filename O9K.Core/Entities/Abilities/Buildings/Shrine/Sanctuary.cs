using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Buildings.Shrine
{
	// Token: 0x020003DB RID: 987
	[AbilityId(AbilityId.filler_ability)]
	public class Sanctuary : PassiveAbility
	{
		// Token: 0x06001074 RID: 4212 RVA: 0x0000E79B File Offset: 0x0000C99B
		public Sanctuary(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DurationData = new SpecialData(baseAbility, "duration");
		}
	}
}
