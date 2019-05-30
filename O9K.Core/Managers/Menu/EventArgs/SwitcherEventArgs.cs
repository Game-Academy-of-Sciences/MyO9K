using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x02000060 RID: 96
	public class SwitcherEventArgs : MenuEventArgs<bool>
	{
		// Token: 0x06000306 RID: 774 RVA: 0x0000400E File Offset: 0x0000220E
		public SwitcherEventArgs(bool newValue, bool oldValue) : base(newValue, oldValue)
		{
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000401F File Offset: 0x0000221F
		// (set) Token: 0x06000308 RID: 776 RVA: 0x00004027 File Offset: 0x00002227
		public bool Process { get; set; } = true;
	}
}
