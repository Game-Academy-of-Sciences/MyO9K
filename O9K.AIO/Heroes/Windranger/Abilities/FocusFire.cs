using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;

namespace O9K.AIO.Heroes.Windranger.Abilities
{
	// Token: 0x0200003D RID: 61
	internal class FocusFire : TargetableAbility
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00002FCA File Offset: 0x000011CA
		public FocusFire(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000C820 File Offset: 0x0000AA20
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return !target.IsEthereal && ((target.Distance(base.Owner) < 300f && target.HealthPercentage < 50f) || target.IsStunned || target.IsRooted || target.IsHexed);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C888 File Offset: 0x0000AA88
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Unit9 target = targetManager.Target;
			UsableAbility usableAbility = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.windrunner_powershot);
			return usableAbility == null || (float)usableAbility.Ability.GetDamage(target) <= target.Health;
		}
	}
}
