using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005D RID: 93
	internal class PriorityEventArgs : MenuEventArgs<int>
	{
		// Token: 0x06000302 RID: 770 RVA: 0x00003FE1 File Offset: 0x000021E1
		public PriorityEventArgs(string ability, int newValue, int oldValue) : base(newValue, oldValue)
		{
			this.Ability = ability;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000303 RID: 771 RVA: 0x00003FF2 File Offset: 0x000021F2
		public string Ability { get; }
	}
}
