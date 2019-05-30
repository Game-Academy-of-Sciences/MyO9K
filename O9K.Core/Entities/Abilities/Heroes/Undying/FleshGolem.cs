using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
	// Token: 0x020001A3 RID: 419
	[AbilityId(AbilityId.undying_flesh_golem)]
	public class FleshGolem : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000884 RID: 2180 RVA: 0x00007D90 File Offset: 0x00005F90
		public FleshGolem(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x00007DB5 File Offset: 0x00005FB5
		public string DebuffModifierName { get; } = "modifier_undying_flesh_golem";
	}
}
