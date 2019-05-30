using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x02000183 RID: 387
	[AbilityId(AbilityId.item_crimson_guard)]
	public class CrimsonGuard : AreaOfEffectAbility, IShield, IActiveAbility
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00007447 File Offset: 0x00005647
		public CrimsonGuard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "bonus_aoe_radius");
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060007C3 RID: 1987 RVA: 0x0000747A File Offset: 0x0000567A
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060007C4 RID: 1988 RVA: 0x00007482 File Offset: 0x00005682
		public string ShieldModifierName { get; } = "modifier_item_crimson_guard_nostack";

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060007C5 RID: 1989 RVA: 0x0000748A File Offset: 0x0000568A
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060007C6 RID: 1990 RVA: 0x00007492 File Offset: 0x00005692
		public bool ShieldsOwner { get; } = 1;
	}
}
