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

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000188 RID: 392
	[AbilityId(AbilityId.item_ethereal_blade)]
	public class EtherealBlade : RangedAbility, IDebuff, IDisable, INuke, IShield, IHasDamageAmplify, IActiveAbility
	{
		// Token: 0x060007DA RID: 2010 RVA: 0x00021E7C File Offset: 0x0002007C
		public EtherealBlade(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "blast_damage_base");
			this.SpeedData = new SpecialData(baseAbility, "projectile_speed");
			this.amplifierData = new SpecialData(baseAbility, "ethereal_damage_bonus");
			this.damageMultiplierData = new SpecialData(baseAbility, "blast_agility_multiplier");
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x000075A3 File Offset: 0x000057A3
		public DamageType AmplifierDamageType { get; } = 2;

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060007DC RID: 2012 RVA: 0x000075AB File Offset: 0x000057AB
		public string AmplifierModifierName { get; } = "modifier_item_ethereal_blade_ethereal";

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060007DD RID: 2013 RVA: 0x000075B3 File Offset: 0x000057B3
		public AmplifiesDamage AmplifiesDamage { get; } = 1;

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x000075BB File Offset: 0x000057BB
		public UnitState AppliesUnitState { get; } = 2L;

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x000075C3 File Offset: 0x000057C3
		public override DamageType DamageType { get; } = 2;

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x000075CB File Offset: 0x000057CB
		public bool IsAmplifierAddedToStats { get; } = 1;

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060007E1 RID: 2017 RVA: 0x000075D3 File Offset: 0x000057D3
		public bool IsAmplifierPermanent { get; }

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060007E2 RID: 2018 RVA: 0x000075DB File Offset: 0x000057DB
		public string ShieldModifierName { get; } = "modifier_item_ethereal_blade_ethereal";

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060007E3 RID: 2019 RVA: 0x000075E3 File Offset: 0x000057E3
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x000075EB File Offset: 0x000057EB
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x000075F3 File Offset: 0x000057F3
		public string DebuffModifierName { get; } = "modifier_item_ethereal_blade_ethereal";

		// Token: 0x060007E6 RID: 2022 RVA: 0x000075FB File Offset: 0x000057FB
		public float AmplifierValue(Unit9 source, Unit9 target)
		{
			return this.amplifierData.GetValue(this.Level) / -100f;
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00021F28 File Offset: 0x00020128
		public override int GetDamage(Unit9 unit)
		{
			float damageAmplification = unit.GetDamageAmplification(base.Owner, this.DamageType, true);
			float damageBlock = unit.GetDamageBlock(this.DamageType);
			Damage rawDamage = this.GetRawDamage(unit, null);
			float num = 1f;
			if (!unit.HasModifier(this.AmplifierModifierName))
			{
				num += this.AmplifierValue(base.Owner, unit);
			}
			return (int)((rawDamage[this.DamageType] - damageBlock) * damageAmplification * num);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00021F9C File Offset: 0x0002019C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage rawDamage = base.GetRawDamage(unit, remainingHealth);
			float value = this.damageMultiplierData.GetValue(this.Level);
			switch (base.Owner.PrimaryAttribute)
			{
			case Ensage.Attribute.Strength:
			{
				Damage damage = rawDamage;
				DamageType damageType = this.DamageType;
				damage[damageType] += value * base.Owner.TotalStrength;
				break;
			}
			case Ensage.Attribute.Agility:
			{
				Damage damage = rawDamage;
				DamageType damageType = this.DamageType;
				damage[damageType] += value * base.Owner.TotalAgility;
				break;
			}
			case Ensage.Attribute.Intelligence:
			{
				Damage damage = rawDamage;
				DamageType damageType = this.DamageType;
				damage[damageType] += value * base.Owner.TotalIntelligence;
				break;
			}
			}
			return rawDamage;
		}

		// Token: 0x040003A3 RID: 931
		private readonly SpecialData amplifierData;

		// Token: 0x040003A4 RID: 932
		private readonly SpecialData damageMultiplierData;
	}
}
