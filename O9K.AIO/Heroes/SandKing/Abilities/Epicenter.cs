using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.SandKing.Abilities
{
	// Token: 0x020000B6 RID: 182
	internal class Epicenter : NukeAbility
	{
		// Token: 0x060003AE RID: 942 RVA: 0x000032F0 File Offset: 0x000014F0
		public Epicenter(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060003AF RID: 943 RVA: 0x00004147 File Offset: 0x00002347
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return !targetManager.Target.IsMagicImmune || base.Ability.PiercesMagicImmunity(targetManager.Target);
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x00014DC0 File Offset: 0x00012FC0
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			if (!base.Owner.BaseUnit.IsVisibleToEnemies)
			{
				return true;
			}
			Unit9 target = targetManager.Target;
			return target.IsStunned || target.IsHexed;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x00014E08 File Offset: 0x00013008
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			UsableAbility usableAbility = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.item_blink);
			UsableAbility usableAbility2 = usableAbilities.Find((UsableAbility x) => x.Ability.Id == AbilityId.sandking_burrowstrike);
			float num = ((usableAbility2 != null) ? usableAbility2.Ability.CastRange : 0f) + ((usableAbility != null) ? usableAbility.Ability.CastRange : 0f) + base.Ability.Radius;
			return base.Owner.Distance(targetManager.Target) < num;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x00014EB0 File Offset: 0x000130B0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay + 0.5f);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
