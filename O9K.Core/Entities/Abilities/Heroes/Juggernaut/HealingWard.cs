using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Heroes.Juggernaut
{
	// Token: 0x0200035B RID: 859
	[AbilityId(AbilityId.juggernaut_healing_ward)]
	public class HealingWard : CircleAbility, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000E8D RID: 3725 RVA: 0x0000CCC7 File Offset: 0x0000AEC7
		public HealingWard(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "healing_ward_aura_radius");
		}

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06000E8E RID: 3726 RVA: 0x0000CCEC File Offset: 0x0000AEEC
		public bool InstantHealthRestore { get; }

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06000E8F RID: 3727 RVA: 0x0000CCF4 File Offset: 0x0000AEF4
		public string HealModifierName { get; } = string.Empty;

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06000E90 RID: 3728 RVA: 0x0000CCFC File Offset: 0x0000AEFC
		// (set) Token: 0x06000E91 RID: 3729 RVA: 0x0000CD04 File Offset: 0x0000AF04
		public bool RestoresAlly { get; set; }

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06000E92 RID: 3730 RVA: 0x0000CD0D File Offset: 0x0000AF0D
		// (set) Token: 0x06000E93 RID: 3731 RVA: 0x0000CD15 File Offset: 0x0000AF15
		public bool RestoresOwner { get; set; }

		// Token: 0x06000E94 RID: 3732 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
