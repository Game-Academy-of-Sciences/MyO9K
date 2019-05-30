using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.NagaSiren
{
	// Token: 0x02000321 RID: 801
	[AbilityId(AbilityId.naga_siren_ensnare)]
	public class Ensnare : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000DD0 RID: 3536 RVA: 0x0000C34D File Offset: 0x0000A54D
		public Ensnare(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "net_speed");
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000DD1 RID: 3537 RVA: 0x0000C37A File Offset: 0x0000A57A
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x0000C382 File Offset: 0x0000A582
		public string ImmobilityModifierName { get; } = "modifier_naga_siren_ensnare";
	}
}
