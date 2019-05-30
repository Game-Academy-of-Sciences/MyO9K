using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.TreantProtector
{
	// Token: 0x0200029E RID: 670
	[AbilityId(AbilityId.treant_leech_seed)]
	public class LeechSeed : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000BE0 RID: 3040 RVA: 0x0000AAF2 File Offset: 0x00008CF2
		public LeechSeed(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0000AB06 File Offset: 0x00008D06
		public string DebuffModifierName { get; } = "modifier_treant_leech_seed";
	}
}
