using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Tinker
{
	// Token: 0x020001CA RID: 458
	[AbilityId(AbilityId.tinker_rearm)]
	public class Rearm : ActiveAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000937 RID: 2359 RVA: 0x000084DF File Offset: 0x000066DF
		public Rearm(Ability baseAbility) : base(baseAbility)
		{
			this.channelTimeData = new SpecialData(baseAbility, "channel_tooltip");
		}

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000938 RID: 2360 RVA: 0x000084F9 File Offset: 0x000066F9
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0000850C File Offset: 0x0000670C
		public bool IsActivatesOnChannelStart { get; }

		// Token: 0x0400049D RID: 1181
		private readonly SpecialData channelTimeData;
	}
}
