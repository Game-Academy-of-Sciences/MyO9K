using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tinker
{
	// Token: 0x020001C9 RID: 457
	[AbilityId(AbilityId.tinker_march_of_the_machines)]
	public class MarchOfTheMachines : AreaOfEffectAbility
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x00006612 File Offset: 0x00004812
		public MarchOfTheMachines(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
