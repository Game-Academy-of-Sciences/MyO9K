using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Viper
{
	// Token: 0x0200027E RID: 638
	[AbilityId(AbilityId.viper_viper_strike)]
	public class ViperStrike : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B7F RID: 2943 RVA: 0x0000A59C File Offset: 0x0000879C
		public ViperStrike(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06000B80 RID: 2944 RVA: 0x0000A5C1 File Offset: 0x000087C1
		public string DebuffModifierName { get; } = "modifier_viper_viper_strike_slow";
	}
}
