using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Oracle
{
	// Token: 0x020001A8 RID: 424
	[AbilityId(AbilityId.oracle_fates_edict)]
	public class FatesEdict : RangedAbility, IDebuff, IShield, IActiveAbility
	{
		// Token: 0x0600089D RID: 2205 RVA: 0x00007E50 File Offset: 0x00006050
		public FatesEdict(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x0600089E RID: 2206 RVA: 0x00007E89 File Offset: 0x00006089
		public UnitState AppliesUnitState { get; } = 514L;

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x0600089F RID: 2207 RVA: 0x00007E91 File Offset: 0x00006091
		public string DebuffModifierName { get; } = "modifier_oracle_fates_edict";

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00007E99 File Offset: 0x00006099
		public string ShieldModifierName { get; } = "modifier_oracle_fates_edict";

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00007EA1 File Offset: 0x000060A1
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00007EA9 File Offset: 0x000060A9
		public bool ShieldsOwner { get; } = 1;
	}
}
