using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.MonkeyKing
{
	// Token: 0x02000324 RID: 804
	[AbilityId(AbilityId.monkey_king_jingu_mastery)]
	public class JinguMastery : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0000C408 File Offset: 0x0000A608
		public JinguMastery(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "bonus_damage");
		}

		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06000DDA RID: 3546 RVA: 0x0000C442 File Offset: 0x0000A642
		public override DamageType DamageType { get; } = 1;

		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x0000C44A File Offset: 0x0000A64A
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06000DDC RID: 3548 RVA: 0x0000C452 File Offset: 0x0000A652
		public bool MultipliedByCrit { get; } = 1;

		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0000C45A File Offset: 0x0000A65A
		public string PassiveDamageModifierName { get; } = "modifier_monkey_king_quadruple_tap_bonuses";

		// Token: 0x06000DDE RID: 3550 RVA: 0x00026FB0 File Offset: 0x000251B0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (base.Owner.HasModifier(this.PassiveDamageModifierName) ? this.DamageData.GetValue(this.Level) : 0f);
			return damage;
		}
	}
}
