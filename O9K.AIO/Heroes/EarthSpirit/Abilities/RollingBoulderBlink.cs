using System;
using System.Linq;
using Ensage.SDK.Geometry;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.EarthSpirit.Abilities
{
	// Token: 0x02000192 RID: 402
	internal class RollingBoulderBlink : BlinkAbility
	{
		// Token: 0x0600082B RID: 2091 RVA: 0x00002F6B File Offset: 0x0000116B
		public RollingBoulderBlink(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00025794 File Offset: 0x00023994
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, Vector3 toPosition)
		{
			if (base.Owner.Distance(toPosition) < 200f)
			{
				return false;
			}
			Vector3 vector = Vector3Extensions.Extend2D(base.Owner.Position, toPosition, Math.Min(base.Ability.CastRange - 25f, base.Owner.Distance(toPosition)));
			Polygon.Rectangle rec = new Polygon.Rectangle(base.Owner.Position, vector, base.Ability.Radius);
			if (EntityManager9.Units.Any((Unit9 x) => x.IsHero && x.IsEnemy(this.Owner) && x.IsAlive && rec.IsInside(x.Position)))
			{
				return false;
			}
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
	}
}
