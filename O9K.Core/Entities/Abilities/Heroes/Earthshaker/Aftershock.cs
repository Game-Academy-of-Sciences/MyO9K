using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Earthshaker
{
	// Token: 0x02000239 RID: 569
	[AbilityId(AbilityId.earthshaker_aftershock)]
	public class Aftershock : PassiveAbility, IHasRadius
	{
		// Token: 0x06000A90 RID: 2704 RVA: 0x000098AF File Offset: 0x00007AAF
		public Aftershock(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aftershock_range");
		}
	}
}
