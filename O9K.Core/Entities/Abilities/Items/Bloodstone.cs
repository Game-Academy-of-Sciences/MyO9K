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
	// Token: 0x0200016C RID: 364
	[AbilityId(AbilityId.item_bloodstone)]
	public class Bloodstone : ActiveAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000753 RID: 1875 RVA: 0x00006DC6 File Offset: 0x00004FC6
		public Bloodstone(Ability baseAbility) : base(baseAbility)
		{
			this.regen = new SpecialData(baseAbility, "mana_cost_percentage");
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000754 RID: 1876 RVA: 0x00006DF2 File Offset: 0x00004FF2
		public string HealModifierName { get; } = "modifier_item_bloodstone_active";

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x00006DFA File Offset: 0x00004FFA
		public bool RestoresAlly { get; }

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000756 RID: 1878 RVA: 0x00006E02 File Offset: 0x00005002
		public bool InstantHealthRestore { get; }

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000757 RID: 1879 RVA: 0x00006E0A File Offset: 0x0000500A
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000758 RID: 1880 RVA: 0x00006E12 File Offset: 0x00005012
		public int HealthRestoreValue(Unit9 unit)
		{
			return (int)(unit.Mana * (this.regen.GetValue(this.Level) / 100f));
		}

		// Token: 0x04000343 RID: 835
		private readonly SpecialData regen;
	}
}
