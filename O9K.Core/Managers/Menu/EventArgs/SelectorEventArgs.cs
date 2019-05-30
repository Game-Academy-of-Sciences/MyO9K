using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005E RID: 94
	public class SelectorEventArgs<T> : MenuEventArgs<T>
	{
		// Token: 0x06000304 RID: 772 RVA: 0x00003FFA File Offset: 0x000021FA
		public SelectorEventArgs(T newValue, T oldValue) : base(newValue, oldValue)
		{
		}
	}
}
