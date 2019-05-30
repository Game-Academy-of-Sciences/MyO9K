using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005C RID: 92
	public abstract class MenuEventArgs<T> : EventArgs
	{
		// Token: 0x060002FF RID: 767 RVA: 0x00003FBB File Offset: 0x000021BB
		protected MenuEventArgs(T newValue, T oldValue)
		{
			this.NewValue = newValue;
			this.OldValue = oldValue;
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00003FD1 File Offset: 0x000021D1
		public T NewValue { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00003FD9 File Offset: 0x000021D9
		public T OldValue { get; }
	}
}
