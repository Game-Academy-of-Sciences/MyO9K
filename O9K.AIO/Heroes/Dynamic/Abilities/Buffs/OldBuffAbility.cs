using System;
using System.Linq;
using O9K.AIO.Heroes.Dynamic.Abilities.Blinks;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Buffs
{
	// Token: 0x020001B0 RID: 432
	internal class OldBuffAbility : OldUsableAbility
	{
		// Token: 0x060008A6 RID: 2214 RVA: 0x00006582 File Offset: 0x00004782
		public OldBuffAbility(IBuff ability) : base(ability)
		{
			this.Buff = ability;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060008A7 RID: 2215 RVA: 0x00006592 File Offset: 0x00004792
		public IBuff Buff { get; }

		// Token: 0x060008A8 RID: 2216 RVA: 0x0000659A File Offset: 0x0000479A
		public bool CanBeCasted(Unit9 target, Unit9 enemy, ComboModeMenu menu, BlinkAbilityGroup blinks)
		{
			return this.CanBeCasted(menu) && this.CanHit(target) && this.ShouldCast(target) && this.ShouldCastBuff(enemy, blinks, menu);
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x000271DC File Offset: 0x000253DC
		public override bool ShouldCast(Unit9 target)
		{
			if (target.IsInvulnerable)
			{
				return false;
			}
			if (target.Equals(this.Buff.Owner))
			{
				if (!this.Buff.BuffsOwner)
				{
					return false;
				}
			}
			else if (!this.Buff.BuffsAlly)
			{
				return false;
			}
			ToggleAbility toggleAbility;
			return (!target.IsMagicImmune || this.Buff.PiercesMagicImmunity(target)) && !target.HasModifier(this.Buff.BuffModifierName) && ((toggleAbility = (this.Buff as ToggleAbility)) == null || !toggleAbility.Enabled);
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00026750 File Offset: 0x00024950
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

		// Token: 0x060008AB RID: 2219 RVA: 0x0002726C File Offset: 0x0002546C
		protected virtual bool ShouldCastBuff(Unit9 target, BlinkAbilityGroup blinks, ComboModeMenu menu)
		{
			float distance = base.Ability.Owner.Distance(target);
			float attackRange = base.Ability.Owner.GetAttackRange(target, 0f);
			return blinks.GetBlinkAbilities(base.Ability.Owner, menu).Any((OldBlinkAbility x) => x.Blink.Range > distance - attackRange) || distance <= attackRange + 125f;
		}
	}
}
