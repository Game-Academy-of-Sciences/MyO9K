using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.Core.Entities.Abilities.Heroes.LegionCommander
{
	// Token: 0x02000357 RID: 855
	[AbilityId(AbilityId.legion_commander_press_the_attack)]
	public class PressTheAttack : RangedAbility, IBuff, IHealthRestore, IActiveAbility
	{
		// Token: 0x06000E7B RID: 3707 RVA: 0x0000CBE3 File Offset: 0x0000ADE3
		public PressTheAttack(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06000E7C RID: 3708 RVA: 0x0000CC1E File Offset: 0x0000AE1E
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_legion_commander_5);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06000E7E RID: 3710 RVA: 0x0000CC44 File Offset: 0x0000AE44
		public bool InstantHealthRestore { get; }

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x0000CC4C File Offset: 0x0000AE4C
		public string BuffModifierName { get; } = "modifier_legion_commander_press_the_attack";

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x06000E80 RID: 3712 RVA: 0x0000CC54 File Offset: 0x0000AE54
		public bool BuffsAlly { get; } = 1;

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		public bool BuffsOwner { get; } = 1;

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x0000CC64 File Offset: 0x0000AE64
		public string HealModifierName { get; } = "modifier_legion_commander_press_the_attack";

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0000CC6C File Offset: 0x0000AE6C
		public bool RestoresAlly { get; } = 1;

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06000E84 RID: 3716 RVA: 0x0000CC74 File Offset: 0x0000AE74
		public bool RestoresOwner { get; } = 1;

		// Token: 0x06000E85 RID: 3717 RVA: 0x0000372C File Offset: 0x0000192C
		public int HealthRestoreValue(Unit9 unit)
		{
			return 0;
		}
	}
}
