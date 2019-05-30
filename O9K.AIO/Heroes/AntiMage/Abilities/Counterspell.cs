using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.AntiMage.Abilities
{
	// Token: 0x020001EC RID: 492
	internal class Counterspell : ShieldAbility
	{
		// Token: 0x060009C4 RID: 2500 RVA: 0x000030D7 File Offset: 0x000012D7
		public Counterspell(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x00006D58 File Offset: 0x00004F58
		public override bool ShouldCast(TargetManager targetManager)
		{
			return base.ShouldCast(targetManager) && EntityManager9.Abilities.OfType<ActiveAbility>().Any((ActiveAbility x) => x.UnitTargetCast && x.TargetsEnemy && x.CastPoint <= 0.1f && !x.Owner.IsAlly(base.Owner) && x.CanHit(base.Owner) && x.CanBeCasted(true) && x.Owner.GetAngle(base.Owner, true) < 0.5f);
		}
	}
}
