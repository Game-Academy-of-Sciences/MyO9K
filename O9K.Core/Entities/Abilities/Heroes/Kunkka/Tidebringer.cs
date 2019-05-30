using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Kunkka
{
	// Token: 0x02000219 RID: 537
	[AbilityId(AbilityId.kunkka_tidebringer)]
	public class Tidebringer : AutoCastAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000A3C RID: 2620 RVA: 0x00023E24 File Offset: 0x00022024
		public Tidebringer(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "cleave_starting_width");
			this.endRadiusData = new SpecialData(baseAbility, "cleave_ending_width");
			this.RangeData = new SpecialData(baseAbility, "cleave_distance");
			this.DamageData = new SpecialData(baseAbility, "damage_bonus");
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000A3D RID: 2621 RVA: 0x0000720E File Offset: 0x0000540E
		public override float CastRange
		{
			get
			{
				return base.CastRange + 100f;
			}
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000A3E RID: 2622 RVA: 0x00006E98 File Offset: 0x00005098
		public override float Range
		{
			get
			{
				return this.RangeData.GetValue(this.Level);
			}
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000A3F RID: 2623 RVA: 0x0000940A File Offset: 0x0000760A
		public float EndRadius
		{
			get
			{
				return this.endRadiusData.GetValue(this.Level);
			}
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000A40 RID: 2624 RVA: 0x0000941D File Offset: 0x0000761D
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000A41 RID: 2625 RVA: 0x00009425 File Offset: 0x00007625
		public bool MultipliedByCrit { get; }

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000A42 RID: 2626 RVA: 0x0000942D File Offset: 0x0000762D
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000A43 RID: 2627 RVA: 0x00023E90 File Offset: 0x00022090
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (!this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x00009435 File Offset: 0x00007635
		public bool IsTidebringerAnimation(string name)
		{
			return name == "tidebringer" || name == "attack1_gunsword_anim";
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x00023EDC File Offset: 0x000220DC
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (this.Enabled)
			{
				damage[this.DamageType] = this.GetOrbDamage(unit);
			}
			return damage;
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x00009451 File Offset: 0x00007651
		private float GetOrbDamage(Unit9 unit)
		{
			if (unit.IsBuilding || !this.CanBeCasted(true))
			{
				return 0f;
			}
			return this.DamageData.GetValue(this.Level);
		}

		// Token: 0x04000531 RID: 1329
		private readonly SpecialData endRadiusData;
	}
}
