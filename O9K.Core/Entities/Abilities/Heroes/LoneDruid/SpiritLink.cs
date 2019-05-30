using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.LoneDruid
{
	// Token: 0x02000344 RID: 836
	[AbilityId(AbilityId.lone_druid_spirit_link)]
	public class SpiritLink : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000E3E RID: 3646 RVA: 0x0000C8D4 File Offset: 0x0000AAD4
		public SpiritLink(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x06000E3F RID: 3647 RVA: 0x0000C8EF File Offset: 0x0000AAEF
		public string BuffModifierName { get; } = "modifier_lone_druid_spirit_link";

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x06000E40 RID: 3648 RVA: 0x0000C8F7 File Offset: 0x0000AAF7
		public bool BuffsAlly { get; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06000E41 RID: 3649 RVA: 0x0000C8FF File Offset: 0x0000AAFF
		public bool BuffsOwner { get; } = 1;
	}
}
