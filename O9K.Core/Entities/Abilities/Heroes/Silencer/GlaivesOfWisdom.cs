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

namespace O9K.Core.Entities.Abilities.Heroes.Silencer
{
	// Token: 0x020001D9 RID: 473
	[AbilityId(AbilityId.silencer_glaives_of_wisdom)]
	public class GlaivesOfWisdom : OrbAbility, IHarass, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x06000973 RID: 2419 RVA: 0x0000882A File Offset: 0x00006A2A
		public GlaivesOfWisdom(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "intellect_damage_pct");
		}

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06000974 RID: 2420 RVA: 0x00008856 File Offset: 0x00006A56
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x06000975 RID: 2421 RVA: 0x0000885E File Offset: 0x00006A5E
		public bool MultipliedByCrit { get; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x06000976 RID: 2422 RVA: 0x00008866 File Offset: 0x00006A66
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000977 RID: 2423 RVA: 0x00023250 File Offset: 0x00021450
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x0002329C File Offset: 0x0002149C
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x000232CC File Offset: 0x000214CC
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsBuilding || unit.IsAlly(base.Owner))
			{
				return 0f;
			}
			return base.Owner.TotalIntelligence * this.DamageData.GetValue(this.Level) / 100f;
		}
	}
}
