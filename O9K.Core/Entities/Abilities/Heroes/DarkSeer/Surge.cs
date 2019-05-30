using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.DarkSeer
{
	// Token: 0x0200024C RID: 588
	[AbilityId(AbilityId.dark_seer_surge)]
	public class Surge : RangedAbility, IBuff, IActiveAbility
	{
		// Token: 0x06000AC5 RID: 2757 RVA: 0x00009B6B File Offset: 0x00007D6B
		public Surge(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06000AC6 RID: 2758 RVA: 0x00009B8D File Offset: 0x00007D8D
		public string BuffModifierName { get; } = "modifier_dark_seer_surge";

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00009B95 File Offset: 0x00007D95
		public bool BuffsAlly { get; } = 1;

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000AC8 RID: 2760 RVA: 0x00009B9D File Offset: 0x00007D9D
		public bool BuffsOwner { get; } = 1;

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00009BA5 File Offset: 0x00007DA5
		public override bool PositionCast
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_dark_seer_3);
				return abilityById != null && abilityById.Level > 0u;
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000ACA RID: 2762 RVA: 0x00007F9C File Offset: 0x0000619C
		public override bool UnitTargetCast
		{
			get
			{
				return !this.PositionCast;
			}
		}
	}
}
