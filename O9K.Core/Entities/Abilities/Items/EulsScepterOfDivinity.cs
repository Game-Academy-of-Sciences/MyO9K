using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000189 RID: 393
	[AbilityId(AbilityId.item_cyclone)]
	public class EulsScepterOfDivinity : RangedAbility, IDisable, IShield, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x060007E9 RID: 2025 RVA: 0x00022064 File Offset: 0x00020264
		public EulsScepterOfDivinity(Ability baseAbility) : base(baseAbility)
		{
			this.DurationData = new SpecialData(baseAbility, "cyclone_duration");
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x00007614 File Offset: 0x00005814
		public UnitState AppliesUnitState { get; } = 256L;

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060007EB RID: 2027 RVA: 0x0000761C File Offset: 0x0000581C
		public string ImmobilityModifierName { get; } = "modifier_eul_cyclone";

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x00007624 File Offset: 0x00005824
		public string ShieldModifierName { get; } = "modifier_eul_cyclone";

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007ED RID: 2029 RVA: 0x0000762C File Offset: 0x0000582C
		public bool ShieldsAlly { get; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007EE RID: 2030 RVA: 0x00007634 File Offset: 0x00005834
		public bool ShieldsOwner { get; } = 1;
	}
}
