using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Leshrac
{
	// Token: 0x02000214 RID: 532
	[AbilityId(AbilityId.leshrac_diabolic_edict)]
	public class DiabolicEdict : AreaOfEffectAbility
	{
		// Token: 0x06000A28 RID: 2600 RVA: 0x00006612 File Offset: 0x00004812
		public DiabolicEdict(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
