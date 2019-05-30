using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Heroes;
using O9K.Core.Extensions;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;
using SharpDX;

namespace O9K.AIO.Heroes.Tusk.Abilities
{
	// Token: 0x02000067 RID: 103
	internal class Kick : NukeAbility
	{
		// Token: 0x06000226 RID: 550 RVA: 0x000032F0 File Offset: 0x000014F0
		public Kick(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000F558 File Offset: 0x0000D758
		public override bool ShouldConditionCast(TargetManager targetManager, IComboModeMenu menu, List<UsableAbility> usableAbilities)
		{
			if (!base.ShouldConditionCast(targetManager, menu, usableAbilities))
			{
				return false;
			}
			Hero9 hero = (from x in EntityManager9.Heroes
			where !x.Equals(base.Owner) && x.IsAlly(base.Owner) && x.IsAlive && x.Distance(base.Owner) < 1500f
			orderby x.Distance(base.Owner)
			select x).FirstOrDefault<Hero9>();
			Vector3 vector = (hero == null) ? EntityManager9.AllyFountain : hero.Position;
			if (Vector3Extensions.AngleBetween(base.Owner.Position, targetManager.Target.Position, vector) <= 30f)
			{
				return true;
			}
			if (base.Owner.Move(Vector3Extensions.Extend2D(targetManager.Target.Position, vector, -100f)))
			{
				base.OrbwalkSleeper.Sleep(0.1f);
				base.Sleeper.Sleep(0.1f);
				return false;
			}
			return false;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000F624 File Offset: 0x0000D824
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Target, 1, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			IDisable disable;
			if ((disable = (base.Ability as IDisable)) != null)
			{
				targetManager.Target.SetExpectedUnitState(disable.AppliesUnitState, base.Ability.GetHitTime(targetManager.Target));
			}
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
