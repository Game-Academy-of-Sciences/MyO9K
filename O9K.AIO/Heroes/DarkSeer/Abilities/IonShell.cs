using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.DarkSeer.Abilities
{
	// Token: 0x020001C9 RID: 457
	internal class IonShell : BuffAbility
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x000069AE File Offset: 0x00004BAE
		public IonShell(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool ShouldCast(TargetManager targetManager)
		{
			return true;
		}

		// Token: 0x06000921 RID: 2337 RVA: 0x00028B38 File Offset: 0x00026D38
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			Unit9 unit = (from x in EntityManager9.Units
			where x.IsUnit && x.IsAlly(this.Owner) && x.IsAlive && !x.IsInvulnerable && this.Ability.CanHit(x) && x.Distance(target) < this.Ability.Radius && !x.HasModifier(this.Buff.BuffModifierName)
			orderby x.IsMyHero descending
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
