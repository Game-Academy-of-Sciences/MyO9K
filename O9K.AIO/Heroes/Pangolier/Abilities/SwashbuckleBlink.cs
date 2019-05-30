using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Pangolier;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Pangolier.Abilities
{
	// Token: 0x020000E9 RID: 233
	internal class SwashbuckleBlink : BlinkAbility
	{
		// Token: 0x060004B5 RID: 1205 RVA: 0x0000478F File Offset: 0x0000298F
		public SwashbuckleBlink(ActiveAbility ability) : base(ability)
		{
			this.swashbuckle = (Swashbuckle)ability;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x00018CB0 File Offset: 0x00016EB0
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (base.Owner.Distance(toPosition) < 300f)
			{
				return false;
			}
			Vector3 vector = Vector3Extensions.Extend2D(base.Owner.Position, toPosition, Math.Min(base.Ability.CastRange - 25f, base.Owner.Distance(toPosition)));
			Unit9 target = targetManager.Target;
			if (target != null && target.Distance(vector) < this.swashbuckle.Range)
			{
				if (!this.swashbuckle.UseAbility(vector, target.Position, false, false))
				{
					return false;
				}
			}
			else if (!this.swashbuckle.UseAbility(vector, vector, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(vector);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x04000293 RID: 659
		private readonly Swashbuckle swashbuckle;
	}
}
