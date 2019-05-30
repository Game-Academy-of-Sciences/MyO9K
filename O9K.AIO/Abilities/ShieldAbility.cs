using System;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Heroes;
using O9K.Core.Helpers;

namespace O9K.AIO.Abilities
{
	// Token: 0x02000203 RID: 515
	internal class ShieldAbility : UsableAbility
	{
		// Token: 0x06000A42 RID: 2626 RVA: 0x000072F0 File Offset: 0x000054F0
		public ShieldAbility(ActiveAbility ability) : base(ability)
		{
			this.Shield = (IShield)ability;
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000A43 RID: 2627 RVA: 0x00007305 File Offset: 0x00005505
		public IShield Shield { get; }

		// Token: 0x06000A44 RID: 2628 RVA: 0x0002C114 File Offset: 0x0002A314
		public override bool ForceUseAbility(TargetManager targetManager, Sleeper comboSleeper)
		{
			if (!base.Ability.UseAbility(targetManager.Owner, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Owner);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x0002CA00 File Offset: 0x0002AC00
		public override bool ShouldCast(TargetManager targetManager)
		{
			Hero9 hero = targetManager.Owner.Hero;
			if (hero.IsInvulnerable)
			{
				return false;
			}
			if (hero.Equals(this.Shield.Owner))
			{
				if (!this.Shield.ShieldsOwner)
				{
					return false;
				}
			}
			else if (!this.Shield.ShieldsAlly)
			{
				return false;
			}
			ToggleAbility toggleAbility;
			return (!hero.IsMagicImmune || this.Shield.PiercesMagicImmunity(hero)) && !hero.HasModifier(this.Shield.ShieldModifierName) && ((toggleAbility = (this.Shield as ToggleAbility)) == null || !toggleAbility.Enabled);
		}

		// Token: 0x06000A46 RID: 2630 RVA: 0x000071DF File Offset: 0x000053DF
		public bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, float distance)
		{
			return base.Owner.Distance(targetManager.Target) <= distance && this.UseAbility(targetManager, comboSleeper, true);
		}

		// Token: 0x06000A47 RID: 2631 RVA: 0x0002C214 File Offset: 0x0002A414
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (!base.Ability.UseAbility(targetManager.Owner, targetManager.AllyHeroes, 1, 0, false, false))
			{
				return false;
			}
			float castDelay = base.Ability.GetCastDelay(targetManager.Owner);
			comboSleeper.Sleep(castDelay);
			base.Sleeper.Sleep(castDelay + 0.5f);
			base.OrbwalkSleeper.Sleep(castDelay);
			return true;
		}
	}
}
