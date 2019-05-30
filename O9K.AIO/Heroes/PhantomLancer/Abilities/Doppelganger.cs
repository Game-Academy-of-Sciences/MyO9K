using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.PhantomLancer.Abilities
{
	// Token: 0x020000E1 RID: 225
	internal class Doppelganger : AoeAbility
	{
		// Token: 0x06000493 RID: 1171 RVA: 0x0000356A File Offset: 0x0000176A
		public Doppelganger(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00018498 File Offset: 0x00016698
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target.GetPredictedPosition(0.5f), false, false))
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
