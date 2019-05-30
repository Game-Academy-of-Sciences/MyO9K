using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Dazzle
{
	// Token: 0x020001AF RID: 431
	[AbilityId(AbilityId.dazzle_bad_juju)]
	public class BadJuju : PassiveAbility
	{
		// Token: 0x060008C9 RID: 2249 RVA: 0x00007FEF File Offset: 0x000061EF
		public BadJuju(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
