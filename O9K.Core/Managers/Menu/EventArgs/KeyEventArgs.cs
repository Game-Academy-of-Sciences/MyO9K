using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005B RID: 91
	public class KeyEventArgs : MenuEventArgs<bool>
	{
		// Token: 0x060002FE RID: 766 RVA: 0x00003FB1 File Offset: 0x000021B1
		public KeyEventArgs(bool newValue, bool oldValue) : base(newValue, oldValue)
		{
		}
	}
}
