using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Underlord
{
	// Token: 0x0200028D RID: 653
	[AbilityId(AbilityId.abyssal_underlord_dark_rift)]
	public class DarkRift : RangedAbility
	{
		// Token: 0x06000BAA RID: 2986 RVA: 0x0000A802 File Offset: 0x00008A02
		public DarkRift(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06000BAB RID: 2987 RVA: 0x0000A816 File Offset: 0x00008A16
		public override float CastRange { get; } = 9999999f;
	}
}
