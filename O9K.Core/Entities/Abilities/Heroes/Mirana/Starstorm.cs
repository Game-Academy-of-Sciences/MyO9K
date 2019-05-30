using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Mirana
{
	// Token: 0x02000207 RID: 519
	[AbilityId(AbilityId.mirana_starfall)]
	public class Starstorm : AreaOfEffectAbility, INuke, IActiveAbility
	{
		// Token: 0x060009FF RID: 2559 RVA: 0x00009060 File Offset: 0x00007260
		public Starstorm(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "starfall_radius");
		}
	}
}
