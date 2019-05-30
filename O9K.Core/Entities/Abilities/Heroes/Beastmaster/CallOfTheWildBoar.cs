using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Beastmaster
{
	// Token: 0x020003C5 RID: 965
	[AbilityId(AbilityId.beastmaster_call_of_the_wild_boar)]
	public class CallOfTheWildBoar : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x06001017 RID: 4119 RVA: 0x0000E2A2 File Offset: 0x0000C4A2
		public CallOfTheWildBoar(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x0000E2BD File Offset: 0x0000C4BD
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x06001019 RID: 4121 RVA: 0x0000E2C5 File Offset: 0x0000C4C5
		public bool BuffsAlly { get; }

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x0000E2CD File Offset: 0x0000C4CD
		public bool BuffsOwner { get; } = 1;
	}
}
