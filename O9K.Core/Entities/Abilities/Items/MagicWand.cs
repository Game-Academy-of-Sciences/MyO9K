using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000192 RID: 402
	[AbilityId(AbilityId.item_magic_stick)]
	[AbilityId(AbilityId.item_magic_wand)]
	public class MagicWand : ActiveAbility, IHealthRestore, IManaRestore, IActiveAbility
	{
		// Token: 0x0600081F RID: 2079 RVA: 0x000078B6 File Offset: 0x00005AB6
		public MagicWand(Ability baseAbility) : base(baseAbility)
		{
			this.restoreData = new SpecialData(baseAbility, "restore_per_charge");
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000820 RID: 2080 RVA: 0x000078E9 File Offset: 0x00005AE9
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000821 RID: 2081 RVA: 0x000078F1 File Offset: 0x00005AF1
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x000078F9 File Offset: 0x00005AF9
		public bool RestoresAlly { get; }

		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000823 RID: 2083 RVA: 0x00007901 File Offset: 0x00005B01
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000824 RID: 2084 RVA: 0x0000754C File Offset: 0x0000574C
		public override bool CanBeCasted(bool checkChanneling = true)
		{
			return base.BaseItem.CurrentCharges > 0u && base.CanBeCasted(checkChanneling);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00007909 File Offset: 0x00005B09
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)(base.BaseItem.CurrentCharges * this.restoreData.GetValue(this.Level));
		}

		// Token: 0x040003D9 RID: 985
		private readonly SpecialData restoreData;
	}
}
