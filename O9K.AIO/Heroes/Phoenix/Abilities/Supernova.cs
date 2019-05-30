using System;
using System.Linq;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using SharpDX;

namespace O9K.AIO.Heroes.Phoenix.Abilities
{
	// Token: 0x020000DB RID: 219
	internal class Supernova : NukeAbility
	{
		// Token: 0x06000478 RID: 1144 RVA: 0x000032F0 File Offset: 0x000014F0
		public Supernova(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00018088 File Offset: 0x00016288
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			Vector3 mouse = Game.MousePosition;
			Unit9 unit = (from x in targetManager.AllyHeroes
			where !x.Equals(this.Owner) && x.HealthPercentage < 35f && x.Distance(this.Owner) < this.Ability.CastRange
			orderby x.Distance(mouse)
			select x).FirstOrDefault<Unit9>() ?? base.Owner;
			if (!base.Ability.UseAbility(unit, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
