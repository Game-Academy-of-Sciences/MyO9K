using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Juggernaut.Abililities
{
	// Token: 0x02000163 RID: 355
	internal class Omnislash : NukeAbility
	{
		// Token: 0x0600075E RID: 1886 RVA: 0x000032F0 File Offset: 0x000014F0
		public Omnislash(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0002273C File Offset: 0x0002093C
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (!base.ShouldCast(targetManager))
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return (long)(from x in EntityManager9.Units
			where x.IsUnit && !x.Equals(target) && x.IsAlive && x.IsVisible && !x.IsInvulnerable && !x.IsAlly(this.Owner) && x.Distance(target) < this.Ability.Radius + 50f
			select x).ToList<Unit9>().Count((Unit9 x) => !x.IsHero || x.IsIllusion) <= (long)((ulong)base.Ability.Level);
		}
	}
}
