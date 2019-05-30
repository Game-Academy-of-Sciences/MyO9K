using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Bristleback
{
	// Token: 0x020003AF RID: 943
	[AbilityId(AbilityId.bristleback_viscous_nasal_goo)]
	public class ViscousNasalGoo : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0000E040 File Offset: 0x0000C240
		public ViscousNasalGoo(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "goo_speed");
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06000FE9 RID: 4073 RVA: 0x0000E065 File Offset: 0x0000C265
		public string DebuffModifierName { get; } = string.Empty;
	}
}
