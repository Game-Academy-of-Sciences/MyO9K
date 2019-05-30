using System;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers.Damage;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003F5 RID: 1013
	public interface IHasPassiveDamageIncrease
	{
		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001117 RID: 4375
		bool IsPassiveDamagePermanent { get; }

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001118 RID: 4376
		bool IsValid { get; }

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001119 RID: 4377
		bool MultipliedByCrit { get; }

		// Token: 0x17000774 RID: 1908
		// (get) Token: 0x0600111A RID: 4378
		string PassiveDamageModifierName { get; }

		// Token: 0x0600111B RID: 4379
		Damage GetRawDamage(Unit9 unit, float? remainingHealth = null);
	}
}
