using System;
using Ensage;
using O9K.AIO.Abilities;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Heroes.Alchemist;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;

namespace O9K.AIO.Heroes.Alchemist.Abilities
{
	// Token: 0x020001F4 RID: 500
	internal class UnstableConcoction : DisableAbility
	{
		// Token: 0x060009E9 RID: 2537 RVA: 0x00006DE1 File Offset: 0x00004FE1
		public UnstableConcoction(ActiveAbility ability) : base(ability)
		{
			this.unstableConcoction = (UnstableConcoctionThrow)ability;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x0002B0E8 File Offset: 0x000292E8
		public bool ThrowAway(TargetManager targetManager, Sleeper comboSleeper)
		{
			Unit9 target = targetManager.Target;
			if (target.IsVisible && !target.IsMagicImmune && !target.IsInvulnerable)
			{
				return false;
			}
			Modifier modifier = base.Owner.GetModifier("modifier_alchemist_unstable_concoction");
			if (modifier == null)
			{
				return false;
			}
			if (modifier.ElapsedTime <= this.unstableConcoction.BrewTime - base.Ability.CastPoint)
			{
				return false;
			}
			Unit9 unit = targetManager.EnemyHeroes.Find((Unit9 x) => base.Ability.CanHit(x));
			if (unit == null)
			{
				return false;
			}
			if (!base.Ability.UseAbility(unit, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(unit);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(base.Ability.GetHitTime(unit) + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x0002B1C8 File Offset: 0x000293C8
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.IsUsable && base.Ability.UseAbility(false, false))
			{
				comboSleeper.Sleep(0.1f);
				return true;
			}
			Modifier modifier = base.Owner.GetModifier("modifier_alchemist_unstable_concoction");
			if (modifier == null)
			{
				return false;
			}
			if (modifier.ElapsedTime < this.unstableConcoction.BrewTime - base.Ability.CastPoint)
			{
				Unit9 target = targetManager.Target;
				if (base.Owner.Distance(target) < base.Ability.CastRange - 100f || base.Owner.Speed > target.Speed || target.GetImmobilityDuration() > 0f)
				{
					return false;
				}
			}
			if (!base.Ability.UseAbility(targetManager.Target, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Target);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(base.Ability.GetHitTime(targetManager.Target) + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x00002E73 File Offset: 0x00001073
		protected override bool ChainStun(Unit9 target, bool invulnerability)
		{
			return true;
		}

		// Token: 0x0400053A RID: 1338
		private readonly UnstableConcoctionThrow unstableConcoction;
	}
}
