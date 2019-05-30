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
	// Token: 0x0200018D RID: 397
	[AbilityId(AbilityId.item_guardian_greaves)]
	public class GuardianGreaves : AreaOfEffectAbility, IHealthRestore, IManaRestore, IActiveAbility
	{
		// Token: 0x06000801 RID: 2049 RVA: 0x000220B4 File Offset: 0x000202B4
		public GuardianGreaves(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "replenish_radius");
			this.healthRestoreData = new SpecialData(baseAbility, "replenish_health");
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000802 RID: 2050 RVA: 0x00007737 File Offset: 0x00005937
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000803 RID: 2051 RVA: 0x0000773F File Offset: 0x0000593F
		public string HealModifierName { get; } = "modifier_item_mekansm_noheal";

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000804 RID: 2052 RVA: 0x00007747 File Offset: 0x00005947
		public bool RestoresAlly { get; } = 1;

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000805 RID: 2053 RVA: 0x0000774F File Offset: 0x0000594F
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000806 RID: 2054 RVA: 0x00007757 File Offset: 0x00005957
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x040003C2 RID: 962
		private readonly SpecialData healthRestoreData;
	}
}
