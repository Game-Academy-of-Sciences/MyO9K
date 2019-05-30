using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Undying
{
	// Token: 0x020001A2 RID: 418
	[AbilityId(AbilityId.undying_decay)]
	public class Decay : CircleAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000882 RID: 2178 RVA: 0x00007D52 File Offset: 0x00005F52
		public Decay(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "decay_damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06000883 RID: 2179 RVA: 0x00007D88 File Offset: 0x00005F88
		public string DebuffModifierName { get; } = string.Empty;
	}
}
