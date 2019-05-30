using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.CrystalMaiden
{
	// Token: 0x0200039D RID: 925
	[AbilityId(AbilityId.crystal_maiden_freezing_field)]
	public class FreezingField : AreaOfEffectAbility, IChanneled, IActiveAbility
	{
		// Token: 0x06000FB8 RID: 4024 RVA: 0x0000DDE0 File Offset: 0x0000BFE0
		public FreezingField(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x0000DE1F File Offset: 0x0000C01F
		public float ChannelTime { get; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06000FBA RID: 4026 RVA: 0x0000DE27 File Offset: 0x0000C027
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
