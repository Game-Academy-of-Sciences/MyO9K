using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000175 RID: 373
	[AbilityId(AbilityId.item_meteor_hammer)]
	public class MeteorHammer : CircleAbility, IDisable, IChanneled, IActiveAbility
	{
		// Token: 0x06000782 RID: 1922 RVA: 0x00021C90 File Offset: 0x0001FE90
		public MeteorHammer(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
			this.RadiusData = new SpecialData(baseAbility, "impact_radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "land_time");
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000783 RID: 1923 RVA: 0x000070CA File Offset: 0x000052CA
		public override float ActivationDelay
		{
			get
			{
				return base.ActivationDelay + this.ChannelTime;
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x000070D9 File Offset: 0x000052D9
		public float ChannelTime { get; }

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000785 RID: 1925 RVA: 0x000070E1 File Offset: 0x000052E1
		public override DamageType DamageType
		{
			get
			{
				return DamageType.Magical;
			}
		}

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x000070E4 File Offset: 0x000052E4
		public bool IsActivatesOnChannelStart { get; }

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000787 RID: 1927 RVA: 0x000070EC File Offset: 0x000052EC
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
