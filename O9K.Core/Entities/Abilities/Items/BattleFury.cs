using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200010B RID: 267
	[AbilityId(AbilityId.item_bfury)]
	public class BattleFury : RangedAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x000218A0 File Offset: 0x0001FAA0
		public BattleFury(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "quelling_bonus");
			this.rangedDamageData = new SpecialData(baseAbility, "quelling_bonus_ranged");
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006B2 RID: 1714 RVA: 0x000068C8 File Offset: 0x00004AC8
		public override DamageType DamageType { get; } = 1;

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006B3 RID: 1715 RVA: 0x000068D0 File Offset: 0x00004AD0
		public override bool TargetsEnemy { get; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x000068D8 File Offset: 0x00004AD8
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x000068E0 File Offset: 0x00004AE0
		public bool MultipliedByCrit { get; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x000068E8 File Offset: 0x00004AE8
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x060006B7 RID: 1719 RVA: 0x000218F0 File Offset: 0x0001FAF0
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (unit.IsCreep && base.IsActive && !unit.IsAlly(base.Owner) && !base.Owner.IsIllusion)
			{
				damage[this.DamageType] = (base.Owner.IsRanged ? this.rangedDamageData.GetValue(this.Level) : this.DamageData.GetValue(this.Level));
			}
			return damage;
		}

		// Token: 0x04000304 RID: 772
		private readonly SpecialData rangedDamageData;
	}
}
