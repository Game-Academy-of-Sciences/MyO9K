using System;
using System.Collections.Generic;
using Ensage;
using O9K.AIO.Heroes.Dynamic.Abilities.Specials;
using O9K.AIO.Modes.Combo;
using O9K.Core.Entities.Abilities.Base.Components;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Extensions;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Disables
{
	// Token: 0x020001BE RID: 446
	internal class OldDisableAbility : OldUsableAbility
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0000678F File Offset: 0x0000498F
		public OldDisableAbility(IDisable ability) : base(ability)
		{
			this.Disable = ability;
			this.isAmplifier = (ability is IHasDamageAmplify || ability.Id == AbilityId.item_bloodthorn || ability.Id == AbilityId.item_orchid);
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000067C7 File Offset: 0x000049C7
		public IDisable Disable { get; }

		// Token: 0x060008E7 RID: 2279 RVA: 0x000067CF File Offset: 0x000049CF
		public virtual bool CanBeCasted(Unit9 target, List<OldDisableAbility> disables, List<OldSpecialAbility> specials, ComboModeMenu menu)
		{
			return this.CanBeCasted(target, menu, true);
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x00027F74 File Offset: 0x00026174
		public void SetTimings(Unit9 target)
		{
			float num = base.Ability.GetHitTime(target) + 0.5f;
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target) + 0.1f);
			base.AbilitySleeper.Sleep(base.Ability.Handle, num + 0.1f);
			base.TargetSleeper.Sleep(target.Handle, 0.05f);
			target.SetExpectedUnitState(this.Disable.AppliesUnitState, num);
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x00028008 File Offset: 0x00026208
		public override bool ShouldCast(Unit9 target)
		{
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (base.Ability.BreaksLinkens && target.IsBlockingAbilities)
			{
				return false;
			}
			if (target.IsDarkPactProtected)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (this.Disable.UnitTargetCast)
				{
					return false;
				}
				float immobilityDuration = target.GetImmobilityDuration();
				if (immobilityDuration <= 0f || immobilityDuration + 0.05f > this.Disable.GetHitTime(target))
				{
					return false;
				}
			}
			if (target.IsStunned)
			{
				return this.ChainStun(target);
			}
			if (target.IsHexed)
			{
				return (this.isAmplifier && !AbilityExtensions.IsHex(this.Disable)) || this.ChainStun(target);
			}
			if (target.IsSilenced)
			{
				return !AbilityExtensions.IsSilence(this.Disable, false) || this.isAmplifier || this.ChainStun(target);
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x000067DB File Offset: 0x000049DB
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, EntityManager9.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			this.SetTimings(target);
			return true;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00028110 File Offset: 0x00026310
		protected virtual bool ChainStun(Unit9 target)
		{
			INuke nuke;
			if ((nuke = (this.Disable as INuke)) != null && (float)nuke.GetDamage(target) > target.Health)
			{
				return true;
			}
			float immobilityDuration = target.GetImmobilityDuration();
			return immobilityDuration > 0f && this.Disable.GetHitTime(target) + 0.05f > immobilityDuration;
		}

		// Token: 0x040004C1 RID: 1217
		private readonly bool isAmplifier;
	}
}
