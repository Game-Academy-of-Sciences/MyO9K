using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Venomancer
{
	// Token: 0x02000281 RID: 641
	[AbilityId(AbilityId.venomancer_poison_nova)]
	public class PoisonNova : AreaOfEffectAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B84 RID: 2948 RVA: 0x0000A5E5 File Offset: 0x000087E5
		public PoisonNova(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x0000A61B File Offset: 0x0000881B
		public string DebuffModifierName { get; } = "modifier_venomancer_poison_nova";
	}
}
