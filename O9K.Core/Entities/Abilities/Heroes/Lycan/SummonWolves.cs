using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lycan
{
	// Token: 0x0200033A RID: 826
	[AbilityId(AbilityId.lycan_summon_wolves)]
	public class SummonWolves : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000E25 RID: 3621 RVA: 0x0000C7BC File Offset: 0x0000A9BC
		public SummonWolves(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005D7 RID: 1495
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0000C7D7 File Offset: 0x0000A9D7
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x170005D8 RID: 1496
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0000C7DF File Offset: 0x0000A9DF
		public bool BuffsAlly { get; }

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0000C7E7 File Offset: 0x0000A9E7
		public bool BuffsOwner { get; } = 1;
	}
}
