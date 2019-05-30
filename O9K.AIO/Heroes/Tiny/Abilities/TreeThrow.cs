using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Tiny.Abilities
{
	// Token: 0x02000076 RID: 118
	internal class TreeThrow : NukeAbility
	{
		// Token: 0x0600026F RID: 623 RVA: 0x000032F0 File Offset: 0x000014F0
		public TreeThrow(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0001032C File Offset: 0x0000E52C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			if (!target.IsVisible)
			{
				return false;
			}
			if (target.IsReflectingDamage)
			{
				return false;
			}
			if ((float)base.Ability.GetDamage(target) < target.Health)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (base.Ability.UnitTargetCast)
				{
					return false;
				}
				if (!this.ChainStun(target, true))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00010390 File Offset: 0x0000E590
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
