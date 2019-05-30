using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ShadowDemon
{
	// Token: 0x020001E4 RID: 484
	[AbilityId(AbilityId.shadow_demon_demonic_purge)]
	public class DemonicPurge : RangedAbility, IDebuff, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000992 RID: 2450 RVA: 0x000089DC File Offset: 0x00006BDC
		public DemonicPurge(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000993 RID: 2451 RVA: 0x000089FB File Offset: 0x00006BFB
		public string DebuffModifierName { get; } = "modifier_shadow_demon_purge_slow";

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00008A03 File Offset: 0x00006C03
		public string ImmobilityModifierName { get; } = "modifier_shadow_demon_purge_slow";
	}
}
