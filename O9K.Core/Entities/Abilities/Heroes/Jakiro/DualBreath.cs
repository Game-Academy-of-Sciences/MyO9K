using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Jakiro
{
	// Token: 0x02000222 RID: 546
	[AbilityId(AbilityId.jakiro_dual_breath)]
	public class DualBreath : ConeAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000A5B RID: 2651 RVA: 0x0002405C File Offset: 0x0002225C
		public DualBreath(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "start_radius");
			this.EndRadiusData = new SpecialData(baseAbility, "end_radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x00009559 File Offset: 0x00007759
		public string DebuffModifierName { get; } = "modifier_jakiro_dual_breath_slow";
	}
}
