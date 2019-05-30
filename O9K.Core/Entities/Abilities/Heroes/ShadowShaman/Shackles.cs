using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowShaman
{
	// Token: 0x020001E0 RID: 480
	[AbilityId(AbilityId.shadow_shaman_shackles)]
	public class Shackles : RangedAbility, IDisable, IChanneled, IActiveAbility
	{
		// Token: 0x06000987 RID: 2439 RVA: 0x00008957 File Offset: 0x00006B57
		public Shackles(Ability baseAbility) : base(baseAbility)
		{
			this.channelTimeData = new SpecialData(baseAbility, "channel_time");
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00008981 File Offset: 0x00006B81
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x00008989 File Offset: 0x00006B89
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x0600098A RID: 2442 RVA: 0x0000899C File Offset: 0x00006B9C
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x040004C7 RID: 1223
		private readonly SpecialData channelTimeData;
	}
}
