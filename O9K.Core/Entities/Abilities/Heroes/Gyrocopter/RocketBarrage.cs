using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
	// Token: 0x0200022D RID: 557
	[AbilityId(AbilityId.gyrocopter_rocket_barrage)]
	public class RocketBarrage : AreaOfEffectAbility
	{
		// Token: 0x06000A71 RID: 2673 RVA: 0x000096EE File Offset: 0x000078EE
		public RocketBarrage(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "rocket_damage");
		}
	}
}
