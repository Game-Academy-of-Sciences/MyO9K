using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
	// Token: 0x020001DD RID: 477
	[AbilityId(AbilityId.shadow_shaman_mass_serpent_ward)]
	public class MassSerpentWard : CircleAbility
	{
		// Token: 0x06000981 RID: 2433 RVA: 0x000088E3 File Offset: 0x00006AE3
		public MassSerpentWard(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000982 RID: 2434 RVA: 0x000088F7 File Offset: 0x00006AF7
		public override float Radius { get; } = 200f;
	}
}
