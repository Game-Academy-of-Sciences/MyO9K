using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Broodmother
{
	// Token: 0x020003AA RID: 938
	[AbilityId(AbilityId.broodmother_insatiable_hunger)]
	public class InsatiableHunger : ActiveAbility, IBuff, IHasLifeSteal, IActiveAbility
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x0000DFBA File Offset: 0x0000C1BA
		public InsatiableHunger(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0000DFD5 File Offset: 0x0000C1D5
		public string BuffModifierName { get; } = "modifier_broodmother_insatiable_hunger";

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06000FDB RID: 4059 RVA: 0x0000DFDD File Offset: 0x0000C1DD
		public bool BuffsAlly { get; }

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
		public bool BuffsOwner { get; } = 1;
	}
}
