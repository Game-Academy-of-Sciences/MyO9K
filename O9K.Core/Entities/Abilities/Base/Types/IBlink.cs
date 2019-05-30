using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003EB RID: 1003
	public interface IBlink : IActiveAbility
	{
		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06001103 RID: 4355
		BlinkType BlinkType { get; }

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001104 RID: 4356
		float Range { get; }
	}
}
