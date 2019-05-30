using System;
using System.Collections.Generic;

namespace O9K.Core.Managers.Menu.Items
{
	// Token: 0x0200004E RID: 78
	public class MenuSelector : MenuSelector<string>
	{
		// Token: 0x06000276 RID: 630 RVA: 0x00003914 File Offset: 0x00001B14
		public MenuSelector(string displayName, IEnumerable<string> items = null, bool heroUnique = false) : base(displayName, items, heroUnique)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000391F File Offset: 0x00001B1F
		public MenuSelector(string displayName, string name, IEnumerable<string> items = null, bool heroUnique = false) : base(displayName, name, items, heroUnique)
		{
		}
	}
}
