using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Phoenix;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Phoenix.Abilities
{
	// Token: 0x020000DA RID: 218
	internal class SunRay : NukeAbility
	{
		// Token: 0x06000475 RID: 1141 RVA: 0x000045C8 File Offset: 0x000027C8
		public SunRay(ActiveAbility ability) : base(ability)
		{
			this.ray = (SunRay)ability;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x000045DD File Offset: 0x000027DD
		public bool IsActive
		{
			get
			{
				return this.ray.IsSunRayActive;
			}
		}

		// Token: 0x06000477 RID: 1143 RVA: 0x00017F3C File Offset: 0x0001613C
		public bool AutoControl(TargetManager targetManager, Sleeper comboSleeper, float distanceMultiplier)
		{
			Unit9 target = targetManager.Target;
			if (!this.ray.IsSunRayActive)
			{
				if (this.ray.CanBeCasted(true) && this.ray.CanHit(target))
				{
					if (base.Owner.Distance(target) < 300f && !target.IsStunned && !target.IsHexed && !target.IsRooted)
					{
						return false;
					}
					if (this.ray.UseAbility(target, 1, false, false))
					{
						comboSleeper.Sleep(0.05f);
						return true;
					}
				}
				return false;
			}
			if (base.Owner.GetAngle(target.Position, false) > 2f)
			{
				this.ray.Stop();
				comboSleeper.Sleep(0.1f);
				return true;
			}
			if (base.Owner.Distance(target) > this.ray.CastRange * distanceMultiplier)
			{
				if (!this.ray.IsSunRayMoving)
				{
					this.ray.ToggleMovement();
					comboSleeper.Sleep(0.05f);
					return true;
				}
			}
			else if (this.ray.IsSunRayMoving)
			{
				this.ray.ToggleMovement();
				comboSleeper.Sleep(0.05f);
				return true;
			}
			base.Owner.BaseUnit.Move(target.Position);
			comboSleeper.Sleep(0.05f);
			return true;
		}

		// Token: 0x04000277 RID: 631
		private readonly SunRay ray;
	}
}
