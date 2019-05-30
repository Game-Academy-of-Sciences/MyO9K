using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003F1 RID: 1009
	public interface IHealthRestore : IActiveAbility
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x0600110B RID: 4363
		string HealModifierName { get; }

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600110C RID: 4364
		bool RestoresAlly { get; }

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600110D RID: 4365
		bool InstantHealthRestore { get; }

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600110E RID: 4366
		bool RestoresOwner { get; }

		// Token: 0x0600110F RID: 4367
		int HealthRestoreValue(Unit9 unit);
	}
}
