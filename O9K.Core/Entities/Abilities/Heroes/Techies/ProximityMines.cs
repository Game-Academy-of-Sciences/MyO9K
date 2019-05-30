using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
	// Token: 0x020001D2 RID: 466
	[AbilityId(AbilityId.techies_land_mines)]
	public class ProximityMines : CircleAbility
	{
		// Token: 0x06000958 RID: 2392 RVA: 0x000086E4 File Offset: 0x000068E4
		public ProximityMines(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "activation_delay");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
