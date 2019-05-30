using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003F3 RID: 1011
	public interface INuke : IActiveAbility
	{
		// Token: 0x06001112 RID: 4370
		int GetDamage(Unit9 unit);
	}
}
