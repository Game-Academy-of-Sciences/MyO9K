using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Heroes.Meepo
{
	// Token: 0x0200032D RID: 813
	[AbilityId(AbilityId.meepo_ransack)]
	public class Ransack : PassiveAbility, IHasPassiveDamageIncrease
	{
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0000C5B3 File Offset: 0x0000A7B3
		public Ransack(Ability baseAbility) : base(baseAbility)
		{
			this.heroDamageData = new SpecialData(baseAbility, "health_steal_heroes");
			this.creepDamageData = new SpecialData(baseAbility, "health_steal_creeps");
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		public bool IsPassiveDamagePermanent { get; } = 1;

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		public bool MultipliedByCrit { get; }

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0000C600 File Offset: 0x0000A800
		public string PassiveDamageModifierName { get; } = string.Empty;

		// Token: 0x06000DFB RID: 3579 RVA: 0x0002714C File Offset: 0x0002534C
		Damage IHasPassiveDamageIncrease.GetRawDamage(Unit9 unit, float? remainingHealth)
		{
			Damage damage = new Damage();
			if (!unit.IsBuilding && !unit.IsAlly(base.Owner) && !base.Owner.IsIllusion)
			{
				damage[this.DamageType] = (unit.IsHero ? this.heroDamageData.GetValue(this.Level) : this.creepDamageData.GetValue(this.Level));
			}
			return damage;
		}

		// Token: 0x04000742 RID: 1858
		private readonly SpecialData creepDamageData;

		// Token: 0x04000743 RID: 1859
		private readonly SpecialData heroDamageData;
	}
}
