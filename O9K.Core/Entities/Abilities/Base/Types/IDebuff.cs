using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003EF RID: 1007
	public interface IDebuff : IActiveAbility
	{
		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001109 RID: 4361
		string DebuffModifierName { get; }
	}
}
