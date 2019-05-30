using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Clockwerk
{
	// Token: 0x02000251 RID: 593
	[AbilityId(AbilityId.rattletrap_rocket_flare)]
	public class RocketFlare : CircleAbility, INuke, IActiveAbility
	{
		// Token: 0x06000AD7 RID: 2775 RVA: 0x00009CC4 File Offset: 0x00007EC4
		public RocketFlare(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00009CFA File Offset: 0x00007EFA
		public override float CastRange { get; } = 9999999f;
	}
}
