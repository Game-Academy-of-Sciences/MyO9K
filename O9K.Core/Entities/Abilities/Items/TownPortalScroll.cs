using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000179 RID: 377
	[AbilityId(AbilityId.item_tpscroll)]
	public class TownPortalScroll : RangedAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000795 RID: 1941 RVA: 0x000071AC File Offset: 0x000053AC
		public TownPortalScroll(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000796 RID: 1942 RVA: 0x000071CD File Offset: 0x000053CD
		public override float CastRange { get; } = 9999999f;

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000797 RID: 1943 RVA: 0x000071D5 File Offset: 0x000053D5
		public float ChannelTime { get; }

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000798 RID: 1944 RVA: 0x000071DD File Offset: 0x000053DD
		public bool IsActivatesOnChannelStart { get; }
	}
}
