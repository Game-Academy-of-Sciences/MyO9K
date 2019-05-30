using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Broodmother
{
	// Token: 0x020003AB RID: 939
	[AbilityId(AbilityId.broodmother_spawn_spiderlings)]
	public class SpawnSpiderlings : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000FDD RID: 4061 RVA: 0x0000DFED File Offset: 0x0000C1ED
		public SpawnSpiderlings(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
		}
	}
}
