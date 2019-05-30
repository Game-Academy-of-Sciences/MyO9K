using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Lich
{
	// Token: 0x02000211 RID: 529
	[AbilityId(AbilityId.lich_sinister_gaze)]
	public class SinisterGaze : RangedAbility, IDisable, IChanneled, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x00009228 File Offset: 0x00007428
		public SinisterGaze(Ability baseAbility) : base(baseAbility)
		{
			this.channelTimeData = new SpecialData(baseAbility, "duration");
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0000925D File Offset: 0x0000745D
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x00009265 File Offset: 0x00007465
		public float ChannelTime
		{
			get
			{
				return this.channelTimeData.GetValue(this.Level);
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x00009278 File Offset: 0x00007478
		public bool IsActivatesOnChannelStart { get; } = 1;

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A22 RID: 2594 RVA: 0x00009280 File Offset: 0x00007480
		public string ImmobilityModifierName { get; } = "modifier_lich_sinister_gaze";

		// Token: 0x04000520 RID: 1312
		private readonly SpecialData channelTimeData;
	}
}
