using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
	// Token: 0x02000372 RID: 882
	[AbilityId(AbilityId.huskar_life_break)]
	public class LifeBreak : RangedAbility, INuke, IActiveAbility
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0000D64B File Offset: 0x0000B84B
		public LifeBreak(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "charge_speed");
			this.DamageData = new SpecialData(baseAbility, "health_damage");
			this.scepterDamageData = new SpecialData(baseAbility, "health_damage_scepter");
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00027F4C File Offset: 0x0002614C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			float num = Math.Max(remainingHealth ?? unit.Health, 0f);
			float num2 = base.Owner.HasAghanimsScepter ? this.scepterDamageData.GetValue(this.Level) : this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = num * num2;
			return damage;
		}

		// Token: 0x040007DB RID: 2011
		private readonly SpecialData scepterDamageData;
	}
}
