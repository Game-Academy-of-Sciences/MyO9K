using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Enchantress
{
	// Token: 0x0200037E RID: 894
	[AbilityId(AbilityId.enchantress_impetus)]
	public class Impetus : OrbAbility, IHarass, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x06000F4E RID: 3918 RVA: 0x00028050 File Offset: 0x00026250
		public Impetus(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "distance_damage_pct");
			this.damageCapRange = new SpecialData(baseAbility, "distance_cap");
			this.aghanimsBonusRangeData = new SpecialData(baseAbility, "bonus_attack_range_scepter");
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000F4F RID: 3919 RVA: 0x0000D85B File Offset: 0x0000BA5B
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000F50 RID: 3920 RVA: 0x0000D863 File Offset: 0x0000BA63
		public bool MultipliedByCrit { get; }

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0000D86B File Offset: 0x0000BA6B
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000F52 RID: 3922 RVA: 0x000280AC File Offset: 0x000262AC
		public override bool CanHit(Unit9 target)
		{
			if (target.IsMagicImmune && !this.CanHitSpellImmuneEnemy)
			{
				return false;
			}
			float num = 0f;
			if (base.Owner.HasAghanimsScepter)
			{
				num = this.aghanimsBonusRangeData.GetValue(this.Level);
			}
			return base.Owner.Distance(target) <= base.Owner.GetAttackRange(null, target.HullRadius + num);
		}

		// Token: 0x06000F53 RID: 3923 RVA: 0x00028118 File Offset: 0x00026318
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000F54 RID: 3924 RVA: 0x00028164 File Offset: 0x00026364
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000F55 RID: 3925 RVA: 0x00028194 File Offset: 0x00026394
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsBuilding || unit.IsAlly(base.Owner))
			{
				return 0f;
			}
			return this.DamageData.GetValue(this.Level) / 100f * Math.Min(base.Owner.Distance(unit), this.damageCapRange.GetValue(this.Level));
		}

		// Token: 0x040007EA RID: 2026
		private readonly SpecialData aghanimsBonusRangeData;

		// Token: 0x040007EB RID: 2027
		private readonly SpecialData damageCapRange;
	}
}
