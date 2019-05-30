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

namespace O9K.Core.Entities.Abilities.Heroes.OutworldDevourer
{
	// Token: 0x0200030A RID: 778
	[AbilityId(AbilityId.obsidian_destroyer_arcane_orb)]
	public class ArcaneOrb : OrbAbility, IHarass, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x06000D73 RID: 3443 RVA: 0x0000BF32 File Offset: 0x0000A132
		public ArcaneOrb(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "mana_pool_damage_pct");
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000D74 RID: 3444 RVA: 0x0000BF5E File Offset: 0x0000A15E
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000D75 RID: 3445 RVA: 0x0000BF66 File Offset: 0x0000A166
		public bool MultipliedByCrit { get; }

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000D76 RID: 3446 RVA: 0x0000BF6E File Offset: 0x0000A16E
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000D77 RID: 3447 RVA: 0x00026C10 File Offset: 0x00024E10
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x00026C5C File Offset: 0x00024E5C
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x00026C8C File Offset: 0x00024E8C
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsBuilding || unit.IsAlly(base.Owner))
			{
				return 0f;
			}
			return base.Owner.Mana * this.DamageData.GetValue(this.Level) / 100f;
		}
	}
}
