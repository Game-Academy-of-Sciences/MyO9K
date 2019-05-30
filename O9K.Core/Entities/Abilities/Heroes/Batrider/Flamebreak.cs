using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Batrider
{
	// Token: 0x0200025B RID: 603
	[AbilityId(AbilityId.batrider_flamebreak)]
	public class Flamebreak : CircleAbility
	{
		// Token: 0x06000B03 RID: 2819 RVA: 0x00009F12 File Offset: 0x00008112
		public Flamebreak(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "explosion_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}
	}
}
