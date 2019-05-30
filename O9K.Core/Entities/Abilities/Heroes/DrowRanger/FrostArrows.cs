using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.DrowRanger
{
	// Token: 0x0200038D RID: 909
	[AbilityId(AbilityId.drow_ranger_frost_arrows)]
	public class FrostArrows : OrbAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000F85 RID: 3973 RVA: 0x0000DAC9 File Offset: 0x0000BCC9
		public FrostArrows(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06000F86 RID: 3974 RVA: 0x0000DADD File Offset: 0x0000BCDD
		public override string OrbModifier { get; } = "modifier_drow_ranger_frost_arrows_slow";
	}
}
