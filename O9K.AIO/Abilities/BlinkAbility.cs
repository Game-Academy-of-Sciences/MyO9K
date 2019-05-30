using System;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000200 RID: 512
	internal class BlinkAbility : UsableAbility
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x00003F23 File Offset: 0x00002123
		public BlinkAbility(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00002E73 File Offset: 0x00001073
		public override bool CanHit(TargetManager targetManager, IComboModeMenu comboMenu)
		{
			return true;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00007250 File Offset: 0x00005450
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			base.Sleeper.Sleep(base.Ability.GetCastDelay(targetManager.Target) + 0.5f);
			return true;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x0002C70C File Offset: 0x0002A90C
		public override bool ShouldCast(TargetManager targetManager)
		{
			if (targetManager.Target == null)
			{
				return true;
			}
			Unit9 target = targetManager.Target;
			return (!base.Ability.UnitTargetCast || target.IsVisible) && !target.HasModifier("modifier_pudge_meat_hook");
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x000244BC File Offset: 0x000226BC
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x0002C758 File Offset: 0x0002A958
		public virtual bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (base.Owner.Distance(toPosition) < 200f)
			{
				return false;
			}
			Vector3 vector = Vector3Extensions.Extend2D(base.Owner.Position, toPosition, Math.Min(base.Ability.CastRange - 25f, base.Owner.Distance(toPosition)));
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(vector);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0002C7F4 File Offset: 0x0002A9F4
		public virtual bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float minDistance, float blinkDistance)
		{
			if (base.Owner.Distance(targetManager.Target) < minDistance)
			{
				return false;
			}
			Vector3 vector = Vector3Extensions.Extend2D(targetManager.Target.Position, base.Owner.Position, blinkDistance);
			if (base.Owner.Distance(vector) > base.Ability.CastRange)
			{
				return false;
			}
			if (!base.Ability.UseAbility(vector, false, false))
			{
				return false;
			}
			float num = base.Ability.GetCastDelay(vector) + 0.1f;
			comboSleeper.Sleep(num);
			base.Sleeper.Sleep(num + 0.5f);
			base.OrbwalkSleeper.Sleep(num);
			return true;
		}
	}
}
