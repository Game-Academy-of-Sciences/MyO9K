using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Gyrocopter
{
	// Token: 0x0200022B RID: 555
	[AbilityId(AbilityId.gyrocopter_call_down)]
	public class CallDown : CircleAbility
	{
		// Token: 0x06000A6E RID: 2670 RVA: 0x00009683 File Offset: 0x00007883
		public CallDown(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.ActivationDelayData = new SpecialData(baseAbility, "slow_duration_first");
			this.DamageData = new SpecialData(baseAbility, "damage_first");
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x06000A6F RID: 2671 RVA: 0x000096BF File Offset: 0x000078BF
		public override float CastRange
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_gyrocopter_5);
				if (abilityById != null && abilityById.Level > 0u)
				{
					return 9999999f;
				}
				return base.CastRange;
			}
		}
	}
}
