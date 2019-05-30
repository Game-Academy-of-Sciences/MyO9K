using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Lifestealer
{
	// Token: 0x0200034E RID: 846
	[AbilityId(AbilityId.life_stealer_feast)]
	public class Feast : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000E53 RID: 3667 RVA: 0x0000CA07 File Offset: 0x0000AC07
		public Feast(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "hp_leech_percent");
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x0000CA3A File Offset: 0x0000AC3A
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170005F1 RID: 1521
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x0000CA42 File Offset: 0x0000AC42
		public bool MultipliedByCrit { get; }

		// Token: 0x170005F2 RID: 1522
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0000CA4A File Offset: 0x0000AC4A
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x170005F3 RID: 1523
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x0000CA52 File Offset: 0x0000AC52
		public override DamageType DamageType { get; } = 1;

		// Token: 0x06000E58 RID: 3672 RVA: 0x00027464 File Offset: 0x00025664
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!unit.IsBuilding && !unit.IsAlly(base.Owner) && !base.Owner.IsIllusion)
			{
				damage[this.DamageType] = unit.Health * this.DamageData.GetValue(this.Level) / 100f;
			}
			return damage;
		}
	}
}
