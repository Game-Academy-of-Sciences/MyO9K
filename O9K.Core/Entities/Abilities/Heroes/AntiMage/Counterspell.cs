using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.AntiMage
{
	// Token: 0x020003CF RID: 975
	[AbilityId(AbilityId.antimage_counterspell)]
	public class Counterspell : ActiveAbility, IShield, IActiveAbility
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
		public Counterspell(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006F7 RID: 1783
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0000E4FE File Offset: 0x0000C6FE
		public UnitState AppliesUnitState { get; }

		// Token: 0x170006F8 RID: 1784
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0000E506 File Offset: 0x0000C706
		public string ShieldModifierName { get; } = "modifier_antimage_counterspell";

		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x0000E50E File Offset: 0x0000C70E
		public bool ShieldsAlly { get; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0000E516 File Offset: 0x0000C716
		public bool ShieldsOwner { get; } = 1;
	}
}
