using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Enigma
{
	// Token: 0x02000232 RID: 562
	[AbilityId(AbilityId.enigma_malefice)]
	public class Malefice : RangedAbility, IDisable, IActiveAbility
	{
		// Token: 0x06000A7B RID: 2683 RVA: 0x000097BF File Offset: 0x000079BF
		public Malefice(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x06000A7C RID: 2684 RVA: 0x000097D1 File Offset: 0x000079D1
		public UnitState AppliesUnitState { get; } = 32L;
	}
}
