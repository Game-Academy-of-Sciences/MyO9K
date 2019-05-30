using System;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x02000196 RID: 406
	internal class StoneRemnantBlink : TargetableAbility
	{
		// Token: 0x06000837 RID: 2103 RVA: 0x00002FCA File Offset: 0x000011CA
		public StoneRemnantBlink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return true;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00025B28 File Offset: 0x00023D28
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(base.Owner.InFront(150f, 0f, true), false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(base.Owner.Position);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(2f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
