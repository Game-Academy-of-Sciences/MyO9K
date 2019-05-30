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
	// Token: 0x02000185 RID: 389
	[AbilityId(AbilityId.item_diffusal_blade)]
	public class DiffusalBlade : RangedAbility, IDebuff, IHasPassiveDamageIncrease, IActiveAbility
	{
		// Token: 0x060007C9 RID: 1993 RVA: 0x00021DA8 File Offset: 0x0001FFA8
		public DiffusalBlade(Ability baseAbility) : base(baseAbility)
		{
			this.DamageData = new SpecialData(baseAbility, "feedback_mana_burn");
			this.burnMultiplierData = new SpecialData(baseAbility, "damage_per_burn");
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060007CA RID: 1994 RVA: 0x000074D9 File Offset: 0x000056D9
		public override DamageType DamageType { get; } = 1;

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060007CB RID: 1995 RVA: 0x000074E1 File Offset: 0x000056E1
		public string DebuffModifierName { get; } = "modifier_item_diffusal_blade_slow";

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x000074E9 File Offset: 0x000056E9
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060007CD RID: 1997 RVA: 0x000074F1 File Offset: 0x000056F1
		public bool MultipliedByCrit { get; }

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x000074F9 File Offset: 0x000056F9
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x060007CF RID: 1999 RVA: 0x00021E04 File Offset: 0x00020004
		public override Damage GetRawDamage(Unit9 unit, float? remainingHealth = null)
		{
			if (!base.IsActive || unit.IsMagicImmune || unit.IsAlly(base.Owner))
			{
				return new Damage();
			}
			float value = this.DamageData.GetValue(this.Level);
			Damage damage = new Damage();
			DamageType damageType = this.DamageType;
			damage[damageType] = (float)((int)(Math.Min(value, unit.Mana) * this.burnMultiplierData.GetValue(this.Level)));
			return damage;
		}

		// Token: 0x04000396 RID: 918
		private readonly SpecialData burnMultiplierData;
	}
}
