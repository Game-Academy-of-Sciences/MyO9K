using System;
using System.ComponentModel.Composition;

namespace O9K.Hud.Core
{
	// Token: 0x020000B1 RID: 177
	[InheritedExport]
	internal interface IHudModule : IDisposable
	{
		// Token: 0x060003F7 RID: 1015
		void Activate();
	}
}
