using System;
using Ensage;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Metadata;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs.Unique
{
	// Token: 0x020001B3 RID: 435
	[AbilityId(AbilityId.item_manta)]
	internal class MantaStyleBuff : OldBuffAbility
	{
		// Token: 0x060008B2 RID: 2226 RVA: 0x00006602 File Offset: 0x00004802
		public MantaStyleBuff(IBuff ability) : base(ability)
		{
		}

		// Token: 0x060008B3 RID: 2227 RVA: 0x00027394 File Offset: 0x00025594
		protected override bool ShouldCastBuff(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
		{
			float num = base.Ability.Owner.Distance(target);
			float attackRange = base.Ability.Owner.GetAttackRange(target, 0f);
			return num <= attackRange;
		}
	}
}
