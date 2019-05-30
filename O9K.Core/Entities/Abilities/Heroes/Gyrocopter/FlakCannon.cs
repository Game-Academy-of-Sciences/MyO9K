using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
	// Token: 0x0200022C RID: 556
	[AbilityId(AbilityId.gyrocopter_flak_cannon)]
	public class FlakCannon : AreaOfEffectAbility
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x00006612 File Offset: 0x00004812
		public FlakCannon(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
