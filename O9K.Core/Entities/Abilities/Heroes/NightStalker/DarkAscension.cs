using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.NightStalker
{
	// Token: 0x020001F4 RID: 500
	[AbilityId(AbilityId.night_stalker_darkness)]
	public class DarkAscension : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x060009CA RID: 2506 RVA: 0x00008D52 File Offset: 0x00006F52
		public DarkAscension(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x00008D6D File Offset: 0x00006F6D
		public string BuffModifierName { get; } = "modifier_night_stalker_darkness";

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x00008D75 File Offset: 0x00006F75
		public bool BuffsAlly { get; }

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00008D7D File Offset: 0x00006F7D
		public bool BuffsOwner { get; } = 1;
	}
}
