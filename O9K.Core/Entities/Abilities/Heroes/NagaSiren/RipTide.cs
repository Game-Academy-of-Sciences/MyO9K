using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NagaSiren
{
	// Token: 0x02000322 RID: 802
	[AbilityId(AbilityId.naga_siren_rip_tide)]
	public class RipTide : PassiveAbility
	{
		// Token: 0x06000DD3 RID: 3539 RVA: 0x0000C38A File Offset: 0x0000A58A
		public RipTide(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
