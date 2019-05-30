using System;
using System.Collections.Generic;
using Ensage;

namespace O9K.Core.Entities.Abilities.Heroes.Invoker.Helpers
{
	// Token: 0x02000366 RID: 870
	public interface IInvokableAbility
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x06000F05 RID: 3845
		AbilityId[] RequiredOrbs { get; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06000F06 RID: 3846
		bool IsInvoked { get; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06000F07 RID: 3847
		bool CanBeInvoked { get; }

		// Token: 0x06000F08 RID: 3848
		bool Invoke(List<AbilityId> currentOrbs = null, bool queue = false, bool bypass = false);
	}
}
