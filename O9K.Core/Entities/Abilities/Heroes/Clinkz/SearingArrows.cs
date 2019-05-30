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

namespace O9K.Core.Entities.Abilities.Heroes.Clinkz
{
	// Token: 0x02000253 RID: 595
	[AbilityId(AbilityId.clinkz_searing_arrows)]
	public class SearingArrows : OrbAbility, IHarass, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x06000ADB RID: 2779 RVA: 0x00009D1E File Offset: 0x00007F1E
		public SearingArrows(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_bonus");
		}

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x06000ADC RID: 2780 RVA: 0x00009D4A File Offset: 0x00007F4A
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x06000ADD RID: 2781 RVA: 0x00009D52 File Offset: 0x00007F52
		public bool MultipliedByCrit { get; }

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000ADE RID: 2782 RVA: 0x00009D5A File Offset: 0x00007F5A
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000ADF RID: 2783 RVA: 0x00024988 File Offset: 0x00022B88
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000249D4 File Offset: 0x00022BD4
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x00009D62 File Offset: 0x00007F62
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsAlly(base.Owner))
			{
				return 0f;
			}
			return this.DamageData.GetValue(this.Level);
		}
	}
}
