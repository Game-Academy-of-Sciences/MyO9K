using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
	// Token: 0x020001E5 RID: 485
	[AbilityId(AbilityId.shadow_demon_disruption)]
	public class Disruption : RangedAbility, IDisable, IShield, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000995 RID: 2453 RVA: 0x00008A0B File Offset: 0x00006C0B
		public Disruption(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00008A44 File Offset: 0x00006C44
		public UnitState AppliesUnitState { get; } = 288L;

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000997 RID: 2455 RVA: 0x00008A4C File Offset: 0x00006C4C
		public string ImmobilityModifierName { get; } = "modifier_shadow_demon_disruption";

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000998 RID: 2456 RVA: 0x00008A54 File Offset: 0x00006C54
		public string ShieldModifierName { get; } = "modifier_shadow_demon_disruption";

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000999 RID: 2457 RVA: 0x00008A5C File Offset: 0x00006C5C
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x0600099A RID: 2458 RVA: 0x00008A64 File Offset: 0x00006C64
		public bool ShieldsOwner { get; } = 1;
	}
}
