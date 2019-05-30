using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
	// Token: 0x020003C3 RID: 963
	[AbilityId(AbilityId.beastmaster_call_of_the_wild_hawk)]
	public class CallOfTheWildHawk : CircleAbility
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x0000E264 File Offset: 0x0000C464
		public CallOfTheWildHawk(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "hawk_vision_tooltip");
			this.SpeedData = new SpecialData(baseAbility, "hawk_speed_tooltip");
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001015 RID: 4117 RVA: 0x0000E29A File Offset: 0x0000C49A
		public override float CastRange { get; } = 9999999f;
	}
}
