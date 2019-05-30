using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.Core.Entities.Abilities.Items
{
	// Token: 0x0200018F RID: 399
	[AbilityId(AbilityId.item_hood_of_defiance)]
	public class HoodOfDefiance : ActiveAbility, IShield, IHasDamageBlock, IActiveAbility
	{
		// Token: 0x06000809 RID: 2057 RVA: 0x00007784 File Offset: 0x00005984
		public HoodOfDefiance(Ability baseAbility) : base(baseAbility)
		{
			this.blockData = new SpecialData(baseAbility, "barrier_block");
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x0600080A RID: 2058 RVA: 0x000077C2 File Offset: 0x000059C2
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x0600080B RID: 2059 RVA: 0x000077CA File Offset: 0x000059CA
		public bool BlocksDamageAfterReduction { get; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x0600080C RID: 2060 RVA: 0x000077D2 File Offset: 0x000059D2
		public DamageType BlockDamageType { get; } = 2;

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600080D RID: 2061 RVA: 0x000077DA File Offset: 0x000059DA
		public string BlockModifierName { get; } = "modifier_item_hood_of_defiance_barrier";

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600080E RID: 2062 RVA: 0x000077E2 File Offset: 0x000059E2
		public bool IsDamageBlockPermanent { get; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000077EA File Offset: 0x000059EA
		public string ShieldModifierName { get; } = "modifier_item_hood_of_defiance_barrier";

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000810 RID: 2064 RVA: 0x000077F2 File Offset: 0x000059F2
		public bool ShieldsAlly { get; }

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000077FA File Offset: 0x000059FA
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000812 RID: 2066 RVA: 0x00007802 File Offset: 0x00005A02
		public float BlockValue(Unit9 target)
		{
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x040003C8 RID: 968
		private readonly SpecialData blockData;
	}
}
