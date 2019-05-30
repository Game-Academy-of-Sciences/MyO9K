using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Ursa
{
	// Token: 0x0200028A RID: 650
	[AbilityId(AbilityId.ursa_overpower)]
	public class Overpower : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000BA4 RID: 2980 RVA: 0x0000A7CF File Offset: 0x000089CF
		public Overpower(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06000BA5 RID: 2981 RVA: 0x0000A7EA File Offset: 0x000089EA
		public string BuffModifierName { get; } = "modifier_ursa_overpower";

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x0000A7F2 File Offset: 0x000089F2
		public bool BuffsAlly { get; }

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06000BA7 RID: 2983 RVA: 0x0000A7FA File Offset: 0x000089FA
		public bool BuffsOwner { get; } = 1;
	}
}
