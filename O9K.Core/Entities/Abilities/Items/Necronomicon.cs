using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000138 RID: 312
	[AbilityId(AbilityId.item_necronomicon)]
	[AbilityId(AbilityId.item_necronomicon_2)]
	[AbilityId(AbilityId.item_necronomicon_3)]
	public class Necronomicon : ActiveAbility, IBuff, IActiveAbility
	{
		// Token: 0x060006F2 RID: 1778 RVA: 0x00006A2D File Offset: 0x00004C2D
		public Necronomicon(Ability baseAbility) : base(baseAbility)
		{
			base.Name = "item_necronomicon_3";
			base.Id = AbilityId.item_necronomicon_3;
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00006A5E File Offset: 0x00004C5E
		public string BuffModifierName { get; } = string.Empty;

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x00006A66 File Offset: 0x00004C66
		public bool BuffsAlly { get; }

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060006F5 RID: 1781 RVA: 0x00006A6E File Offset: 0x00004C6E
		public bool BuffsOwner { get; } = 1;
	}
}
