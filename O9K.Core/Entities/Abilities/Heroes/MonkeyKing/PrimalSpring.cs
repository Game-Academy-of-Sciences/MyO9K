using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x02000328 RID: 808
	[AbilityId(AbilityId.monkey_king_primal_spring)]
	public class PrimalSpring : CircleAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000DE7 RID: 3559 RVA: 0x000270E4 File Offset: 0x000252E4
		public PrimalSpring(Ability baseAbility) : base(baseAbility)
		{
			this.ChannelTime = baseAbility.GetChannelTime(0u);
			this.RadiusData = new SpecialData(baseAbility, "impact_radius");
			this.DamageData = new SpecialData(baseAbility, "impact_damage");
			this.castRangeData = new SpecialData(baseAbility, "max_distance");
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06000DE8 RID: 3560 RVA: 0x0000C4C2 File Offset: 0x0000A6C2
		public override float Speed { get; } = 1300f;

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06000DE9 RID: 3561 RVA: 0x0000C4CA File Offset: 0x0000A6CA
		public override float ActivationDelay
		{
			get
			{
				return this.ChannelTime;
			}
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06000DEA RID: 3562 RVA: 0x0000C4D2 File Offset: 0x0000A6D2
		public float ChannelTime { get; }

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06000DEB RID: 3563 RVA: 0x0000C4DA File Offset: 0x0000A6DA
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06000DEC RID: 3564 RVA: 0x0000C4E2 File Offset: 0x0000A6E2
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x06000DED RID: 3565 RVA: 0x0000C4F5 File Offset: 0x0000A6F5
		public int GetCurrentDamage(Unit9 unit)
		{
			return (int)((float)this.GetDamage(unit) * (base.BaseAbility.ChannelTime / this.ChannelTime));
		}

		// Token: 0x04000739 RID: 1849
		private readonly SpecialData castRangeData;
	}
}
