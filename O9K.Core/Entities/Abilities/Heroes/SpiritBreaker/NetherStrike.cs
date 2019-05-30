using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.SpiritBreaker
{
	// Token: 0x020002C0 RID: 704
	[AbilityId(AbilityId.spirit_breaker_nether_strike)]
	public class NetherStrike : RangedAbility
	{
		// Token: 0x06000C71 RID: 3185 RVA: 0x0000A97B File Offset: 0x00008B7B
		public NetherStrike(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
