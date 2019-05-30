using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Disruptor
{
	// Token: 0x02000243 RID: 579
	[AbilityId(AbilityId.disruptor_thunder_strike)]
	public class ThunderStrike : RangedAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000AAF RID: 2735 RVA: 0x000099D2 File Offset: 0x00007BD2
		public ThunderStrike(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "strike_damage");
		}
	}
}
