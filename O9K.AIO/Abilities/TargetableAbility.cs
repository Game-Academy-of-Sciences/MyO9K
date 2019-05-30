using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000205 RID: 517
	internal class TargetableAbility : UsableAbility
	{
		// Token: 0x06000A4A RID: 2634 RVA: 0x00003F23 File Offset: 0x00002123
		public TargetableAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A4B RID: 2635 RVA: 0x000071B7 File Offset: 0x000053B7
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			return this.UseAbility(targetManager, comboSleeper, true);
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x0002CAE4 File Offset: 0x0002ACE4
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsVisible && !target.IsInvulnerable && !target.IsReflectingDamage && (!base.Ability.BreaksLinkens || !target.IsBlockingAbilities);
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x00010390 File Offset: 0x0000E590
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
