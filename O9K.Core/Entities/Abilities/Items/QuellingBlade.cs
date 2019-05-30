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
	// Token: 0x02000177 RID: 375
	[AbilityId(AbilityId.item_quelling_blade)]
	public class QuellingBlade : RangedAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x0600078D RID: 1933 RVA: 0x00021CDC File Offset: 0x0001FEDC
		public QuellingBlade(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "damage_bonus");
			this.rangedDamageData = new SpecialData(baseAbility, "damage_bonus_ranged");
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600078E RID: 1934 RVA: 0x00007184 File Offset: 0x00005384
		public override DamageType DamageType { get; } = 1;

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x0600078F RID: 1935 RVA: 0x0000718C File Offset: 0x0000538C
		public override bool TargetsEnemy { get; }

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000790 RID: 1936 RVA: 0x00007194 File Offset: 0x00005394
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000791 RID: 1937 RVA: 0x0000719C File Offset: 0x0000539C
		public bool MultipliedByCrit { get; }

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000792 RID: 1938 RVA: 0x000071A4 File Offset: 0x000053A4
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000793 RID: 1939 RVA: 0x00021D2C File Offset: 0x0001FF2C
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			Damage damage = new Damage();
			if (unit.IsCreep && base.IsActive && !unit.IsAlly(base.Owner) && !base.Owner.IsIllusion)
			{
				damage[this.DamageType] = (base.Owner.IsRanged ? this.rangedDamageData.GetValue(this.Level) : this.DamageData.GetValue(this.Level));
			}
			return damage;
		}

		// Token: 0x04000369 RID: 873
		private readonly SpecialData rangedDamageData;
	}
}
