using System;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Types
{
	// Token: 0x020003EC RID: 1004
	public interface ISpeedBuff : IBuff, IActiveAbility
	{
		// Token: 0x06001105 RID: 4357
		float GetSpeedBuff(Unit9 unit);
	}
}
