using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003F4 RID: 1012
	public interface IShield : IActiveAbility
	{
		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06001113 RID: 4371
		string ShieldModifierName { get; }

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001114 RID: 4372
		bool ShieldsAlly { get; }

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001115 RID: 4373
		bool ShieldsOwner { get; }

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001116 RID: 4374
		UnitState AppliesUnitState { get; }
	}
}
