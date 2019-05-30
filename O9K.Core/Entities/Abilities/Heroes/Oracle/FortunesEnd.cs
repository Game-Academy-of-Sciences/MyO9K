using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
	// Token: 0x020001A9 RID: 425
	[AbilityId(AbilityId.oracle_fortunes_end)]
	public class FortunesEnd : RangedAbility, IDisable, IChanneled, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x060008A3 RID: 2211 RVA: 0x00022434 File Offset: 0x00020634
		public FortunesEnd(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ChannelTime = baseAbility.GetChannelTime(0u);
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060008A4 RID: 2212 RVA: 0x00007EB1 File Offset: 0x000060B1
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060008A5 RID: 2213 RVA: 0x00007EB9 File Offset: 0x000060B9
		public float ChannelTime { get; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060008A6 RID: 2214 RVA: 0x00007EC1 File Offset: 0x000060C1
		public string ImmobilityModifierName { get; } = "modifier_oracle_fortunes_end_purge";

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00007EC9 File Offset: 0x000060C9
		public bool IsActivatesOnChannelStart { get; } = 1;
	}
}
