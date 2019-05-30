using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Warlock
{
	// Token: 0x02000273 RID: 627
	[AbilityId(AbilityId.warlock_upheaval)]
	public class Upheaval : CircleAbility, IDebuff, IChanneled, IActiveAbility
	{
		// Token: 0x06000B5E RID: 2910 RVA: 0x0000A3F0 File Offset: 0x000085F0
		public Upheaval(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "aoe");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x17000453 RID: 1107
		// (get) Token: 0x06000B5F RID: 2911 RVA: 0x0000A429 File Offset: 0x00008629
		public float ChannelTime { get; }

		// Token: 0x17000454 RID: 1108
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0000A431 File Offset: 0x00008631
		public string DebuffModifierName { get; } = "modifier_warlock_upheaval";

		// Token: 0x17000455 RID: 1109
		// (get) Token: 0x06000B61 RID: 2913 RVA: 0x0000A439 File Offset: 0x00008639
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
