using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Spectre
{
	// Token: 0x020002C4 RID: 708
	[AbilityId(AbilityId.spectre_haunt)]
	public class Haunt : AreaOfEffectAbility
	{
		// Token: 0x06000C80 RID: 3200 RVA: 0x0000B421 File Offset: 0x00009621
		public Haunt(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0000B435 File Offset: 0x00009635
		public override float Radius { get; } = 9999999f;
	}
}
