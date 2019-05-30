using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Bloodseeker.Abilities
{
	// Token: 0x020001E3 RID: 483
	internal class BloodRite : DisableAbility
	{
		// Token: 0x0600099A RID: 2458 RVA: 0x00003482 File Offset: 0x00001682
		public BloodRite(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x00029E40 File Offset: 0x00028040
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			if (targetManager.Target.IsRuptured)
			{
				return true;
			}
			if (usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.bloodseeker_rupture) != null && targetManager.Target.GetImmobilityDuration() <= 0f)
			{
				return false;
			}
			return usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.item_cyclone) == null;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x00029EC4 File Offset: 0x000280C4
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			if (target.IsRuptured)
			{
				if (target.IsMoving)
				{
					return false;
				}
				if (!base.Ability.UseAbility(target, 1, false, false))
				{
					return false;
				}
			}
			if (!base.Ability.UseAbility(target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float num = base.Ability.GetHitTime(target) + 0.5f;
			float castDelay = base.Ability.GetCastDelay(target);
			target.SetExpectedUnitState(base.Disable.AppliesUnitState, num);
			comboSleeper.Sleep(castDelay);
			base.OrbwalkSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(num);
			return true;
		}
	}
}
