using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019C RID: 412
	[AbilityId(AbilityId.item_sheepstick)]
	public class ScytheOfVyse : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000859 RID: 2137 RVA: 0x00007B9E File Offset: 0x00005D9E
		public ScytheOfVyse(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x0600085A RID: 2138 RVA: 0x00007BBB File Offset: 0x00005DBB
		public UnitState AppliesUnitState { get; } = 74L;

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x0600085B RID: 2139 RVA: 0x00007BC3 File Offset: 0x00005DC3
		public string ImmobilityModifierName { get; } = "modifier_sheepstick_debuff";
	}
}
