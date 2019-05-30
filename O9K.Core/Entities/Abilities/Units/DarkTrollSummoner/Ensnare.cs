using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.DarkTrollSummoner
{
	// Token: 0x020000F4 RID: 244
	[AbilityId(AbilityId.dark_troll_warlord_ensnare)]
	public class Ensnare : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000682 RID: 1666 RVA: 0x00006646 File Offset: 0x00004846
		public Ensnare(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "net_speed");
		}

		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x06000683 RID: 1667 RVA: 0x00006673 File Offset: 0x00004873
		public UnitState AppliesUnitState { get; } = 1L;

		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x0000667B File Offset: 0x0000487B
		public string ImmobilityModifierName { get; } = "modifier_dark_troll_warlord_ensnare";
	}
}
