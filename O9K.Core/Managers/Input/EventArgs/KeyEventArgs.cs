using System;
using System.Windows.Input;
using Ensage;

namespace O9K.Core.Managers.Input.EventArgs
{
	// Token: 0x02000064 RID: 100
	public sealed class KeyEventArgs : EventArgs
	{
		// Token: 0x06000329 RID: 809 RVA: 0x00004049 File Offset: 0x00002249
		public KeyEventArgs(WndEventArgs args)
		{
			this.Key = KeyInterop.KeyFromVirtualKey((int)args.WParam);
			this.Process = args.Process;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600032A RID: 810 RVA: 0x0000406F File Offset: 0x0000226F
		// (set) Token: 0x0600032B RID: 811 RVA: 0x00004077 File Offset: 0x00002277
		public bool Process { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600032C RID: 812 RVA: 0x00004080 File Offset: 0x00002280
		public Key Key { get; }
	}
}
