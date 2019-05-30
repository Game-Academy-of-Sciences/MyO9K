using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Magnus
{
	// Token: 0x02000209 RID: 521
	[AbilityId(AbilityId.magnataur_empower)]
	public class Empower : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x00009095 File Offset: 0x00007295
		public Empower(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x000090B7 File Offset: 0x000072B7
		public string BuffModifierName { get; } = "modifier_magnataur_empower";

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x000090BF File Offset: 0x000072BF
		public bool BuffsAlly { get; } = 1;

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000A06 RID: 2566 RVA: 0x000090C7 File Offset: 0x000072C7
		public bool BuffsOwner { get; } = 1;
	}
}
