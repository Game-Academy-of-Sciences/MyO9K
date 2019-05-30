using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
	// Token: 0x02000395 RID: 917
	[AbilityId(AbilityId.doom_bringer_doom)]
	public class Doom : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000F98 RID: 3992 RVA: 0x0000DBFD File Offset: 0x0000BDFD
		public Doom(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06000F99 RID: 3993 RVA: 0x0000DC0F File Offset: 0x0000BE0F
		public UnitState AppliesUnitState { get; } = 24L;
	}
}
