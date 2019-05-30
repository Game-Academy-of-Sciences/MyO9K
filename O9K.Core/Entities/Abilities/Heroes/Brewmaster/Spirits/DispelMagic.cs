using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster.Spirits
{
	// Token: 0x020003B6 RID: 950
	[AbilityId(AbilityId.brewmaster_storm_dispel_magic)]
	public class DispelMagic : CircleAbility
	{
		// Token: 0x06000FF7 RID: 4087 RVA: 0x00007DDD File Offset: 0x00005FDD
		public DispelMagic(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
