using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019B RID: 411
	[AbilityId(AbilityId.item_satanic)]
	public class Satanic : ActiveAbility, IBuff, IHasLifeSteal, IActiveAbility
	{
		// Token: 0x06000855 RID: 2133 RVA: 0x00007B6B File Offset: 0x00005D6B
		public Satanic(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000856 RID: 2134 RVA: 0x00007B86 File Offset: 0x00005D86
		public string BuffModifierName { get; } = "modifier_item_satanic_unholy";

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000857 RID: 2135 RVA: 0x00007B8E File Offset: 0x00005D8E
		public bool BuffsAlly { get; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x00007B96 File Offset: 0x00005D96
		public bool BuffsOwner { get; } = 1;
	}
}
