using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
	// Token: 0x02000288 RID: 648
	[AbilityId(AbilityId.ursa_earthshock)]
	public class Earthshock : AreaOfEffectAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000B94 RID: 2964 RVA: 0x0000A706 File Offset: 0x00008906
		public Earthshock(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "shock_radius");
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06000B95 RID: 2965 RVA: 0x0000A72B File Offset: 0x0000892B
		public string DebuffModifierName { get; } = "modifier_ursa_earthshock";
	}
}
