using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005F RID: 95
	public class SliderEventArgs : MenuEventArgs<int>
	{
		// Token: 0x06000305 RID: 773 RVA: 0x00004004 File Offset: 0x00002204
		public SliderEventArgs(int newValue, int oldValue) : base(newValue, oldValue)
		{
		}
	}
}
