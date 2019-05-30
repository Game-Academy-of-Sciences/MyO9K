using System;
using O9K.Core.Entities.Abilities.Base.Types;
using O9K.Core.Entities.Units;
using O9K.Core.Managers.Entity;

namespace O9K.AIO.Heroes.Dynamic.Abilities.Nukes
{
	// Token: 0x020001A5 RID: 421
	internal class OldNukeAbility : OldUsableAbility
	{
		// Token: 0x06000886 RID: 2182 RVA: 0x00006455 File Offset: 0x00004655
		public OldNukeAbility(INuke ability) : base(ability)
		{
			this.Nuke = ability;
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000887 RID: 2183 RVA: 0x00006465 File Offset: 0x00004665
		public INuke Nuke { get; }

		// Token: 0x06000888 RID: 2184 RVA: 0x00026A54 File Offset: 0x00024C54
		public override bool ShouldCast(Unit9 target)
		{
			if (base.Ability.UnitTargetCast && !target.IsVisible)
			{
				return false;
			}
			if (target.IsReflectingDamage)
			{
				return false;
			}
			if (base.Ability.BreaksLinkens && target.IsBlockingAbilities)
			{
				return false;
			}
			int damage = this.Nuke.GetDamage(target);
			if (damage <= 0)
			{
				return false;
			}
			if ((target.IsStunned || target.IsHexed) && base.Ability is IDisable && (float)damage < target.Health)
			{
				return false;
			}
			if (target.IsInvulnerable)
			{
				if (this.Nuke.UnitTargetCast)
				{
					return false;
				}
				float immobilityDuration = target.GetImmobilityDuration();
				if (immobilityDuration <= 0f || immobilityDuration + 0.05f > this.Nuke.GetHitTime(target))
				{
					return false;
				}
			}
			return !target.IsRooted || base.Ability.UnitTargetCast || target.GetImmobilityDuration() > 0f;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00026B38 File Offset: 0x00024D38
		public override bool Use(Unit9 target)
		{
			if (!base.Ability.UseAbility(target, EntityManager9.EnemyHeroes, 1, 0, false, false))
			{
				return false;
			}
			base.OrbwalkSleeper.Sleep(base.Ability.Owner.Handle, base.Ability.GetCastDelay(target));
			base.AbilitySleeper.Sleep(base.Ability.Handle, base.Ability.GetHitTime(target) + 0.5f);
			return true;
		}
	}
}
