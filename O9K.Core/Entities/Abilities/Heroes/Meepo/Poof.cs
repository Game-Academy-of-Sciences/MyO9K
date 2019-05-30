using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Meepo
{
	// Token: 0x0200032E RID: 814
	[AbilityId(AbilityId.meepo_poof)]
	public class Poof : RangedAbility
	{
		// Token: 0x06000DFC RID: 3580 RVA: 0x0000C608 File Offset: 0x0000A808
		public Poof(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "poof_damage");
		}

		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x0000C63E File Offset: 0x0000A83E
		public override float CastRange { get; } = 9999999f;
	}
}
