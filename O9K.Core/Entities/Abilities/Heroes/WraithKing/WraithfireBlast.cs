using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.WraithKing
{
	// Token: 0x0200026B RID: 619
	[AbilityId(AbilityId.skeleton_king_hellfire_blast)]
	public class WraithfireBlast : RangedAbility, IDisable, INuke, IActiveAbility
	{
		// Token: 0x06000B40 RID: 2880 RVA: 0x0000A225 File Offset: 0x00008425
		public WraithfireBlast(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "blast_speed");
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000B41 RID: 2881 RVA: 0x0000A248 File Offset: 0x00008448
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
