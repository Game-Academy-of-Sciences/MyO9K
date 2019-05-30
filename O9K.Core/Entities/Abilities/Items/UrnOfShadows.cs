using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x020001A0 RID: 416
	[AbilityId(AbilityId.item_urn_of_shadows)]
	public class UrnOfShadows : RangedAbility, IDebuff, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x00007C9D File Offset: 0x00005E9D
		public UrnOfShadows(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000871 RID: 2161 RVA: 0x00007CD1 File Offset: 0x00005ED1
		public override DamageType DamageType { get; } = 2;

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000872 RID: 2162 RVA: 0x00007CD9 File Offset: 0x00005ED9
		public override bool BreaksLinkens { get; }

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000873 RID: 2163 RVA: 0x00007CE1 File Offset: 0x00005EE1
		public bool InstantHealthRestore { get; }

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000874 RID: 2164 RVA: 0x00007CE9 File Offset: 0x00005EE9
		public string DebuffModifierName { get; } = "modifier_item_urn_damage";

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000875 RID: 2165 RVA: 0x00007CF1 File Offset: 0x00005EF1
		public string HealModifierName { get; } = "modifier_item_urn_heal";

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000876 RID: 2166 RVA: 0x00007CF9 File Offset: 0x00005EF9
		public bool RestoresAlly { get; } = 1;

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00007D01 File Offset: 0x00005F01
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000878 RID: 2168 RVA: 0x0000754C File Offset: 0x0000574C
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.BaseItem.CurrentCharges > 0u && base.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
