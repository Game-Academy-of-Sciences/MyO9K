using System;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.EmberSpirit.Abilities
{
	// Token: 0x02000186 RID: 390
	internal class FireRemnantBlink : BlinkAbility
	{
		// Token: 0x060007EF RID: 2031 RVA: 0x00002F6B File Offset: 0x0000116B
		public FireRemnantBlink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x00024414 File Offset: 0x00022614
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
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
			float castDelay = base.Ability.GetCastDelay(toPosition);
			float hitTime = base.Ability.GetHitTime(toPosition);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(hitTime * 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
