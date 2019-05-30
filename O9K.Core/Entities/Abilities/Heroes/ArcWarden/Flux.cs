using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ArcWarden
{
	// Token: 0x02000260 RID: 608
	[AbilityId(AbilityId.arc_warden_flux)]
	public class Flux : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B0E RID: 2830 RVA: 0x00009FF3 File Offset: 0x000081F3
		public Flux(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000425 RID: 1061
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0000A007 File Offset: 0x00008207
		public string DebuffModifierName { get; } = string.Empty;
	}
}
