using System;

namespace O9K.Core.Managers.Menu.EventArgs
{
	// Token: 0x02000059 RID: 89
	public class AbilityPriorityEventArgs : MenuEventArgs<int>
	{
		// Token: 0x060002FA RID: 762 RVA: 0x00003F7F File Offset: 0x0000217F
		public AbilityPriorityEventArgs(string ability, int newValue, int oldValue) : base(newValue, oldValue)
		{
			this.Ability = ability;
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00003F90 File Offset: 0x00002190
		public string Ability { get; }
	}
}
