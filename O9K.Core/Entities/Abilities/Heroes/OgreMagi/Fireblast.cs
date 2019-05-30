using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.OgreMagi
{
	// Token: 0x02000313 RID: 787
	[AbilityId(AbilityId.ogre_magi_fireblast)]
	[AbilityId(AbilityId.ogre_magi_unrefined_fireblast)]
	public class Fireblast : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000DA0 RID: 3488 RVA: 0x0000C0B5 File Offset: 0x0000A2B5
		public Fireblast(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "fireblast_damage");
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x06000DA1 RID: 3489 RVA: 0x0000C0D8 File Offset: 0x0000A2D8
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
