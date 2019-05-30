using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Zeus
{
	// Token: 0x020001B6 RID: 438
	[AbilityId(AbilityId.zuus_static_field)]
	public class StaticField : PassiveAbility, IHasRadius
	{
		// Token: 0x060008DF RID: 2271 RVA: 0x0000811D File Offset: 0x0000631D
		public StaticField(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_health_pct");
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00022888 File Offset: 0x00020A88
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float num = remainingHealth ?? unit.Health;
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)(num * this.DamageData.GetValue(this.Level) / 100f));
			return damage;
		}
	}
}
