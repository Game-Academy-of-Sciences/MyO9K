using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.AncientApparition
{
	// Token: 0x02000262 RID: 610
	[AbilityId(AbilityId.ancient_apparition_cold_feet)]
	public class ColdFeet : RangedAbility, IDebuff, IActiveAbility
	{
		// Token: 0x06000B15 RID: 2837 RVA: 0x0000A06A File Offset: 0x0000826A
		public ColdFeet(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x1700042A RID: 1066
		// (get) Token: 0x06000B16 RID: 2838 RVA: 0x0000A07E File Offset: 0x0000827E
		public string DebuffModifierName { get; } = "modifier_cold_feet";

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x0000A086 File Offset: 0x00008286
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_ancient_apparition_6);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x06000B18 RID: 2840 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}
	}
}
