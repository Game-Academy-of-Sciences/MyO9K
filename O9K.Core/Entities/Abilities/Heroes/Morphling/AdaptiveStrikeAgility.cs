using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Morphling
{
	// Token: 0x02000201 RID: 513
	[AbilityId(AbilityId.morphling_adaptive_strike_agi)]
	public class AdaptiveStrikeAgility : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x060009EF RID: 2543 RVA: 0x000238A0 File Offset: 0x00021AA0
		public AdaptiveStrikeAgility(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.DamageData = new SpecialData(baseAbility, "damage_base");
			this.minMultiplierData = new SpecialData(baseAbility, "damage_min");
			this.maxMultiplierData = new SpecialData(baseAbility, "damage_max");
		}

		// Token: 0x060009F0 RID: 2544 RVA: 0x000238F8 File Offset: 0x00021AF8
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			float totalAgility = base.Owner.TotalAgility;
			float totalStrength = base.Owner.TotalStrength;
			float num = ((double)totalAgility * 0.75 > (double)totalStrength) ? this.maxMultiplierData.GetValue(this.Level) : this.minMultiplierData.GetValue(this.Level);
			float num2 = totalAgility * num;
			Damage damage = rawDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] += (float)((int)num2);
			return rawDamage;
		}

		// Token: 0x04000505 RID: 1285
		private readonly SpecialData maxMultiplierData;

		// Token: 0x04000506 RID: 1286
		private readonly SpecialData minMultiplierData;
	}
}
