using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
	// Token: 0x020003CD RID: 973
	[AbilityId(AbilityId.antimage_mana_break)]
	public class ManaBreak : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06001034 RID: 4148 RVA: 0x0000E459 File Offset: 0x0000C659
		public ManaBreak(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "mana_per_hit");
			this.burnMultiplierData = new SpecialData(baseAbility, "damage_per_burn");
		}

		// Token: 0x170006F2 RID: 1778
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0000E496 File Offset: 0x0000C696
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170006F3 RID: 1779
		// (get) Token: 0x06001036 RID: 4150 RVA: 0x0000E49E File Offset: 0x0000C69E
		public bool MultipliedByCrit { get; }

		// Token: 0x170006F4 RID: 1780
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0000E4A6 File Offset: 0x0000C6A6
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06001038 RID: 4152 RVA: 0x00028B14 File Offset: 0x00026D14
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (!unit.IsUnit || unit.IsMagicImmune || unit.IsAlly(base.Owner))
			{
				return new Damage();
			}
			float value = this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)(Math.Min(value, unit.Mana) * this.burnMultiplierData.GetValue(this.Level)));
			return damage;
		}

		// Token: 0x0400086F RID: 2159
		private readonly SpecialData burnMultiplierData;
	}
}
