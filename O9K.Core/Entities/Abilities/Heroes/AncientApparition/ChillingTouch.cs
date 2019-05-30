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

namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
	// Token: 0x02000267 RID: 615
	[AbilityId(AbilityId.ancient_apparition_chilling_touch)]
	public class ChillingTouch : OrbAbility, IHarass, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x06000B2D RID: 2861 RVA: 0x00024CF8 File Offset: 0x00022EF8
		public ChillingTouch(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.attackRangeIncrease = new SpecialData(baseAbility, "attack_range_bonus");
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0000A184 File Offset: 0x00008384
		public override DamageType DamageType { get; } = 2;

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x0000A18C File Offset: 0x0000838C
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x06000B30 RID: 2864 RVA: 0x0000A194 File Offset: 0x00008394
		public bool MultipliedByCrit { get; }

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06000B31 RID: 2865 RVA: 0x0000A19C File Offset: 0x0000839C
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000B32 RID: 2866 RVA: 0x00024D48 File Offset: 0x00022F48
		public override bool CanHit(Unit9 target)
		{
			return (!target.IsMagicImmune || this.CanHitSpellImmuneEnemy) && base.Owner.Distance(target) <= base.Owner.GetAttackRange(null, target.HullRadius + this.attackRangeIncrease.GetValue(this.Level));
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x00024D9C File Offset: 0x00022F9C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x00024DE8 File Offset: 0x00022FE8
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x0000A1A4 File Offset: 0x000083A4
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsBuilding || unit.IsAlly(base.Owner))
			{
				return 0f;
			}
			return this.DamageData.GetValue(this.Level);
		}

		// Token: 0x040005B3 RID: 1459
		private readonly SpecialData attackRangeIncrease;
	}
}
