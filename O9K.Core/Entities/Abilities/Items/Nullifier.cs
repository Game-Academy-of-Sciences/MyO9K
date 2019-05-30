using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000197 RID: 407
	[AbilityId(AbilityId.item_nullifier)]
	public class Nullifier : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x0600083C RID: 2108 RVA: 0x00007A2A File Offset: 0x00005C2A
		public Nullifier(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00007A4D File Offset: 0x00005C4D
		public UnitState AppliesUnitState { get; } = 16L;
	}
}
