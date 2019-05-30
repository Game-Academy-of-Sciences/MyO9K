using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Doom
{
	// Token: 0x02000393 RID: 915
	[AbilityId(AbilityId.doom_bringer_infernal_blade)]
	public class InfernalBlade : OrbAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000F95 RID: 3989 RVA: 0x00009561 File Offset: 0x00007761
		public InfernalBlade(Ability baseAbility) : base(baseAbility)
		{
		}
	}
}
