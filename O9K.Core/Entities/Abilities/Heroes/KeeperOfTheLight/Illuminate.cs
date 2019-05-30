using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
	// Token: 0x0200021D RID: 541
	[AbilityId(AbilityId.keeper_of_the_light_illuminate)]
	public class Illuminate : LineAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000A4E RID: 2638 RVA: 0x00024004 File Offset: 0x00022204
		public Illuminate(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.channelTimeData = new SpecialData(baseAbility, new Func<uint, float>(baseAbility.GetChannelTime));
		}

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000A4F RID: 2639 RVA: 0x00009483 File Offset: 0x00007683
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000A50 RID: 2640 RVA: 0x00009496 File Offset: 0x00007696
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x04000536 RID: 1334
		private readonly SpecialData channelTimeData;
	}
}
