using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Brewmaster
{
	// Token: 0x020003B0 RID: 944
	[AbilityId(AbilityId.brewmaster_drunken_brawler)]
	public class DrunkenBrawler : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000FEA RID: 4074 RVA: 0x0000E06D File Offset: 0x0000C26D
		public DrunkenBrawler(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06000FEB RID: 4075 RVA: 0x0000E088 File Offset: 0x0000C288
		public string BuffModifierName { get; } = "modifier_brewmaster_drunken_brawler";

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06000FEC RID: 4076 RVA: 0x0000E090 File Offset: 0x0000C290
		public bool BuffsAlly { get; }

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06000FED RID: 4077 RVA: 0x0000E098 File Offset: 0x0000C298
		public bool BuffsOwner { get; } = 1;
	}
}
