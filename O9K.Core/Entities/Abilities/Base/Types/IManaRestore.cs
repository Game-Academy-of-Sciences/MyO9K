using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003F2 RID: 1010
	public interface IManaRestore : IActiveAbility
	{
		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001110 RID: 4368
		bool RestoresAlly { get; }

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001111 RID: 4369
		bool RestoresOwner { get; }
	}
}
