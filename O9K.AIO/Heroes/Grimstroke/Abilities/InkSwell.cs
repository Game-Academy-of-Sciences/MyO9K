using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Grimstroke.Abilities
{
	// Token: 0x0200016A RID: 362
	internal class InkSwell : ShieldAbility
	{
		// Token: 0x0600077D RID: 1917 RVA: 0x000030D7 File Offset: 0x000012D7
		public InkSwell(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x00022CC8 File Offset: 0x00020EC8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Unit9 unit = (from x in EntityManager9.Units
			where x.IsUnit && x.IsAlly(this.Owner) && x.IsAlive && !x.IsInvulnerable && !x.IsMagicImmune && this.Ability.CanHit(x) && x.Distance(target) < this.Ability.Radius
			orderby x.IsRanged
			select x).FirstOrDefault<Unit9>();
			if (unit == null || !base.Ability.UseAbility(unit, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(unit);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
