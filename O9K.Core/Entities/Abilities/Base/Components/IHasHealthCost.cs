using System;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003FB RID: 1019
	public interface IHasHealthCost
	{
		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x0600112F RID: 4399
		bool CanSuicide { get; }

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x06001130 RID: 4400
		int HealthCost { get; }
	}
}
