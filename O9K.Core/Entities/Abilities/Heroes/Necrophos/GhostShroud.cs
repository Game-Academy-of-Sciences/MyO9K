using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Necrophos
{
	// Token: 0x0200031D RID: 797
	[AbilityId(AbilityId.necrolyte_sadist)]
	public class GhostShroud : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x06000DC6 RID: 3526 RVA: 0x0000C2D7 File Offset: 0x0000A4D7
		public GhostShroud(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0000C2FA File Offset: 0x0000A4FA
		public UnitState AppliesUnitState { get; } = 6L;

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x06000DC8 RID: 3528 RVA: 0x0000C302 File Offset: 0x0000A502
		public string ShieldModifierName { get; } = "modifier_necrolyte_sadist_active";

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x0000C30A File Offset: 0x0000A50A
		public bool ShieldsAlly { get; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x06000DCA RID: 3530 RVA: 0x0000C312 File Offset: 0x0000A512
		public bool ShieldsOwner { get; } = 1;
	}
}
