using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;

namespace O9K.AIO.Heroes.Magnus.Abilities
{
	// Token: 0x02000113 RID: 275
	internal class Shockwave : NukeAbility
	{
		// Token: 0x0600056F RID: 1391 RVA: 0x000032F0 File Offset: 0x000014F0
		public Shockwave(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x0001BB48 File Offset: 0x00019D48
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			if (base.Owner.Distance(targetManager.Target) < 500f)
			{
				return true;
			}
			if (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.magnataur_skewer) != null)
			{
				if (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.magnataur_reverse_polarity) != null)
				{
					return false;
				}
			}
			return usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.item_blink || x.Ability.Id == AbilityId.item_force_staff) == null || (float)base.Ability.GetDamage(targetManager.Target) > targetManager.Target.Health || base.Owner.Distance(targetManager.Target) <= 800f;
		}
	}
}
