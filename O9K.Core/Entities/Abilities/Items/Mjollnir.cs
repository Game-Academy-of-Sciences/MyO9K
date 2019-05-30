using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000196 RID: 406
	[AbilityId(AbilityId.item_mjollnir)]
	public class Mjollnir : RangedAbility, IShield, IActiveAbility
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x000079E8 File Offset: 0x00005BE8
		public Mjollnir(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000838 RID: 2104 RVA: 0x00007A0A File Offset: 0x00005C0A
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x00007A12 File Offset: 0x00005C12
		public string ShieldModifierName { get; } = "modifier_item_mjollnir_static";

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x00007A1A File Offset: 0x00005C1A
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00007A22 File Offset: 0x00005C22
		public bool ShieldsOwner { get; } = 1;
	}
}
