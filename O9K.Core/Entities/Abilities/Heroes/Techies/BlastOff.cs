using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Techies
{
	// Token: 0x020001D1 RID: 465
	[AbilityId(AbilityId.techies_suicide)]
	public class BlastOff : CircleAbility
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x000086A8 File Offset: 0x000068A8
		public BlastOff(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "duration");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
