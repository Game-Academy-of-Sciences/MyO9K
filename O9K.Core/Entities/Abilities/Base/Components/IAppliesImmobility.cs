using System;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003FC RID: 1020
	public interface IAppliesImmobility
	{
		// Token: 0x17000787 RID: 1927
		// (get) Token: 0x06001131 RID: 4401
		string ImmobilityModifierName { get; }

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x06001132 RID: 4402
		bool IsValid { get; }
	}
}
