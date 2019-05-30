using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Medusa
{
	// Token: 0x02000332 RID: 818
	[AbilityId(AbilityId.medusa_stone_gaze)]
	public class StoneGaze : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000E0D RID: 3597 RVA: 0x0000C6F5 File Offset: 0x0000A8F5
		public StoneGaze(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06000E0E RID: 3598 RVA: 0x0000C71A File Offset: 0x0000A91A
		public string DebuffModifierName { get; } = "modifier_medusa_stone_gaze_slow";
	}
}
