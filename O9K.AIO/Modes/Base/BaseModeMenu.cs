using System;
using O9K.Core.Managers.Menu.Items;

namespace O9K.AIO.Modes.Base
{
	// Token: 0x0200002B RID: 43
	internal class BaseModeMenu
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00002AA5 File Offset: 0x00000CA5
		public BaseModeMenu(Menu rootMenu, string displayName)
		{
			this.SimplifiedName = displayName.Replace(" ", "").ToLower();
			this.Menu = new Menu(displayName, this.SimplifiedName);
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00002ADA File Offset: 0x00000CDA
		public string SimplifiedName { get; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002AE2 File Offset: 0x00000CE2
		protected Menu Menu { get; }
	}
}
