using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.BountyHunter
{
	// Token: 0x020003BD RID: 957
	[AbilityId(AbilityId.bounty_hunter_shuriken_toss)]
	public class ShurikenToss : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06001002 RID: 4098 RVA: 0x0000E1A0 File Offset: 0x0000C3A0
		public ShurikenToss(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}
	}
}
