using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.QueenOfPain
{
	// Token: 0x020001E9 RID: 489
	[AbilityId(AbilityId.queenofpain_shadow_strike)]
	public class ShadowStrike : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x060009A1 RID: 2465 RVA: 0x00008AF9 File Offset: 0x00006CF9
		public ShadowStrike(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "strike_damage");
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060009A2 RID: 2466 RVA: 0x00008B2F File Offset: 0x00006D2F
		public string DebuffModifierName { get; } = "modifier_queenofpain_shadow_strike";
	}
}
