using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Riki
{
	// Token: 0x020002E4 RID: 740
	[AbilityId(AbilityId.riki_permanent_invisibility)]
	public class CloakAndDagger : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000CE1 RID: 3297 RVA: 0x0000B924 File Offset: 0x00009B24
		public CloakAndDagger(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
			this.DamageData = new SpecialData(baseAbility, "damage_multiplier");
		}

		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000CE2 RID: 3298 RVA: 0x0000B957 File Offset: 0x00009B57
		public override bool IntelligenceAmplify { get; }

		// Token: 0x1700052E RID: 1326
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0000B95F File Offset: 0x00009B5F
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x06000CE4 RID: 3300 RVA: 0x0000B967 File Offset: 0x00009B67
		public bool MultipliedByCrit { get; }

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0000B96F File Offset: 0x00009B6F
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000CE6 RID: 3302 RVA: 0x0000B977 File Offset: 0x00009B77
		public override int GetDamage(Unit9 unit)
		{
			if ((double)unit.GetAngle(base.Owner.Position, false) < 1.5)
			{
				return 0;
			}
			return base.GetDamage(unit);
		}

		// Token: 0x06000CE7 RID: 3303 RVA: 0x00025E30 File Offset: 0x00024030
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage;
			Damage result = rawDamage = base.GetRawDamage(unit, remainingHealth);
			DamageType damageType = this.DamageType;
			rawDamage[damageType] *= base.Owner.TotalAgility;
			return result;
		}

		// Token: 0x06000CE8 RID: 3304 RVA: 0x00025E68 File Offset: 0x00024068
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (!unit.IsBuilding && !unit.IsAlly(base.Owner) && !base.Owner.IsIllusion && unit.GetAngle(base.Owner, false) > 2f)
			{
				damage[this.DamageType] = this.DamageData.GetValue(this.Level) * base.Owner.TotalAgility;
			}
			return damage;
		}
	}
}
