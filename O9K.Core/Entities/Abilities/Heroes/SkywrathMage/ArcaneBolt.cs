using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.SkywrathMage
{
	// Token: 0x020001D8 RID: 472
	[AbilityId(AbilityId.skywrath_mage_arcane_bolt)]
	public class ArcaneBolt : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000971 RID: 2417 RVA: 0x000087EE File Offset: 0x000069EE
		public ArcaneBolt(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "bolt_speed");
			this.DamageData = new SpecialData(baseAbility, "bolt_damage");
			this.damageMultiplierData = new SpecialData(baseAbility, "int_multiplier");
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x00023200 File Offset: 0x00021400
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			float value = this.damageMultiplierData.GetValue(this.Level);
			Damage damage = rawDamage;
			DamageType damageType = this.DamageType;
			damage[damageType] += (float)((int)(base.Owner.TotalIntelligence * value));
			return rawDamage;
		}

		// Token: 0x040004BC RID: 1212
		private readonly SpecialData damageMultiplierData;
	}
}
