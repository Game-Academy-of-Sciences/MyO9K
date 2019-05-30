using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200018B RID: 395
	[AbilityId(AbilityId.item_ghost)]
	public class GhostScepter : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x060007F5 RID: 2037 RVA: 0x000076A3 File Offset: 0x000058A3
		public GhostScepter(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060007F6 RID: 2038 RVA: 0x000076C6 File Offset: 0x000058C6
		public UnitState AppliesUnitState { get; } = 6L;

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x060007F7 RID: 2039 RVA: 0x000076CE File Offset: 0x000058CE
		public string ShieldModifierName { get; } = "modifier_ghost_state";

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x060007F8 RID: 2040 RVA: 0x000076D6 File Offset: 0x000058D6
		public bool ShieldsAlly { get; }

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x060007F9 RID: 2041 RVA: 0x000076DE File Offset: 0x000058DE
		public bool ShieldsOwner { get; } = 1;
	}
}
