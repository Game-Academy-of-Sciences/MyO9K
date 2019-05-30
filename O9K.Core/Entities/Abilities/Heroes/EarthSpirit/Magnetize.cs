using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EarthSpirit
{
	// Token: 0x02000236 RID: 566
	[AbilityId(AbilityId.earth_spirit_magnetize)]
	public class Magnetize : AreaOfEffectAbility
	{
		// Token: 0x06000A88 RID: 2696 RVA: 0x00009862 File Offset: 0x00007A62
		public Magnetize(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "cast_radius");
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x0000987C File Offset: 0x00007A7C
		public override float CastRange { get; }
	}
}
