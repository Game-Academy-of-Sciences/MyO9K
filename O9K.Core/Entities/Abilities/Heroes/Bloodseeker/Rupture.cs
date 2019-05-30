using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Bloodseeker
{
	// Token: 0x020003C2 RID: 962
	[AbilityId(AbilityId.bloodseeker_rupture)]
	public class Rupture : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06001012 RID: 4114 RVA: 0x0000E248 File Offset: 0x0000C448
		public Rupture(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001013 RID: 4115 RVA: 0x0000E25C File Offset: 0x0000C45C
		public string DebuffModifierName { get; } = "modifier_bloodseeker_rupture";
	}
}
