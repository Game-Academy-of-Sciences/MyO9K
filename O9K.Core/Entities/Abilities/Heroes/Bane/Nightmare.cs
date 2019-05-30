using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Bane
{
	// Token: 0x020001B3 RID: 435
	[AbilityId(AbilityId.bane_nightmare)]
	public class Nightmare : RangedAbility, IDisable, IShield, IActiveAbility
	{
		// Token: 0x060008D1 RID: 2257 RVA: 0x0000808C File Offset: 0x0000628C
		public Nightmare(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170002F8 RID: 760
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000080B7 File Offset: 0x000062B7
		public UnitState AppliesUnitState { get; } = 32L;

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x000080BF File Offset: 0x000062BF
		public string ShieldModifierName { get; } = "modifier_bane_nightmare";

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x000080C7 File Offset: 0x000062C7
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x060008D5 RID: 2261 RVA: 0x000080CF File Offset: 0x000062CF
		public bool ShieldsOwner { get; } = 1;
	}
}
