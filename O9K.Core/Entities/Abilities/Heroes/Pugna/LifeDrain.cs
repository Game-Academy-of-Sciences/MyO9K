using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Pugna
{
	// Token: 0x020001EB RID: 491
	[AbilityId(AbilityId.pugna_life_drain)]
	public class LifeDrain : RangedAbility, IChanneled, IActiveAbility
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00008B37 File Offset: 0x00006D37
		public LifeDrain(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00008B54 File Offset: 0x00006D54
		public float ChannelTime { get; }

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00008B5C File Offset: 0x00006D5C
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
