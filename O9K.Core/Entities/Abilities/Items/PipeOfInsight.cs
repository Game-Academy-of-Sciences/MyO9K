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
	// Token: 0x02000199 RID: 409
	[AbilityId(AbilityId.item_pipe)]
	public class PipeOfInsight : AreaOfEffectAbility, IShield, IHasDamageBlock, IActiveAbility
	{
		// Token: 0x06000847 RID: 2119 RVA: 0x00022164 File Offset: 0x00020364
		public PipeOfInsight(Ability baseAbility) : base(baseAbility)
		{
			this.RadiusData = new SpecialData(baseAbility, "barrier_radius");
			this.blockData = new SpecialData(baseAbility, "barrier_block");
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000848 RID: 2120 RVA: 0x00007AD9 File Offset: 0x00005CD9
		public UnitState AppliesUnitState { get; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000849 RID: 2121 RVA: 0x00007AE1 File Offset: 0x00005CE1
		public bool BlocksDamageAfterReduction { get; }

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x0600084A RID: 2122 RVA: 0x00007AE9 File Offset: 0x00005CE9
		public DamageType BlockDamageType { get; } = 2;

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x0600084B RID: 2123 RVA: 0x00007AF1 File Offset: 0x00005CF1
		public string BlockModifierName { get; } = "modifier_item_pipe_barrier";

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x00007AF9 File Offset: 0x00005CF9
		public bool IsDamageBlockPermanent { get; }

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x00007B01 File Offset: 0x00005D01
		public string ShieldModifierName { get; } = "modifier_item_pipe_barrier";

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x0600084E RID: 2126 RVA: 0x00007B09 File Offset: 0x00005D09
		public bool ShieldsAlly { get; } = 1;

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x0600084F RID: 2127 RVA: 0x00007B11 File Offset: 0x00005D11
		public bool ShieldsOwner { get; } = 1;

		// Token: 0x06000850 RID: 2128 RVA: 0x00007B19 File Offset: 0x00005D19
		public float BlockValue(Unit9 target)
		{
			return this.blockData.GetValue(this.Level);
		}

		// Token: 0x040003F8 RID: 1016
		private readonly SpecialData blockData;
	}
}
