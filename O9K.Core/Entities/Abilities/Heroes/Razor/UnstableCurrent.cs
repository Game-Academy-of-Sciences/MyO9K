using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
	// Token: 0x020002EA RID: 746
	[AbilityId(AbilityId.razor_unstable_current)]
	public class UnstableCurrent : PassiveAbility
	{
		// Token: 0x06000D01 RID: 3329 RVA: 0x00007FEF File Offset: 0x000061EF
		public UnstableCurrent(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
