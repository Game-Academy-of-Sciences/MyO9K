using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Luna
{
	// Token: 0x0200033E RID: 830
	[AbilityId(AbilityId.luna_eclipse)]
	public class Eclipse : AreaOfEffectAbility
	{
		// Token: 0x06000E30 RID: 3632 RVA: 0x00006612 File Offset: 0x00004812
		public Eclipse(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
