using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.BountyHunter
{
	// Token: 0x020003BE RID: 958
	[AbilityId(AbilityId.bounty_hunter_track)]
	public class Track : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06001003 RID: 4099 RVA: 0x0000E1CB File Offset: 0x0000C3CB
		public Track(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0000E1DF File Offset: 0x0000C3DF
		public string DebuffModifierName { get; } = "modifier_bounty_hunter_track";
	}
}
