using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000151 RID: 337
	[AbilityId(AbilityId.item_shadow_amulet)]
	public class ShadowAmulet : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000714 RID: 1812 RVA: 0x00006B33 File Offset: 0x00004D33
		public ShadowAmulet(Ability baseAbility) : base(baseAbility)
		{
			base.IsInvisibility = true;
		}

		// Token: 0x170001D1 RID: 465
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x00006B5C File Offset: 0x00004D5C
		public string ShieldModifierName { get; } = "modifier_item_shadow_amulet_fade";

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x00006B64 File Offset: 0x00004D64
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x00006B6C File Offset: 0x00004D6C
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x00006B74 File Offset: 0x00004D74
		public UnitState AppliesUnitState { get; }
	}
}
