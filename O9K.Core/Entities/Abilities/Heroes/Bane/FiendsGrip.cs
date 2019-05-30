using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
	// Token: 0x020001B2 RID: 434
	[AbilityId(AbilityId.bane_fiends_grip)]
	public class FiendsGrip : RangedAbility, IDisable, IChanneled, IActiveAbility
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x0000803F File Offset: 0x0000623F
		public FiendsGrip(Ability baseAbility) : base(baseAbility)
		{
			this.channelData = new SpecialData(baseAbility, "fiend_grip_duration");
		}

		// Token: 0x170002F5 RID: 757
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00008069 File Offset: 0x00006269
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170002F6 RID: 758
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00008071 File Offset: 0x00006271
		public float ChannelTime
		{
			get
			{
				return this.channelData.GetValue(this.Level);
			}
		}

		// Token: 0x170002F7 RID: 759
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00008084 File Offset: 0x00006284
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x0400045A RID: 1114
		private readonly SpecialData channelData;
	}
}
