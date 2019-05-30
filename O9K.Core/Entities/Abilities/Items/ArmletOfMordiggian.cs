using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200017C RID: 380
	[AbilityId(AbilityId.item_armlet)]
	public class ArmletOfMordiggian : ToggleAbility, IBuff, IActiveAbility
	{
		// Token: 0x060007A0 RID: 1952 RVA: 0x00007254 File Offset: 0x00005454
		public ArmletOfMordiggian(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060007A1 RID: 1953 RVA: 0x0000726F File Offset: 0x0000546F
		public string BuffModifierName { get; } = "modifier_item_armlet_unholy_strength";

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x00007277 File Offset: 0x00005477
		public bool BuffsAlly { get; }

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x060007A3 RID: 1955 RVA: 0x0000727F File Offset: 0x0000547F
		public bool BuffsOwner { get; } = 1;
	}
}
