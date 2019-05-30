using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003F0 RID: 1008
	public interface IDisable : IActiveAbility
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600110A RID: 4362
		UnitState AppliesUnitState { get; }
	}
}
