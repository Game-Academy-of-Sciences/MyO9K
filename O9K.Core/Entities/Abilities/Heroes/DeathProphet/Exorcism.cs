using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.DeathProphet
{
	// Token: 0x02000246 RID: 582
	[AbilityId(AbilityId.death_prophet_exorcism)]
	public class Exorcism : AreaOfEffectAbility
	{
		// Token: 0x06000AB4 RID: 2740 RVA: 0x00009A6A File Offset: 0x00007C6A
		public Exorcism(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "spirit_speed");
		}
	}
}
