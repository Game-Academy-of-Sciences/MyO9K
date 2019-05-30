using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.ElderTitan
{
	// Token: 0x02000388 RID: 904
	[AbilityId(AbilityId.elder_titan_earth_splitter)]
	public class EarthSplitter : LineAbility
	{
		// Token: 0x06000F7A RID: 3962 RVA: 0x00028358 File Offset: 0x00026558
		public EarthSplitter(Ability baseAbility) : base(baseAbility)
		{
			this.ActivationDelayData = new SpecialData(baseAbility, "crack_time");
			this.RadiusData = new SpecialData(baseAbility, "crack_width");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.DamageData = new SpecialData(baseAbility, "damage_pct");
		}

		// Token: 0x06000F7B RID: 3963 RVA: 0x000283B0 File Offset: 0x000265B0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float value = unit.MaximumHealth * (this.DamageData.GetValue(this.Level) / 100f) * 0.5f;
			Damage damage = new Damage();
			damage[DamageType.Magical] = value;
			damage[DamageType.Physical] = value;
			return damage;
		}
	}
}
