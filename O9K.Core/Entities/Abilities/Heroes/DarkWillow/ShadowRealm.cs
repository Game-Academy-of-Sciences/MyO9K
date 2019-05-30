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
using O9K.Core.Helpers.Range;

namespace O9K.Core.Entities.Abilities.Heroes.DarkWillow
{
	// Token: 0x0200039A RID: 922
	[AbilityId(AbilityId.dark_willow_shadow_realm)]
	public class ShadowRealm : ActiveAbility, INuke, IShield, IHasRangeIncrease, IActiveAbility
	{
		// Token: 0x06000FA8 RID: 4008 RVA: 0x0002850C File Offset: 0x0002670C
		public ShadowRealm(Ability baseAbility) : base(baseAbility)
		{
			this.attackRange = new SpecialData(baseAbility, "attack_range_bonus");
			this.DamageData = new SpecialData(baseAbility, "damage");
			this.maxDamageDurationData = new SpecialData(baseAbility, "max_damage_duration");
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06000FA9 RID: 4009 RVA: 0x0000DD24 File Offset: 0x0000BF24
		public UnitState AppliesUnitState { get; } = 17179869184L;

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06000FAA RID: 4010 RVA: 0x0000DD2C File Offset: 0x0000BF2C
		public string ShieldModifierName { get; } = "modifier_dark_willow_shadow_realm_buff";

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06000FAB RID: 4011 RVA: 0x0000DD34 File Offset: 0x0000BF34
		public bool ShieldsAlly { get; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06000FAC RID: 4012 RVA: 0x0000DD3C File Offset: 0x0000BF3C
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06000FAD RID: 4013 RVA: 0x0000DD44 File Offset: 0x0000BF44
		public bool IsRangeIncreasePermanent { get; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06000FAE RID: 4014 RVA: 0x0000DD4C File Offset: 0x0000BF4C
		public float RangeIncrease
		{
			get
			{
				return this.attackRange.GetValue(this.Level);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06000FAF RID: 4015 RVA: 0x0000DD5F File Offset: 0x0000BF5F
		public RangeIncreaseType RangeIncreaseType { get; } = 2;

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06000FB0 RID: 4016 RVA: 0x0000DD67 File Offset: 0x0000BF67
		public string RangeModifierName { get; } = "modifier_dark_willow_shadow_realm_buff";

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06000FB1 RID: 4017 RVA: 0x00028588 File Offset: 0x00026788
		public bool DamageMaxed
		{
			get
			{
				Modifier modifier = base.Owner.GetModifier(this.ShieldModifierName);
				return ((modifier != null) ? new float?(modifier.ElapsedTime) : null) >= this.maxDamageDurationData.GetValue(this.Level);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06000FB2 RID: 4018 RVA: 0x0000DD6F File Offset: 0x0000BF6F
		public float RealmTime
		{
			get
			{
				Modifier modifier = base.Owner.GetModifier(this.ShieldModifierName);
				if (modifier == null)
				{
					return 0f;
				}
				return modifier.ElapsedTime;
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06000FB3 RID: 4019 RVA: 0x0000DD91 File Offset: 0x0000BF91
		public bool Casted
		{
			get
			{
				return base.Owner.HasModifier(this.ShieldModifierName);
			}
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000285E8 File Offset: 0x000267E8
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			Modifier modifier = base.Owner.GetModifier(this.ShieldModifierName);
			if (modifier == null)
			{
				return damage;
			}
			float num = Math.Min(modifier.ElapsedTime / this.maxDamageDurationData.GetValue(this.Level), 1f);
			damage[this.DamageType] = this.DamageData.GetValue(this.Level) * num;
			return damage + base.Owner.GetRawAttackDamage(unit, DamageValue.Minimum, 1f, 0f);
		}

		// Token: 0x0400081E RID: 2078
		private readonly SpecialData attackRange;

		// Token: 0x0400081F RID: 2079
		private readonly SpecialData maxDamageDurationData;
	}
}
