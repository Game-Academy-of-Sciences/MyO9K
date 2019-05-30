using System;
using System.Collections.Generic;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Abilities.Heroes.Riki;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Riki.Abilities
{
	// Token: 0x020000BB RID: 187
	internal class TricksOfTheTrade : NukeAbility
	{
		// Token: 0x060003C9 RID: 969 RVA: 0x00004189 File Offset: 0x00002389
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new TricksOfTheTradeMenu(base.Ability, simplifiedName);
		}

		// Token: 0x060003CA RID: 970 RVA: 0x00004197 File Offset: 0x00002397
		public TricksOfTheTrade(ActiveAbility ability) : base(ability)
		{
			this.tricks = (TricksOfTheTrade)ability;
		}

		// Token: 0x060003CB RID: 971 RVA: 0x000154C8 File Offset: 0x000136C8
		public bool CancelChanneling(TargetManager targetManager)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				return false;
			}
			if (!base.Ability.IsChanneling || !base.Ability.BaseAbility.IsChanneling)
			{
				return false;
			}
			Unit9 target = targetManager.Target;
			return !target.IsStunned && !target.IsRooted && target.Distance(base.Owner) >= base.Ability.Radius && base.Owner.BaseUnit.Stop();
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0001554C File Offset: 0x0001374C
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			if (comboMenu.GetAbilitySettingsMenu<TricksOfTheTradeMenu>(this).SmartUsage)
			{
				Unit9 target = targetManager.Target;
				if (base.Owner.HealthPercentage < 25f)
				{
					return true;
				}
				if (target.GetImmobilityDuration() > 1.5f)
				{
					return true;
				}
				if (!target.IsRanged && target.Distance(base.Owner) < 200f && target.HealthPercentage < 40f)
				{
					return true;
				}
				int damage = base.Ability.GetDamage(target);
				if (Math.Floor((double)(base.Ability.Radius / target.Speed / this.tricks.AttackRate)) * (double)damage * 0.89999997615814209 < (double)target.Health)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0001561C File Offset: 0x0001381C
		public override bool ShouldCast(TargetManager targetManager)
		{
			Unit9 target = targetManager.Target;
			return target.IsVisible && (!target.IsInvulnerable || this.ChainStun(target, true)) && base.Ability.GetDamage(target) > 0;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00015660 File Offset: 0x00013860
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (base.Owner.HasAghanimsScepter)
			{
				List<Unit9> enemies = targetManager.EnemyHeroes;
				Unit9 unit = (from x in targetManager.AllyHeroes
				where (x.HealthPercentage < 50f || x.Equals(this.Owner)) && x.Distance(this.Owner) < this.Ability.CastRange && enemies.Any((Unit9 z) => z.Distance(x) < this.Ability.Radius)
				orderby enemies.Count((Unit9 z) => z.Distance(x) < this.Ability.Radius) descending
				select x).FirstOrDefault<Unit9>();
				if (unit == null)
				{
					return false;
				}
				if (!base.Ability.UseAbility(unit, false, false))
				{
					return false;
				}
			}
			else if (!base.Ability.UseAbility(targetManager.Target, targetManager.EnemyHeroes, 1, 0, false, false))
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

		// Token: 0x0400021A RID: 538
		private readonly TricksOfTheTrade tricks;
	}
}
