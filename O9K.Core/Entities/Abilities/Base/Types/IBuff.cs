using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003EE RID: 1006
	public interface IBuff : IActiveAbility
	{
		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001106 RID: 4358
		string BuffModifierName { get; }

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001107 RID: 4359
		bool BuffsAlly { get; }

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001108 RID: 4360
		bool BuffsOwner { get; }
	}
}
