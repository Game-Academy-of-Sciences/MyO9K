using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.AncientApparition.Abilities
{
	// Token: 0x020001EF RID: 495
	internal class ChillingTouch : TargetableAbility
	{
		// Token: 0x060009D4 RID: 2516 RVA: 0x00002FCA File Offset: 0x000011CA
		public ChillingTouch(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060009D5 RID: 2517 RVA: 0x00010390 File Offset: 0x0000E590
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
