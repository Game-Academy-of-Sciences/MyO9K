using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Abaddon
{
	// Token: 0x020003DA RID: 986
	[AbilityId(AbilityId.abaddon_death_coil)]
	public class MistCoil : RangedAbility, IHealthRestore, INuke, IHasHealthCost, IActiveAbility
	{
		// Token: 0x0600106C RID: 4204 RVA: 0x00028D60 File Offset: 0x00026F60
		public MistCoil(Ability baseAbility) : base(baseAbility)
		{
			this.SpeedData = new SpecialData(baseAbility, "missile_speed");
			this.DamageData = new SpecialData(baseAbility, "target_damage");
			this.healthCostData = new SpecialData(baseAbility, "self_damage");
			this.healthRestoreData = new SpecialData(baseAbility, "heal_amount");
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x0000E74B File Offset: 0x0000C94B
		public bool InstantHealthRestore { get; } = 1;

		// Token: 0x17000714 RID: 1812
		// (get) Token: 0x0600106E RID: 4206 RVA: 0x0000E753 File Offset: 0x0000C953
		public bool CanSuicide { get; } = 1;

		// Token: 0x17000715 RID: 1813
		// (get) Token: 0x0600106F RID: 4207 RVA: 0x0000E75B File Offset: 0x0000C95B
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000716 RID: 1814
		// (get) Token: 0x06001070 RID: 4208 RVA: 0x0000E763 File Offset: 0x0000C963
		public int HealthCost
		{
			get
			{
				return (int)this.healthCostData.GetValue(this.Level);
			}
		}

		// Token: 0x17000717 RID: 1815
		// (get) Token: 0x06001071 RID: 4209 RVA: 0x0000E777 File Offset: 0x0000C977
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000718 RID: 1816
		// (get) Token: 0x06001072 RID: 4210 RVA: 0x0000E77F File Offset: 0x0000C97F
		public bool RestoresOwner { get; }

		// Token: 0x06001073 RID: 4211 RVA: 0x0000E787 File Offset: 0x0000C987
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)this.healthRestoreData.GetValue(this.Level);
		}

		// Token: 0x04000894 RID: 2196
		private readonly SpecialData healthCostData;

		// Token: 0x04000895 RID: 2197
		private readonly SpecialData healthRestoreData;
	}
}
