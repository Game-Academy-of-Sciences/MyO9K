using System;
using System.Linq;
using O9K.AIO.Abilities;
using O9K.AIO.Abilities.Menus;
using O9K.AIO.Modes.Combo;
using O9K.AIO.TargetManager;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Helpers;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Tiny.Abilities
{
	// Token: 0x02000071 RID: 113
	internal class Toss : NukeAbility
	{
		// Token: 0x0600025D RID: 605 RVA: 0x000032F0 File Offset: 0x000014F0
		public Toss(ActiveAbility ability) : base(ability)
		{
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000FFC8 File Offset: 0x0000E1C8
		public override bool CanBeCasted(TargetManager targetManager, bool channelingCheck, IComboModeMenu comboMenu)
		{
			if (!base.CanBeCasted(targetManager, channelingCheck, comboMenu))
			{
				return false;
			}
			TossMenu menu = comboMenu.GetAbilitySettingsMenu<TossMenu>(this);
			if (menu.TossToAlly)
			{
				Unit9 unit = (from x in EntityManager9.Units
				where x.IsUnit && x.IsAlive && x.IsVisible && !x.IsMagicImmune && !x.IsInvulnerable && !x.Equals(this.Owner) && x.Distance(this.Owner) < this.Ability.Radius
				orderby x.Distance(this.Owner)
				select x).FirstOrDefault<Unit9>();
				if (!(unit != null) || !(unit == targetManager.Target))
				{
					return false;
				}
				this.tossTarget = targetManager.AllyHeroes.Find((Unit9 x) => menu.TossAlly.IsEnabled(x.Name) && !x.Equals(this.Owner) && x.Distance(this.Owner) < this.Ability.CastRange);
			}
			return true;
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00003836 File Offset: 0x00001A36
		public override UsableAbilityMenu GetAbilityMenu(string simplifiedName)
		{
			return new TossMenu(base.Ability, simplifiedName);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00010074 File Offset: 0x0000E274
		public override bool UseAbility(TargetManager targetManager, Sleeper comboSleeper, bool aoe)
		{
			if (this.tossTarget == null)
			{
				this.tossTarget = targetManager.Target;
			}
			if (this.tossTarget.HasModifier("modifier_tiny_toss"))
			{
				return false;
			}
			if (!base.Ability.UseAbility(this.tossTarget, false, false))
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
			this.tossTarget = null;
			return true;
		}

		// Token: 0x04000144 RID: 324
		private Unit9 tossTarget;
	}
}
