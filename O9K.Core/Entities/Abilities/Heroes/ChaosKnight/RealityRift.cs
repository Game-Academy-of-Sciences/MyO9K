using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.ChaosKnight
{
	// Token: 0x020003A2 RID: 930
	[AbilityId(AbilityId.chaos_knight_reality_rift)]
	public class RealityRift : RangedAbility
	{
		// Token: 0x06000FC2 RID: 4034 RVA: 0x00006527 File Offset: 0x00004727
		public RealityRift(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0000DE99 File Offset: 0x0000C099
		public override bool CanHitSpellImmuneEnemy
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_chaos_knight);
				return (abilityById != null && abilityById.Level > 0u) || base.CanHitSpellImmuneEnemy;
			}
		}
	}
}
