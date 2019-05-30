using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Abilities.Items
{
	// Token: 0x0200020E RID: 526
	internal class ForceStaff : BlinkAbility
	{
		// Token: 0x06000A70 RID: 2672 RVA: 0x00002F6B File Offset: 0x0000116B
		public ForceStaff(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0002CE80 File Offset: 0x0002B080
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(base.Owner, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(base.Ability.GetHitTime(base.Owner.InFront(base.Ability.Range, 0f, true)));
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0002CEFC File Offset: 0x0002B0FC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (base.Owner.GetAngle(toPosition, false) > 0.5f)
			{
				return false;
			}
			if (!base.Ability.UseAbility(base.Owner, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(base.Ability.GetHitTime(base.Owner.InFront(base.Ability.Range, 0f, true)));
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0002CF90 File Offset: 0x0002B190
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
		{
			Unit9 target = targetManager.Target;
			if (base.Owner.Distance(target) < minDistance)
			{
				return false;
			}
			if (target.Distance(base.Owner) > base.Ability.Range + blinkDistance)
			{
				return false;
			}
			if (base.Owner.GetAngle(target.Position, false) > 0.5f)
			{
				return base.Owner.BaseUnit.Move(target.Position);
			}
			if (!base.Ability.UseAbility(base.Owner, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay();
			comboSleeper.Sleep(base.Ability.GetHitTime(target.Position));
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x0002D05C File Offset: 0x0002B25C
		public virtual bool UseAbilityOnTarget(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			if (target.GetAngle(base.Owner.Position, false) > 0.3f)
			{
				return false;
			}
			if (target.Distance(base.Owner) > base.Ability.Range + 100f)
			{
				return false;
			}
			if (!base.Ability.UseAbility(target, false, false))
			{
				return false;
			}
			comboSleeper.Sleep(base.Ability.GetHitTime(target.Position));
			base.Sleeper.Sleep(base.Ability.GetCastDelay() + 0.5f);
			return true;
		}
	}
}
