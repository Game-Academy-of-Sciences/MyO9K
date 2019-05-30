using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.FacelessVoid
{
	// Token: 0x0200037B RID: 891
	[AbilityId(AbilityId.faceless_void_time_dilation)]
	public class TimeDilation : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000F48 RID: 3912 RVA: 0x0000D7E5 File Offset: 0x0000B9E5
		public TimeDilation(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x1700066E RID: 1646
		// (get) Token: 0x06000F49 RID: 3913 RVA: 0x0000D80A File Offset: 0x0000BA0A
		public string DebuffModifierName { get; } = "modifier_faceless_void_time_dilation_slow";
	}
}
