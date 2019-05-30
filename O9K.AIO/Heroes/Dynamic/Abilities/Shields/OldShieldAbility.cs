using System;
using System.Linq;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Shields
{
	// Token: 0x020001A2 RID: 418
	internal class OldShieldAbility : OldUsableAbility
	{
		// Token: 0x06000878 RID: 2168 RVA: 0x000063D5 File Offset: 0x000045D5
		public OldShieldAbility(IShield ability) : base(ability)
		{
			this.Shield = ability;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000879 RID: 2169 RVA: 0x000063E5 File Offset: 0x000045E5
		public IShield Shield { get; }

		// Token: 0x0600087A RID: 2170 RVA: 0x000063ED File Offset: 0x000045ED
		public bool CanBeCasted(Unit9 target, Unit9 enemy, BlinkAbilityGroup blinks, ComboModeMenu menu)
		{
			return this.CanBeCasted(menu) && this.CanHit(target) && this.ShouldCast(target) && this.ShouldCastShield(enemy, blinks, menu);
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x000266C0 File Offset: 0x000248C0
		public override bool ShouldCast(Unit9 target)
		{
			if (target.IsInvulnerable)
			{
				return false;
			}
			if (target.Equals(this.Shield.Owner))
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
			return (!target.IsMagicImmune || this.Shield.PiercesMagicImmunity(target)) && !target.HasModifier(this.Shield.ShieldModifierName) && ((toggleAbility = (this.Shield as ToggleAbility)) == null || !toggleAbility.Enabled);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x00026750 File Offset: 0x00024950
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, EntityManager9.AllyHeroes, 1, 0, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x000267C8 File Offset: 0x000249C8
		protected virtual bool ShouldCastShield(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
		{
			float distance = base.Ability.Owner.Distance(target);
			float attackRange = base.Ability.Owner.GetAttackRange(target, 0f);
			return blinks.GetBlinkAbilities(base.Ability.Owner, menu).Any((OldBlinkAbility x) => x.Blink.Range > distance - attackRange) || distance <= target.GetAttackRange(base.Owner, 125f);
		}
	}
}
