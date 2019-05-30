using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
	// Token: 0x02000340 RID: 832
	[AbilityId(AbilityId.lone_druid_true_form_battle_cry)]
	public class BattleCry : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000E33 RID: 3635 RVA: 0x0000C867 File Offset: 0x0000AA67
		public BattleCry(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005DF RID: 1503
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0000C882 File Offset: 0x0000AA82
		public string BuffModifierName { get; } = "modifier_lone_druid_true_form_battle_cry";

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0000C88A File Offset: 0x0000AA8A
		public bool BuffsAlly { get; }

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x0000C892 File Offset: 0x0000AA92
		public bool BuffsOwner { get; } = 1;
	}
}
