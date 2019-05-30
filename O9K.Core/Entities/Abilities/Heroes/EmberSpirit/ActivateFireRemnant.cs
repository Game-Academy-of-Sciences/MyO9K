using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.EmberSpirit
{
	// Token: 0x02000380 RID: 896
	[AbilityId(AbilityId.ember_spirit_activate_fire_remnant)]
	public class ActivateFireRemnant : RangedAbility, IBlink, IActiveAbility
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x000281F8 File Offset: 0x000263F8
		public ActivateFireRemnant(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.RadiusData = new SpecialData(baseAbility, "radius");
		}

		// Token: 0x17000678 RID: 1656
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0000D8C6 File Offset: 0x0000BAC6
		public BlinkType BlinkType { get; } = 2;

		// Token: 0x17000679 RID: 1657
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0000D8CE File Offset: 0x0000BACE
		public override float CastRange { get; } = 9999999f;

		// Token: 0x06000F5F RID: 3935 RVA: 0x0000D8D6 File Offset: 0x0000BAD6
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.CanBeCasted(checkChanneling) && base.Owner.HasModifier("modifier_ember_spirit_fire_remnant_timer");
		}
	}
}
