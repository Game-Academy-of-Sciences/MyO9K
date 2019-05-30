using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Chen
{
	// Token: 0x02000256 RID: 598
	[AbilityId(AbilityId.chen_penitence)]
	public class Penitence : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000AEB RID: 2795 RVA: 0x00009DF4 File Offset: 0x00007FF4
		public Penitence(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00009E19 File Offset: 0x00008019
		public string DebuffModifierName { get; } = "modifier_chen_penitence";
	}
}
