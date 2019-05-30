using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x0200005A RID: 90
	public class HeroEventArgs : MenuEventArgs<bool>
	{
		// Token: 0x060002FC RID: 764 RVA: 0x00003F98 File Offset: 0x00002198
		public HeroEventArgs(string hero, bool newValue, bool oldValue) : base(newValue, oldValue)
		{
			this.Hero = hero;
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002FD RID: 765 RVA: 0x00003FA9 File Offset: 0x000021A9
		public string Hero { get; }
	}
}
