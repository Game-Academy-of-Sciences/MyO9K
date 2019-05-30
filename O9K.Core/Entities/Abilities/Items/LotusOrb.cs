using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000191 RID: 401
	[AbilityId(AbilityId.item_lotus_orb)]
	public class LotusOrb : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x0600081A RID: 2074 RVA: 0x00007874 File Offset: 0x00005A74
		public LotusOrb(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x0600081B RID: 2075 RVA: 0x00007896 File Offset: 0x00005A96
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0000789E File Offset: 0x00005A9E
		public string ShieldModifierName { get; } = "modifier_item_lotus_orb_active";

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x000078A6 File Offset: 0x00005AA6
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x000078AE File Offset: 0x00005AAE
		public bool ShieldsOwner { get; } = 1;
	}
}
