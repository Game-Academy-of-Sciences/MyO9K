using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Timbersaw
{
	// Token: 0x020002AA RID: 682
	[AbilityId(AbilityId.shredder_timber_chain)]
	public class TimberChain : LineAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x0002555C File Offset: 0x0002375C
		public TimberChain(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.chainRadiusData = new SpecialData(baseAbility, "chain_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.castRangeData = new SpecialData(baseAbility, "range");
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000C11 RID: 3089 RVA: 0x0000AD5E File Offset: 0x00008F5E
		public float ChainRadius
		{
			get
			{
				return this.chainRadiusData.GetValue(this.Level);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x0000AD71 File Offset: 0x00008F71
		public override bool HasAreaOfEffect { get; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x0000AD79 File Offset: 0x00008F79
		public BlinkType BlinkType { get; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000C14 RID: 3092 RVA: 0x0000AD81 File Offset: 0x00008F81
		protected override float BaseCastRange
		{
			get
			{
				return this.castRangeData.GetValue(this.Level);
			}
		}

		// Token: 0x04000631 RID: 1585
		private readonly SpecialData castRangeData;

		// Token: 0x04000632 RID: 1586
		private readonly SpecialData chainRadiusData;
	}
}
