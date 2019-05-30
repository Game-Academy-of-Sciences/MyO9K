using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Leshrac.Abilities
{
	// Token: 0x0200011F RID: 287
	internal class DiabolicEdict : UsableAbility
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x00003F23 File Offset: 0x00002123
		public DiabolicEdict(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x00003880 File Offset: 0x00001A80
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return false;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x0001CBC0 File Offset: 0x0001ADC0
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return (target.IsStunned || target.IsInvulnerable || target.IsRooted) && base.Owner.Distance(target) <= 400f && !target.IsEthereal;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x0001CC0C File Offset: 0x0001AE0C
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			Unit9 target = targetManager.Target;
			float immobilityDuration = target.GetImmobilityDuration();
			if (immobilityDuration > 0f)
			{
				UsableAbility usableAbility = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.leshrac_split_earth);
				if (usableAbility == null)
				{
					return true;
				}
				if (immobilityDuration < usableAbility.Ability.GetHitTime(target) + base.Ability.GetHitTime(target))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00004FC2 File Offset: 0x000031C2
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(false, false))
			{
				return false;
			}
			base.Sleeper.Sleep(base.Ability.GetCastDelay() + 0.5f);
			return true;
		}
	}
}
