using System;
using Ensage;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Base.Components
{
	// Token: 0x020003FE RID: 1022
	public interface IHasDamageBlock
	{
		// Token: 0x17000789 RID: 1929
		// (get) Token: 0x06001133 RID: 4403
		DamageType BlockDamageType { get; }

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06001134 RID: 4404
		string BlockModifierName { get; }

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06001135 RID: 4405
		bool IsDamageBlockPermanent { get; }

		// Token: 0x1700078C RID: 1932
		// (get) Token: 0x06001136 RID: 4406
		bool IsValid { get; }

		// Token: 0x1700078D RID: 1933
		// (get) Token: 0x06001137 RID: 4407
		bool BlocksDamageAfterReduction { get; }

		// Token: 0x06001138 RID: 4408
		float BlockValue(Unit9 target);
	}
}
