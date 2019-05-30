using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019A RID: 410
	[AbilityId(AbilityId.item_rod_of_atos)]
	public class RodOfAtos : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000851 RID: 2129 RVA: 0x00007B2C File Offset: 0x00005D2C
		public RodOfAtos(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x00007B53 File Offset: 0x00005D53
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00007B5B File Offset: 0x00005D5B
		public string ImmobilityModifierName { get; } = "modifier_rod_of_atos_debuff";

		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000854 RID: 2132 RVA: 0x00007B63 File Offset: 0x00005D63
		public override float Speed { get; } = 1500f;
	}
}
