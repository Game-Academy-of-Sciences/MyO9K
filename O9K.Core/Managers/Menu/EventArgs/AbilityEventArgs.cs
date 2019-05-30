using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x02000058 RID: 88
	public class AbilityEventArgs : MenuEventArgs<bool>
	{
		// Token: 0x060002F8 RID: 760 RVA: 0x00003F66 File Offset: 0x00002166
		public AbilityEventArgs(string ability, bool newValue, bool oldValue) : base(newValue, oldValue)
		{
			this.Ability = ability;
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00003F77 File Offset: 0x00002177
		public string Ability { get; }
	}
}
