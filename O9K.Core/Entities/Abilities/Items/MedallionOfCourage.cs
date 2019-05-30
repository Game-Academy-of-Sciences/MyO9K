using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000194 RID: 404
	[AbilityId(AbilityId.item_medallion_of_courage)]
	public class MedallionOfCourage : RangedAbility, IDebuff, IShield, IActiveAbility
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x0000795E File Offset: 0x00005B5E
		public MedallionOfCourage(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00007984 File Offset: 0x00005B84
		public UnitState AppliesUnitState { get; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x0600082C RID: 2092 RVA: 0x0000798C File Offset: 0x00005B8C
		public override bool BreaksLinkens { get; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00007994 File Offset: 0x00005B94
		public string DebuffModifierName { get; } = "modifier_item_medallion_of_courage_armor_reduction";

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x0600082E RID: 2094 RVA: 0x0000799C File Offset: 0x00005B9C
		public string ShieldModifierName { get; } = "modifier_item_medallion_of_courage_armor_addition";

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x000079A4 File Offset: 0x00005BA4
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000830 RID: 2096 RVA: 0x000079AC File Offset: 0x00005BAC
		public bool ShieldsOwner { get; }
	}
}
