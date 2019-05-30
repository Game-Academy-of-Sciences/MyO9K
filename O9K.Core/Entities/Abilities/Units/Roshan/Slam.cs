using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.Roshan
{
	// Token: 0x020000E3 RID: 227
	[AbilityId(AbilityId.roshan_slam)]
	public class Slam : AreaOfEffectAbility
	{
		// Token: 0x0600066C RID: 1644 RVA: 0x00006555 File Offset: 0x00004755
		public Slam(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
