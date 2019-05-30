using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.BountyHunter
{
	// Token: 0x020003BC RID: 956
	[AbilityId(AbilityId.bounty_hunter_wind_walk)]
	public class ShadowWalk : ActiveAbility
	{
		// Token: 0x06001001 RID: 4097 RVA: 0x0000E17F File Offset: 0x0000C37F
		public ShadowWalk(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.ActivationDelayData = new SpecialData(baseAbility, "fade_time");
		}
	}
}
