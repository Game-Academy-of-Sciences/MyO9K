using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.KeeperOfTheLight
{
	// Token: 0x0200021F RID: 543
	[AbilityId(AbilityId.keeper_of_the_light_will_o_wisp)]
	public class WilloWisp : CircleAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A53 RID: 2643 RVA: 0x000094BA File Offset: 0x000076BA
		public WilloWisp(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000A54 RID: 2644 RVA: 0x000094DD File Offset: 0x000076DD
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
