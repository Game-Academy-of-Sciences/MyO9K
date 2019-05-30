using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
	// Token: 0x0200028E RID: 654
	[AbilityId(AbilityId.abyssal_underlord_firestorm)]
	public class Firestorm : CircleAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x00007DDD File Offset: 0x00005FDD
		public Firestorm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
