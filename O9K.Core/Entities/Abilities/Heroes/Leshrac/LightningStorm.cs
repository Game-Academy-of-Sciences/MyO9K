using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
	// Token: 0x02000216 RID: 534
	[AbilityId(AbilityId.leshrac_lightning_storm)]
	public class LightningStorm : RangedAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000A2F RID: 2607 RVA: 0x00009331 File Offset: 0x00007531
		public LightningStorm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000A30 RID: 2608 RVA: 0x00009356 File Offset: 0x00007556
		public string DebuffModifierName { get; } = "modifier_leshrac_lightning_storm_slow";
	}
}
