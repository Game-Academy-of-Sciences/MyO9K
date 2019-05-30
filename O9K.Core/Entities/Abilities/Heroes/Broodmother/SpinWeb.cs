using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Broodmother
{
	// Token: 0x020003A9 RID: 937
	[AbilityId(AbilityId.broodmother_spin_web)]
	public class SpinWeb : CircleAbility
	{
		// Token: 0x06000FD8 RID: 4056 RVA: 0x00007DDD File Offset: 0x00005FDD
		public SpinWeb(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}
	}
}
