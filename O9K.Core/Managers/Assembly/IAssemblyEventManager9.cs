using System;

namespace O9K.Core.Managers.Assembly
{
	// Token: 0x0200007C RID: 124
	public interface IAssemblyEventManager9
	{
		// Token: 0x14000029 RID: 41
		// (add) Token: 0x060003FD RID: 1021
		// (remove) Token: 0x060003FE RID: 1022
		event EventHandler<string> OnAssemblyLoad;

		// Token: 0x1400002A RID: 42
		// (add) Token: 0x060003FF RID: 1023
		// (remove) Token: 0x06000400 RID: 1024
		event EventHandler OnEvaderPredictedDeath;

		// Token: 0x06000401 RID: 1025
		void AssemblyLoaded();

		// Token: 0x06000402 RID: 1026
		void InvokeOnEvaderPredictedDeath();
	}
}
