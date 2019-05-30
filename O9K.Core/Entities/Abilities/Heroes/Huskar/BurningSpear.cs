using System;
using Ensage;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Components.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;

namespace O9K.Core.Entities.Abilities.Heroes.Huskar
{
	// Token: 0x02000371 RID: 881
	[AbilityId(AbilityId.huskar_burning_spear)]
	public class BurningSpear : OrbAbility, IHarass, IActiveAbility
	{
		// Token: 0x06000F30 RID: 3888 RVA: 0x00009561 File Offset: 0x00007761
		public BurningSpear(Ability baseAbility) : base(baseAbility)
		{
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06000F31 RID: 3889 RVA: 0x0000D620 File Offset: 0x0000B820
		public override bool CanHitSpellImmuneEnemy
		{
			get
			{
				Ability abilityById = base.Owner.GetAbilityById(AbilityId.special_bonus_unique_huskar_5);
				return (abilityById != null && abilityById.Level > 0u) || base.CanHitSpellImmuneEnemy;
			}
		}
	}
}
