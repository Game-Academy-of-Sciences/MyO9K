using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.AntiMage.Abilities
{
	// Token: 0x020001EB RID: 491
	internal class AntiMageBlink : BlinkAbility
	{
		// Token: 0x060009C2 RID: 2498 RVA: 0x00002F6B File Offset: 0x0000116B
		public AntiMageBlink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x0002A788 File Offset: 0x00028988
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Unit9 target = targetManager.Target;
			float attackRange = base.Owner.GetAttackRange(target, 0f);
			float num = target.Distance(base.Owner);
			if (num <= attackRange + 100f)
			{
				return false;
			}
			if (num <= attackRange + 250f && base.Owner.Speed > targetManager.Target.Speed + 50f)
			{
				return false;
			}
			Vector3 vector = (target.GetAngle(base.Owner, false) < 1f) ? Vector3Extensions.Extend2D(target.Position, EntityManager9.EnemyFountain, 100f) : target.GetPredictedPosition(base.Ability.CastPoint + 0.3f);
			if (base.Owner.Distance(vector) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
