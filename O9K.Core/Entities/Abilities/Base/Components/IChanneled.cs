using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003F8 RID: 1016
	public interface IChanneled : IActiveAbility
	{
		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06001126 RID: 4390
		float ChannelTime { get; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06001127 RID: 4391
		bool IsActivatesOnChannelStart { get; }
	}
}
