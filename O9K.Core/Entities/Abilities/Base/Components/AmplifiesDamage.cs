using System;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003F9 RID: 1017
	[Flags]
	public enum AmplifiesDamage
	{
		// Token: 0x040008D2 RID: 2258
		None = 0,
		// Token: 0x040008D3 RID: 2259
		Incoming = 1,
		// Token: 0x040008D4 RID: 2260
		Outgoing = 2,
		// Token: 0x040008D5 RID: 2261
		All = 3
	}
}
