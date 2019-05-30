using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Units.HarpyStormcrafter
{
	// Token: 0x020000F2 RID: 242
	[AbilityId(AbilityId.harpy_storm_chain_lightning)]
	public class ChainLightning : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x0000662C File Offset: 0x0000482C
		public ChainLightning(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "initial_damage");
		}
	}
}
