using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
	// Token: 0x0200020C RID: 524
	[AbilityId(AbilityId.lion_mana_drain)]
	public class ManaDrain : RangedAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000A0C RID: 2572 RVA: 0x0000913A File Offset: 0x0000733A
		public ManaDrain(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000A0D RID: 2573 RVA: 0x00009157 File Offset: 0x00007357
		public float ChannelTime { get; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000A0E RID: 2574 RVA: 0x0000915F File Offset: 0x0000735F
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
