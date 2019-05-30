using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
	// Token: 0x02000224 RID: 548
	[AbilityId(AbilityId.jakiro_macropyre)]
	public class Macropyre : LineAbility
	{
		// Token: 0x06000A5E RID: 2654 RVA: 0x0000956A File Offset: 0x0000776A
		public Macropyre(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "path_radius");
		}
	}
}
