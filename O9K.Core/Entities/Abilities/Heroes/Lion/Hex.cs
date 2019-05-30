using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Lion
{
	// Token: 0x0200020F RID: 527
	[AbilityId(AbilityId.lion_voodoo)]
	public class Hex : RangedAbility, IDisable, IAppliesImmobility, IActiveAbility
	{
		// Token: 0x06000A17 RID: 2583 RVA: 0x00009199 File Offset: 0x00007399
		public Hex(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x000091B6 File Offset: 0x000073B6
		public UnitState AppliesUnitState { get; } = 74L;

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x000091BE File Offset: 0x000073BE
		public string ImmobilityModifierName { get; } = "modifier_lion_voodoo";

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000091C6 File Offset: 0x000073C6
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_lion_4);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}
	}
}
