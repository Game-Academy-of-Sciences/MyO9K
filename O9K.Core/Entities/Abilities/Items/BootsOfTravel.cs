using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200016D RID: 365
	[AbilityId(AbilityId.item_travel_boots)]
	[AbilityId(AbilityId.item_travel_boots_2)]
	public class BootsOfTravel : RangedAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000759 RID: 1881 RVA: 0x00006E33 File Offset: 0x00005033
		public BootsOfTravel(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600075A RID: 1882 RVA: 0x00006E54 File Offset: 0x00005054
		public override float CastRange { get; } = 9999999f;

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600075B RID: 1883 RVA: 0x00006E5C File Offset: 0x0000505C
		public float ChannelTime { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600075C RID: 1884 RVA: 0x00006E64 File Offset: 0x00005064
		public bool IsActivatesOnChannelStart { get; }
	}
}
