using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Razor
{
	// Token: 0x020002EC RID: 748
	[AbilityId(AbilityId.razor_plasma_field)]
	public class PlasmaField : AreaOfEffectAbility, IDebuff, INuke, IActiveAbility
	{
		// Token: 0x06000D03 RID: 3331 RVA: 0x000261F0 File Offset: 0x000243F0
		public PlasmaField(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "radius");
			this.SpeedData = new SpecialData(baseAbility, "speed");
			this.minDamageData = new SpecialData(baseAbility, "damage_min");
			this.maxDamageData = new SpecialData(baseAbility, "damage_max");
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0000BA62 File Offset: 0x00009C62
		public string DebuffModifierName { get; } = "modifier_razor_plasma_field_slow";

		// Token: 0x06000D05 RID: 3333 RVA: 0x00026254 File Offset: 0x00024454
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float num = Math.Max(unit.Distance(base.Owner) - 100f, 0f);
			float value = this.maxDamageData.GetValue(this.Level);
			float value2 = this.minDamageData.GetValue(this.Level);
			float value3 = Math.Max(Math.Min(num / this.Radius * value, value), value2);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = value3;
			return damage;
		}

		// Token: 0x040006BB RID: 1723
		private readonly SpecialData maxDamageData;

		// Token: 0x040006BC RID: 1724
		private readonly SpecialData minDamageData;
	}
}
