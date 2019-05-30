using System;
using O9K.Core.Managers.Menu.Items;

namespace O9K.Hud.Modules.Units.Ranges.Abilities
{
	// Token: 0x02000010 RID: 16
	internal interface IRange : IDisposable
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000053 RID: 83
		bool IsEnabled { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000054 RID: 84
		string Name { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000055 RID: 85
		uint Handle { get; }

		// Token: 0x06000056 RID: 86
		void Enable(Menu heroMenu);

		// Token: 0x06000057 RID: 87
		void UpdateRange();
	}
}
