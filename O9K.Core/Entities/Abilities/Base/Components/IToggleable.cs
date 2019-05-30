using System;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003F7 RID: 1015
	public interface IToggleable
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001121 RID: 4385
		// (set) Token: 0x06001122 RID: 4386
		bool Enabled { get; set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001123 RID: 4387
		bool IsValid { get; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001124 RID: 4388
		Unit9 Owner { get; }

		// Token: 0x06001125 RID: 4389
		bool CanBeCasted(bool checkChanneling = true);
	}
}
