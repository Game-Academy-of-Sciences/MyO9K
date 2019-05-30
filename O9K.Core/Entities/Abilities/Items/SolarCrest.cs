using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200019E RID: 414
	[AbilityId(AbilityId.item_solar_crest)]
	public class SolarCrest : RangedAbility, IDebuff, IShield, IActiveAbility
	{
		// Token: 0x0600085F RID: 2143 RVA: 0x00007BDB File Offset: 0x00005DDB
		public SolarCrest(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00007C01 File Offset: 0x00005E01
		public UnitState AppliesUnitState { get; }

		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x00007C09 File Offset: 0x00005E09
		public override bool BreaksLinkens { get; }

		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00007C11 File Offset: 0x00005E11
		public string DebuffModifierName { get; } = "modifier_item_solar_crest_armor_reduction";

		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06000863 RID: 2147 RVA: 0x00007C19 File Offset: 0x00005E19
		public string ShieldModifierName { get; } = "modifier_item_solar_crest_armor_addition";

		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00007C21 File Offset: 0x00005E21
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x00007C29 File Offset: 0x00005E29
		public bool ShieldsOwner { get; }
	}
}
