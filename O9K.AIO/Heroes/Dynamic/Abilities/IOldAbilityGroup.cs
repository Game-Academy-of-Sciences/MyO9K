using System;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Dynamic.Abilities
{
	// Token: 0x0200019D RID: 413
	internal interface IOldAbilityGroup
	{
		// Token: 0x0600085E RID: 2142
		bool AddAbility(Ability9 ability);

		// Token: 0x0600085F RID: 2143
		void RemoveAbility(Ability9 ability);
	}
}
