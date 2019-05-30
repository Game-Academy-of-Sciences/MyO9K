using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Sniper
{
	// Token: 0x020002C9 RID: 713
	[AbilityId(AbilityId.sniper_shrapnel)]
	public class Shrapnel : CircleAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0000B4F1 File Offset: 0x000096F1
		public Shrapnel(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "damage_delay");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0000B527 File Offset: 0x00009727
		public string DebuffModifierName { get; } = "modifier_sniper_shrapnel_slow";
	}
}
